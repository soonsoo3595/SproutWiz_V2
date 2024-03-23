using System.Collections;
using System.Collections.Generic;
using System.Linq;
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

    private float runnigTime = 0f;

    SortedDictionary<float, IMiniGame> scedulMiniGame;

    private float originVolume;

    private void Awake()
    {
        timeLimit = mainGame.GetData().TimeLimit;
    }

    private void Start()
    {
        EventManager.resetMainGame += ResetTimer;

        scedulMiniGame = new SortedDictionary<float, IMiniGame>();

        originVolume = audioSource.volume;
        SetVolume(PlayerPrefs.GetFloat("SFXVolume"));
    }

    public void StartTimer()
    {
        RemainTime = timeLimit;
        runnigTime = 0f;
        StartCoroutine(RunTimer());
    }
    
    public void ResetTimer()
    {
        RemainTime = timeLimit;
        runnigTime = 0f;
        scedulMiniGame.Clear();
    }

    public void AddTime(float time)
    {
        RemainTime = Mathf.Clamp(RemainTime + time, 0, timeLimit);
    }

    public void SetVolume(float ratio)
    {
        float adjustVolme = Mathf.Lerp(0f, originVolume, ratio);

        audioSource.volume = adjustVolme;
    }

    private IEnumerator RunTimer()
    {
        while(RemainTime > 0f)
        {
            if(!mainGame.isPaused)
            {
                RemainTime -= Time.deltaTime;

                if (RemainTime > 5f && audioSource.isPlaying)
                {
                    audioSource.Stop();
                }
                else if (RemainTime < 5f && !audioSource.isPlaying)
                {
                    audioSource.Play();
                }

                // �̴ϰ��� �����ٷ�
                runnigTime += Time.deltaTime;

                if (scedulMiniGame.Count > 0 )
                {
                    foreach (var pair in scedulMiniGame)
                    {
                        //Debug.Log($"����ð� : {runnigTime}");

                        if (pair.Key  <= runnigTime)
                        {
                            //Debug.Log($"����ð� : {runnigTime}, ����ð� : {pair.Key}");

                            MiniGameController.Instance.ExecuteMiniGame(pair.Value);
                            scedulMiniGame.Remove(pair.Key);
                            break;
                        }
                    }
                }


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


    public float GetRunTime()
    {
        return runnigTime;
    }

    public void ScheduleGame(float time, IMiniGame game)
    {
        if (!scedulMiniGame.ContainsKey(time + runnigTime))
        {
            scedulMiniGame.Add(time + runnigTime, game);
            Debug.Log($"�̴ϰ��� ���� / ����ð�: {runnigTime}, ����ð�: {time + runnigTime}, ���� : {game}");
        }
        else
        {
            // �ð� �浹 ó��
            Debug.Log($"�̴ϰ��� �ð� �浿");
        }
    }
}
