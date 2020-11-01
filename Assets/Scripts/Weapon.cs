using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class Weapon : MonoBehaviour
{
    [SerializeField]
    GameObject _ammoPrefab;

    [SerializeField]
    int _poolSize;

    [SerializeField]
    float _weaponVelocity;

    static GameObject[] _ammoPool;
    bool _isFiring;
    Animator _animator;
    Camera _localCamera;

    float _positiveSlope;
    float _negativeSlope;
    enum Quadrant
    {
        East,
        South,
        West,
        North
    }

    void Start()
    {
        _animator = GetComponent<Animator>();
        _isFiring = false;
        _localCamera = Camera.main;

        Vector2 lowerLeft = _localCamera.ScreenToWorldPoint(Vector2.zero);
        Vector2 upperRight = _localCamera.ScreenToWorldPoint(new Vector2(Screen.width, Screen.height));
        Vector2 upperLeft = _localCamera.ScreenToWorldPoint(new Vector2(0, Screen.height));
        Vector2 lowerRight = _localCamera.ScreenToWorldPoint(new Vector2(Screen.width, 0));

        _positiveSlope = GetSlope(lowerLeft, upperRight);
        _negativeSlope = GetSlope(upperLeft, lowerRight);
    }

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
            _isFiring = true;
            FireAmmo();
        }

        UpdateState();
    }

    private void UpdateState()
    {
        if (_isFiring)
        {
            Vector2 quadrantVector;
            Quadrant quadEnum = GetQuadrant();
            switch (quadEnum)
            {
                case Quadrant.East:
                    quadrantVector = Vector2.right;
                    break;
                case Quadrant.West:
                    quadrantVector = Vector2.left;
                    break;
                case Quadrant.North:
                    quadrantVector = Vector2.up;
                    break;
                case Quadrant.South:
                    quadrantVector = Vector2.down;
                    break;
                default:
                    quadrantVector = Vector2.zero;
                    break;
            }

            _animator.SetBool("isFiring", true);
            _animator.SetFloat("fireXDir", quadrantVector.x);
            _animator.SetFloat("fireYDir", quadrantVector.y);

            _isFiring = false;
        }
        else
        {
            _animator.SetBool("isFiring", false);
        }
    }

    private float GetSlope(Vector2 pointOne, Vector2 pointTwo)
    {
        return (pointTwo.y - pointOne.y) / (pointTwo.x - pointOne.x);
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

    bool HigherThanPositiveSlope(Vector2 inputPos)
    {
        Vector2 playerPos = transform.position;
        Vector2 mousePos = _localCamera.ScreenToWorldPoint(inputPos);

        float yIntercept = playerPos.y - (_positiveSlope * playerPos.x);
        float inputIntercept = mousePos.y - (_positiveSlope * mousePos.y);

        return inputIntercept > yIntercept;
    }

    bool HigherThanNegativeSlope(Vector2 inputPos)
    {
        Vector2 playerPos = transform.position;
        Vector2 mousePos = _localCamera.ScreenToWorldPoint(inputPos);

        float yIntercept = playerPos.y - (_negativeSlope * playerPos.x);
        float inputIntercept = mousePos.y - (_negativeSlope * mousePos.y);

        return inputIntercept > yIntercept;
    }

    Quadrant GetQuadrant()
    {
        Vector2 mousePos = Input.mousePosition;
        Vector2 playerPos = transform.position;

        bool higherThanPosSlope = HigherThanPositiveSlope(mousePos);
        bool higherThanNegSlope = HigherThanNegativeSlope(mousePos);

        if (higherThanPosSlope && higherThanNegSlope) return Quadrant.North;
        else if (!higherThanPosSlope && higherThanNegSlope) return Quadrant.East;
        else if (higherThanPosSlope && !higherThanNegSlope) return Quadrant.West;
        else return Quadrant.South;
    }
}
