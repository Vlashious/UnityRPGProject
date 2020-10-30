using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPoint : MonoBehaviour
{
    [SerializeField]
    private GameObject _prefabToSpawn;
    [SerializeField]
    private float _repeatInterval;
    void Start()
    {
        if (_repeatInterval > 0)
        {
            InvokeRepeating("SpawnObject", 0, _repeatInterval);
        }
    }
    public GameObject SpawnObject()
    {
        if (_prefabToSpawn)
        {
            return Instantiate(_prefabToSpawn, transform.position, Quaternion.identity);
        }
        return null;
    }
}
