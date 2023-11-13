using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
    private float remainTime = 0f;


    public MainGame mainGame;
    public Slider slider;
    public float totalTime = 80f;
    public bool isPaused = false;

    private void Awake()
    {
        totalTime = GridManager.Instance.GetSetting().timeLimit;
    }

    private IEnumerator RunTimer()
    {
        while(remainTime > 0f)
        {
            if(!mainGame.isPaused)
            {
                remainTime -= Time.deltaTime;

                UpdateTimerUI();

                yield return null;
            }
            else
            {
                yield return null;
            }
        }
        
        mainGame.EndGame();
    }

    private void UpdateTimerUI()
    {
        slider.value = remainTime / totalTime;
    }

    public void ResetTimer()
    {
        remainTime = totalTime;
        UpdateTimerUI();
    }

    public void StartTimer()
    {
        remainTime = totalTime;
        UpdateTimerUI();
        StartCoroutine(RunTimer());
    }

}
