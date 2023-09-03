using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
    private float remainTime = 0f;

    public Slider slider;
    public float totalTime = 10f;
    public bool isPaused = false;

    private IEnumerator RunTimer()
    {
        while(remainTime > 0f)
        {
            if(!isPaused)
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
    }

    private void UpdateTimerUI()
    {
        slider.value = remainTime / totalTime;
    }

    public void PauseTimer()
    {
        isPaused = true;
    }

    public void ResumeTimer()
    {
        isPaused = false;
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
