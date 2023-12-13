using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraOrthoHandler : MonoBehaviour
{
    readonly float screenRatiolimit = 2.7f;
    readonly float gridmapWorldSize = 5f;

    CinemachineVirtualCamera virtualCamera;

    void Awake()
    {
        virtualCamera = GetComponent<CinemachineVirtualCamera>();

        float screenWidth = Screen.width;
        float screenHeight = Screen.height;

        float screenRatio = (float)screenHeight / screenWidth;

        if (screenRatio < screenRatiolimit)
        {
            virtualCamera.m_Lens.OrthographicSize = (gridmapWorldSize * screenRatiolimit) / 2;
        }
    }
}
