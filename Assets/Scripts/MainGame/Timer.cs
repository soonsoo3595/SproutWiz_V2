using System.Collections;
using System.Collections.Generic;
using System.Linq;
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

    private float runnigTime = 0f;

    SortedDictionary<float, IMiniGame> scedulMiniGame;

    public AudioSource audioSource;
    public MainGame mainGame;
    public Image timebar;
    public float totalTime = 80f;

    private void Start()
    {
        totalTime = GridManager.Instance.GetSetting().timeLimit;
        EventManager.resetMainGame += ResetTimer;

        scedulMiniGame = new SortedDictionary<float, IMiniGame>();
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

                // 미니게임 스케줄러
                runnigTime += Time.deltaTime;

                if (scedulMiniGame.Count > 0 )
                {
                    foreach (var pair in scedulMiniGame)
                    {
                        if(pair.Key  <= runnigTime)
                        {
                            Debug.Log($"현재시간 : {runnigTime}, 예약시간 : {pair.Key + runnigTime}");

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
        mainGame.EndGame();
    }
    public void StartTimer()
    {
        RemainTime = totalTime;
        runnigTime = 0f;
        StartCoroutine(RunTimer());
    }
    
    public void ResetTimer()
    {
        RemainTime = totalTime;
        runnigTime = 0f;
        scedulMiniGame.Clear();
    }

    public void AddTime(float time)
    {
        RemainTime = Mathf.Clamp(RemainTime + time, 0, totalTime);
    }


    public float GetRunTime()
    {
        return runnigTime;
    }

    public void ScheduleGame(float time, IMiniGame game)
    {
        if (!scedulMiniGame.ContainsKey(time))
        {
            scedulMiniGame.Add(time, game);
            Debug.Log($"미니게임 예약 / 시간: {time}, 종류 : {game}");
        }
        else
        {
            // 시간 충돌 처리
        }
    }
}
