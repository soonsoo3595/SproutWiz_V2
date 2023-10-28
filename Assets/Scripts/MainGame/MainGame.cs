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

    public GoalContainer goalContainer;
    
    [Header("GameOver")] 
    public GameObject gameOverPopup;

    public Button retryBtn;

    [Header("System")]
    public ScoreSystem scoreSystem;
    public FeverSystem feverSystem;


    void Awake()
    {
        pauseBtn.onClick.AddListener(ClickPause);
        retryBtn.onClick.AddListener(Retry);
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

    public void EndGame()
    {
        gameOverPopup.SetActive(true);
    }

    public void Retry()
    {
        GridManager.clearGrid();

        gameOverPopup.SetActive(false);
        
        stage.InitStage();
        goalContainer.UpdateContainer();

        scoreSystem.InitScore();
        feverSystem.InitFever();

        timer.ResetTimer();
        StartGame();
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
