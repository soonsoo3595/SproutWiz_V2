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
        bgmSlider.value = PlayerPrefs.GetFloat("BGMVolume", 1f);
        sfxSlider.value = PlayerPrefs.GetFloat("SFXVolume", 1f);

        correctionMode.isOn = DataManager.playerData.isCorrectionMode;
    }

    public void SetBGMVolume(float ratio)
    {
        PlayerPrefs.SetFloat("BGMVolume", ratio);

        GameManager.Instance.soundBGM.SetVolume(ratio);
    }

    public void SetSFXVolume(float ratio)
    {
        PlayerPrefs.SetFloat("SFXVolume", ratio);

        GameManager.Instance.soundEffect.SetVolume(ratio);
    }

    public void SetCorrectionMode(bool isOn)
    {
        DataManager.playerData.isCorrectionMode = isOn;

        // �ϵ� �ڵ� -> �ٲ�� ��
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
