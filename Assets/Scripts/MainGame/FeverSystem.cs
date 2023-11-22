using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FeverSystem : MonoBehaviour
{
    public MainGame mainGame;
    public Button feverBtn;
    public Image disable;
    public GameObject feverImage;

    public float feverTime = 10f;
    public List<int> gaugeList;
    public float maxFeverGauge = 60f;

    private float feverGauge = 0f;
    public float FeverGauge
    {
        get => feverGauge;
        set
        {
            feverGauge = Mathf.Clamp(value, 0, maxFeverGauge);
            disable.fillAmount = feverGauge / maxFeverGauge;
        }
    }

    void Start()
    {
        feverBtn.onClick.AddListener(FeverOn);

        EventManager.harvestCount += IncreaseGauge;
        EventManager.resetMainGame += RetryGame;
    }

    public void FeverOn()
    {
        mainGame.isFeverOn = true;
        feverImage.SetActive(true);
        mainGame.gameRecord.feverCount++;

        feverBtn.interactable = false;

        Invoke("FeverOff", feverTime);
    }

    public void FeverOff()
    {
        if(mainGame.isGameOver) return;

        mainGame.isFeverOn = false;
        feverImage.SetActive(false);
        StartFever();
    }

    public void StartFever()
    {
        FeverGauge = 0f;
        StartCoroutine(FillFeverGauge());
    }

    public void EndFever()
    {
        mainGame.isFeverOn = false;
        feverImage.SetActive(false);
        feverBtn.interactable = false;
    }

    public void RetryGame()
    {
        FeverGauge = 0f;
    }

    IEnumerator FillFeverGauge()
    {
        while (FeverGauge < maxFeverGauge)
        {
            if(mainGame.isGameOver) yield break;

            if (mainGame.isPaused)
            {
                yield return null;
            }
            else
            {
                FeverGauge += 0.1f;

                yield return new WaitForSeconds(0.1f);
            }
        }

        feverBtn.interactable = true;
    }

    public void IncreaseGauge(int count)
    {
        if (count == 0) return;

        FeverGauge += gaugeList[count - 1];
    }
}
