using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ScreenResolution : MonoBehaviour
{
    public int setWidth = 1440; // 사용자 설정 너비
    public int setHeight = 2770; // 사용자 설정 높이

    Resolution originResolution;

    private void OnPreRender()
    {
        GL.Clear(true, true, Color.black);
    }

    private void Start()
    {
        originResolution = Screen.currentResolution;
        SceneManager.sceneLoaded += OnSceneChange;
        // SetResolution();
    }


    private void OnSceneChange(Scene scene, LoadSceneMode mode)
    {
        SetResolution();
    }

    private void SetResolution()
    {
        if(SceneManager.GetActiveScene().buildIndex != (int)SceneType.Town)
        {
            Screen.SetResolution(originResolution.width, originResolution.height, true);    
            return;
        }

        int deviceWidth = Screen.width; // 기기 너비 저장
        int deviceHeight = Screen.height; // 기기 높이 저장

        Screen.SetResolution(setWidth, (int)(((float)deviceHeight / deviceWidth) * setWidth), true); // SetResolution 함수 제대로 사용하기

        if ((float)setWidth / setHeight < (float)deviceWidth / deviceHeight) // 기기의 해상도 비가 더 큰 경우
        {
            float newWidth = ((float)setWidth / setHeight) / ((float)deviceWidth / deviceHeight); // 새로운 너비
            Camera.main.rect = new Rect((1f - newWidth) / 2f, 0f, newWidth, 1f); // 새로운 Rect 적용
        }
        else // 게임의 해상도 비가 더 큰 경우
        {
            float newHeight = ((float)deviceWidth / deviceHeight) / ((float)setWidth / setHeight); // 새로운 높이
            Camera.main.rect = new Rect(0f, (1f - newHeight) / 2f, 1f, newHeight); // 새로운 Rect 적용
        }
    }

}
