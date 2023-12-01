using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ReRollSystem : MonoBehaviour
{
    public Button rerollBtn;
    public Image disable;

    public MainGame mainGame;
    public TetrisViewPanel tetrisViewPanel;

    public float reuseTime = 60f;

    private float coolTime = 0f;
    public float CoolTime
    {
        get => coolTime;
        set
        {
            coolTime = Mathf.Clamp(value, 0, reuseTime);
            disable.fillAmount = coolTime / reuseTime;
        }
    }

    private void Start()
    {
        rerollBtn.onClick.AddListener(ReRoll);

        EventManager.resetMainGame += RetryGame;
    }

    private void ReRoll()
    {
        GameManager.Instance.soundEffect.PlayOneShotSoundEffect("reroll");
        mainGame.gameRecord.rerollCount++;

        tetrisViewPanel.ReRoll();
        StartCoolTime();
    }

    private void StartCoolTime()
    {
        rerollBtn.interactable = false;
        CoolTime = 0f;
        StartCoroutine(CoolDown());
    }

    public void RetryGame()
    {
        CoolTime = reuseTime;
        rerollBtn.interactable = true;
    }

    IEnumerator CoolDown()
    {
        while (CoolTime < reuseTime)
        {
            if (mainGame.isGameOver) yield break;

            if (mainGame.isPaused)
            {
                yield return null;
            }
            else
            {
                CoolTime += Time.deltaTime;

                yield return new WaitForFixedUpdate();
            }
        }

        rerollBtn.interactable = true;
    }
}
