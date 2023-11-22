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

    [Header("Status")]
    public bool isPaused = false;
    public bool isGameOver = false;
    public bool isFeverOn = false;
    public bool skipReady = false;

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
        StartCoroutine(Ready());
    }

    public void EndGame()
    {
        isGameOver = true;
        feverSystem.EndFever();
        gameOverPopup.SetActive(true);

        GridManager.clearGrid();
        LevelData.changeTileData(new GridPosition(-1, -1));
        GridManager.Instance.ResetDeployableGrid();
    }

    public IEnumerator Ready()
    {
        if(!skipReady)
        {
            blind.SetActive(true);

            yield return new WaitForSeconds(1f);
            countDown.SetActive(true);

            float countDownTime = 3f;

            while (countDownTime > 0f)
            {
                countDownTime -= Time.deltaTime;
                countDownTxt.text = Mathf.CeilToInt(countDownTime).ToString();
                yield return null;
            }

            countDownTxt.text = "Game Start!";
            yield return new WaitForSeconds(1.5f);

            countDown.SetActive(false);
            blind.SetActive(false);
        }

        timer.StartTimer();
        feverSystem.StartFever();
        isGameOver = false;
    }

    public void Retry()
    {
        ResetGame();

        gameOverPopup.SetActive(false);
        
        StartGame();
    }

    public void ResetGame()
    {
        GridManager.clearGrid();

        stage.InitStage();
        goalContainer.UpdateContainer();

        scoreSystem.InitScore();

        timer.ResetTimer();
    }

    void ClickPause()
    {
        isPaused = !isPaused;
        blind.SetActive(isPaused);
    }

}
