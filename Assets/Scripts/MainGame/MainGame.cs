using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static LevelData;

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

    [Header("CountDown")]
    public GameObject countDown;
    public Text countDownTxt;

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
        StartGame();
    }


    public void StartGame()
    {
        StartCoroutine(GameStart());
    }

    public void EndGame()
    {
        gameOverPopup.SetActive(true);

        GridManager.clearGrid();
        LevelData.changeTileData(new GridPosition(-1, -1));
    }

    public IEnumerator GameStart()
    {
        blind.SetActive(true);

        yield return new WaitForSeconds(1f);
        countDown.SetActive(true);

        float countDownTime = 3f;

        while(countDownTime > 0f)
        {
            countDownTime -= Time.deltaTime;
            countDownTxt.text = Mathf.CeilToInt(countDownTime).ToString();
            yield return null;
        }

        countDownTxt.text = "Game Start!";
        yield return new WaitForSeconds(1.5f);

        countDown.SetActive(false);
        blind.SetActive(false);

        timer.StartTimer();
    }

    public void Retry()
    {
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
