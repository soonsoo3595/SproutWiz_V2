using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
    public MainGame mainGame;
    public AudioSource audioSource;
    public Image timebar;

    private float timeLimit;
    private float remainTime = 0f;
    public float RemainTime
    {
        get { return remainTime; } 
        set
        {
            remainTime = value;
            timebar.fillAmount = remainTime / timeLimit;
        }
    }

    private void Start()
    {
        timeLimit = DataManager.GameData.timeLimit;
        EventManager.resetMainGame += ResetTimer;
    }

    public void StartTimer()
    {
        RemainTime = timeLimit;
        StartCoroutine(RunTimer());
    }
    
    public void ResetTimer()
    {
        RemainTime = timeLimit;
    }

    public void AddTime(float time)
    {
        RemainTime = Mathf.Clamp(RemainTime + time, 0, timeLimit);
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
        mainGame.GameEnd();
    }
}
