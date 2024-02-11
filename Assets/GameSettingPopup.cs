using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameSettingPopup : MonoBehaviour
{
    [Header("Sound")]
    public Slider sfxSlider;
    public Slider bgmSlider;

    void OnEnable()
    {
        sfxSlider.value = DataManager.playerData.sfxVolume;
        bgmSlider.value = DataManager.playerData.bgmVolume;
    }

    public void SetSFXVolume(float ratio)
    {
        DataManager.playerData.sfxVolume = ratio;

        GameManager.Instance.soundEffect.SetVolume(ratio);
    }

    public void SetBGMVolume(float ratio)
    {
        DataManager.playerData.bgmVolume = ratio;

        GameManager.Instance.soundBGM.SetVolume(ratio);
    }

}
