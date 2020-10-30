using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    public static CameraManager Instance { get; private set; }
    public CinemachineVirtualCamera vCam;
    void Awake()
    {
        Instance = this;
        var vCamObj = GameObject.FindWithTag("VirtCam");
        vCam = vCamObj.GetComponent<CinemachineVirtualCamera>();
    }
}
