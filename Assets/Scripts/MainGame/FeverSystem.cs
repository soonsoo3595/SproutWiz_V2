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
    public Text remainTime;
    public GameObject feverImage;

    public float feverTime = 10f;
    public float reuseTime = 40f;
    public float fisrtReuseTime = 35f;

    void Start()
    {
        feverBtn.onClick.AddListener(StartFever);
        DisableFeverBtn(fisrtReuseTime);
    }

    void StartFever()
    {
        mainGame.scoreSystem.StartFever();
        DisableFeverBtn(reuseTime);
        feverImage.SetActive(true);

        Invoke("EndFever", feverTime);
    }

    public void EndFever()
    {
        mainGame.scoreSystem.EndFever();
        feverImage.SetActive(false);
    }

    void DisableFeverBtn(float reuseTime)
    {
        feverBtn.interactable = false;
        StartCoroutine(CoolTimeFever(reuseTime));
    }

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

    public void InitFever()
    {
        DisableFeverBtn(fisrtReuseTime);
    }
}
