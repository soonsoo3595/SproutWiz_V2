using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainGame : MonoBehaviour
{
    public Timer timer;
    public Button pauseBtn;

    public GameObject blind;

    [Header("Stage")]
    public Stage stage;
    public int initStage = 1;
    public int currentStage = 1;
    
    void Awake()
    {
        pauseBtn.onClick.AddListener(ClickPause);
        
        stage = new Stage();
    }

    void Start()
    {
        // 스테이지 세팅
        StartGame();
    }

    public void StartGame()
    {
        timer.StartTimer();
    }

    void ClickPause()
    {
        if(timer.isPaused)
        {
            timer.ResumeTimer();
            blind.SetActive(false);
        }
        else
        {
            timer.PauseTimer();
            blind.SetActive(true);
        }
    }
}
