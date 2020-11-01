using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    [SerializeField]
    GameObject _ammoPrefab;

    [SerializeField]
    int _poolSize;

    [SerializeField]
    float _weaponVelocity;

    static GameObject[] _ammoPool;

    void Awake()
    {
        if (_ammoPool == null)
        {
            _ammoPool = new GameObject[_poolSize];
            _ammoPool = _ammoPool.Select(x =>
            {
                GameObject ammoObject = Instantiate(_ammoPrefab);
                ammoObject.SetActive(false);
                return ammoObject;
            }).ToArray();
        }
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            FireAmmo();
        }
    }

    private void FireAmmo()
    {
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        GameObject ammo = SpawnAmmo(transform.position);

        if (ammo)
        {
            Arc arc = ammo.GetComponent<Arc>();
            float travelDuration = 1f / _weaponVelocity;
            StartCoroutine(arc.TravelArc(mousePos, travelDuration));
        }
    }

    private GameObject SpawnAmmo(Vector3 location)
    {
        foreach (var ammo in _ammoPool)
        {
            if (!ammo.activeSelf)
            {
                ammo.SetActive(true);
                ammo.transform.position = location;
                return ammo;
            }
        }

        return null;
    }

    void OnDestroy()
    {
        _ammoPool = null;
    }
}
