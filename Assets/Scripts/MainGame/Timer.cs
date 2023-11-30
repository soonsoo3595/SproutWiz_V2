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

    public AudioSource audioSource;
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
            if(RemainTime > 5f && audioSource.isPlaying)
            {
                audioSource.Stop();
            }
            else if(RemainTime < 5f && !audioSource.isPlaying)
            {
                audioSource.Play();
            }

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

        audioSource.Stop();
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

    public void AddTime(float time)
    {
        RemainTime = Mathf.Clamp(RemainTime + time, 0, totalTime);
    }
}
