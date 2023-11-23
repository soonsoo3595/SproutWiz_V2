using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
    private float remainTime = 0f;
    public float RemainTime
    {
        get { return remainTime; } 
        set
        {
            remainTime = value;
            timebar.fillAmount = remainTime / totalTime;
        }
    }

    public MainGame mainGame;
    public Image timebar;
    public float totalTime = 80f;

    private void Start()
    {
        totalTime = GridManager.Instance.GetSetting().timeLimit;
        EventManager.resetMainGame += ResetTimer;
    }

    private IEnumerator RunTimer()
    {
        while(RemainTime > 0f)
        {
            if(!mainGame.isPaused)
            {
                RemainTime -= Time.deltaTime;

                yield return null;
            }
            else
            {
                yield return null;
            }
        }
        
        mainGame.EndGame();
    }
    public void StartTimer()
    {
        RemainTime = totalTime;
        StartCoroutine(RunTimer());
    }
    
    public void ResetTimer()
    {
        RemainTime = totalTime;
    }


}
