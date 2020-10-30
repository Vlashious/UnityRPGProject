using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    public SpawnPoint playerSpawnPoint;
    void Awake()
    {
        Instance = this;
    }
    void Start()
    {
        SetupScene();
    }

    private void SetupScene()
    {
        SpawnPlayer();
    }

    public void SpawnPlayer()
    {
        if (playerSpawnPoint)
        {
            GameObject player = playerSpawnPoint.SpawnObject();
        }
    }
}
