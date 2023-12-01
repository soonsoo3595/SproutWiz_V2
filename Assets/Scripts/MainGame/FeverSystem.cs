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
    public Animator feverAnim;
    public AudioSource audioSource;

    public float feverTime = 10f;
    public List<int> gaugeList;
    public float maxFeverGauge = 60f;

    private float animationDuration = 0f;
    private float animationSpeed = 0f;
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

        #region 애니메이션 부분
        animationDuration = feverAnim.GetCurrentAnimatorStateInfo(0).length;
        animationSpeed = animationDuration / feverTime;
        feverAnim.speed = 0f;
        #endregion
    }

    public void FeverOn()
    {
        mainGame.isFeverOn = true;
        feverBtn.interactable = false;

        mainGame.gameRecord.feverCount++;
        StartCoroutine(DuringFever());
    }

    public void FeverOff()
    {
        mainGame.isFeverOn = false;
        StartCoolTime();
    }

    public void StartCoolTime()
    {
        FeverGauge = 0f;
        StartCoroutine(CoolDown());
    }

    public void GameOver()
    {
        mainGame.isFeverOn = false;
        feverBtn.interactable = false;
    }

    public void RetryGame()
    {
        FeverGauge = 0f;
    }

    IEnumerator CoolDown()
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

    IEnumerator DuringFever()
    {
        feverAnim.speed = animationSpeed;
        feverAnim.Play("Fever", 0, 0f);
        audioSource.Play();

        while(feverAnim.GetCurrentAnimatorStateInfo(0).normalizedTime < 1f)
        {
            if(mainGame.isGameOver) break;

            if (mainGame.isPaused)
            {
                feverAnim.speed = 0f;
                yield return null;
                feverAnim.speed = animationSpeed;
            }
            else yield return new WaitForFixedUpdate();
        }

        feverAnim.speed = 0f;
        feverAnim.Play("Fever", 0, 0f);
        audioSource.Stop();

        FeverOff();
    }

    public void IncreaseGauge(int count)
    {
        if (count == 0) return;

        FeverGauge += gaugeList[count - 1];
    }
}
