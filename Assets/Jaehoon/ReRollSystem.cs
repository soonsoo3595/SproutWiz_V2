using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ReRollSystem : MonoBehaviour
{
    public Button rerollBtn;
    public Image disable;
    public Text remainTime;

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
    }

    private void ReRoll()
    {
        mainGame.gameRecord.rerollCount++;

        tetrisViewPanel.ReRoll();
        StartCoolTime();
    }

    public void StartCoolTime()
    {
        rerollBtn.interactable = false;
        CoolTime = 0f;
        StartCoroutine(CoolDown());
    }

    public void RetryGame()
    {
        CoolTime = 0f;
        rerollBtn.interactable = true;
    }

    IEnumerator CoolDown()
    {
        while(CoolTime <= reuseTime)
        {
            if(mainGame.isGameOver) yield break;

            if(mainGame.isPaused)
            {
                yield return null;
            }
            else
            {
                CoolTime += Time.deltaTime;

                string t = TimeSpan.FromSeconds(CoolTime).ToString("s\\:ff");
                string[] tokens = t.Split(':');
                remainTime.text = string.Format("{0}", tokens[0]);

                yield return new WaitForFixedUpdate();
            }
        }
    
        rerollBtn.interactable = true;
    }

}
