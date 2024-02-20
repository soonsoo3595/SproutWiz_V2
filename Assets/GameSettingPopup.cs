using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameSettingPopup : MonoBehaviour
{
    [Header("Sound")]
    public Slider sfxSlider;
    public Slider bgmSlider;

    [Header("Correction")]
    public Toggle correctionMode;
    [SerializeField] private GameSetting gameSetting;

    void OnEnable()
    {
        bgmSlider.value = DataManager.playerData.bgmVolume;
        sfxSlider.value = DataManager.playerData.sfxVolume;

        correctionMode.isOn = DataManager.playerData.isCorrectionMode;
    }

    public void SetBGMVolume(float ratio)
    {
        DataManager.playerData.bgmVolume = ratio;

        GameManager.Instance.soundBGM.SetVolume(ratio);
    }

    public void SetSFXVolume(float ratio)
    {
        DataManager.playerData.sfxVolume = ratio;

        GameManager.Instance.soundEffect.SetVolume(ratio);
    }

    public void SetCorrectionMode(bool isOn)
    {
        DataManager.playerData.isCorrectionMode = isOn;

        // 하드 코딩 -> 바꿔야 함
        if(isOn)
        {
            gameSetting.DistanceFromHand = 327f;
            gameSetting.DistanceFromTetris_x = 0.47f;
            gameSetting.DistanceFromTetris_y = 0.7f;
        }
        else
        {
            gameSetting.DistanceFromHand = 0f;
            gameSetting.DistanceFromTetris_x = 0f;
            gameSetting.DistanceFromTetris_y = 0f;
        }
    }

}
