using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    public SpawnPoint playerSpawnPoint;
    [SerializeField]
    private CameraManager _cameraManager;
    void Awake()
    {
        Instance = this;
    }
    void Start()
    {
        SetupScene();
    }
    void Update()
    {
        if (Input.GetKey("escape")) Application.Quit();
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
            _cameraManager.vCam.Follow = player.transform;
        }
    }
}
