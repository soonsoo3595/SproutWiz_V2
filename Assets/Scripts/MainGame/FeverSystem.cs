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
    }

    public void FeverOn()
    {
        mainGame.isFeverOn = true;
        feverImage.SetActive(true);

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
        StopAllCoroutines();
    }

    IEnumerator FillFeverGauge()
    {
        while (FeverGauge < maxFeverGauge)
        {
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

    /* 쿨타임(시간) -> 스택으로 변동 
    IEnumerator CoolTimeFever(float coolTime)
    {
        while(coolTime > 0.0f)
        {
            coolTime -= Time.deltaTime;

            disable.fillAmount = coolTime / reuseTime;

            string t = TimeSpan.FromSeconds(coolTime).ToString("s\\:ff");
            string[] tokens = t.Split(':');
            remainTime.text = string.Format("{0}", tokens[0]);

            yield return new WaitForFixedUpdate();
        }

        feverBtn.interactable = true;
    }
    */

    public void IncreaseGauge(int count)
    {
        if (count == 0) return;

        FeverGauge += gaugeList[count - 1];
    }
}
