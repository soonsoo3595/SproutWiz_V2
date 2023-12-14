using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraOrthoHandler : MonoBehaviour
{
    readonly float screenRatiolimit = 2.4f;
    readonly float gridmapWorldSize = 6f;

    [SerializeField] RectTransform middleUI;
    CinemachineVirtualCamera virtualCamera;

    void Awake()
    {
        // 카메라 배율 조정.
        virtualCamera = GetComponent<CinemachineVirtualCamera>();

        float screenRatio = (float)Screen.height / Screen.width;

        if (screenRatio > screenRatiolimit)
        {
            virtualCamera.m_Lens.OrthographicSize = (gridmapWorldSize * screenRatiolimit) / 2;
        }


        // MiddleUI 위치 조정.
        Rect safeArea = Screen.safeArea;

        float separationY = Screen.height - (safeArea.height - safeArea.y);

        Vector3 separationWorldSpace =
            Camera.main.ScreenToWorldPoint(new Vector3(0f, Screen.height, 0f))
            - Camera.main.ScreenToWorldPoint(new Vector3(0f, safeArea.height + safeArea.y * 2, 0f));


        if(separationY > 0)
            middleUI.position = new Vector3(middleUI.position.x, middleUI.position.y + separationWorldSpace.y / 2, middleUI.position.z);


        Debug.Log($"screenRatio: {screenRatio}");
        Debug.Log($"screenWidth : {Screen.width}, screenHeight : {Screen.height}");
        Debug.Log($"Safe Area: {safeArea}");
        Debug.Log($"separationWorldSpaceY : {separationWorldSpace.y}");
        Debug.Log($"separationY : {separationY}");
    }
}
