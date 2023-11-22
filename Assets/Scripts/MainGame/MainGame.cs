using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using static LevelData;
using TMPro;

public class MainGame : MonoBehaviour
{
    [Header("Timer")]
    public Timer timer;
    public Button pauseBtn;

    public GameObject blind;
    
    [Header("Stage")]
    public Stage stage;

    public GoalContainer goalContainer;
    
    [Header("GameOver")] 
    public GameObject gameOverPopup;
    public GameObject title;
    public TextMeshProUGUI[] records;
    public GameObject score;
    public GameObject buttons;

    public Button retryBtn;

    [Header("CountDown")]
    public GameObject countDown;
    public TextMeshProUGUI countDownTxt;

    [Header("System")]
    public RewardSystem scoreSystem;
    public FeverSystem feverSystem;
    public ReRollSystem rerollSystem;
    public GameRecord gameRecord;

    [Header("Status")]
    public bool isPaused = false;
    public bool isGameOver = false;
    public bool isFeverOn = false;
    public bool skipReady = false;

    void Awake()
    {
        pauseBtn.onClick.AddListener(ClickPause);
        retryBtn.onClick.AddListener(Retry);

        gameRecord = new GameRecord();
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
        
        StartCoroutine(GameOver());

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

        isGameOver = false;

        timer.StartTimer();
        feverSystem.StartFever();

    }

    IEnumerator GameOver()
    {
        gameOverPopup.SetActive(true);

        List<int> gameRecords = gameRecord.GetRecord();

        for (int i = 0; i < records.Length; i++)
        {
            for(int j = 0; j <= gameRecords[i]; j++)
            {
                string formattedNumber = j.ToString("0000");
                records[i].text = formattedNumber;

                yield return new WaitForSeconds(0.1f);
            }
        }

        for(int i = 0; i <= scoreSystem.Score; i += 10)
        {
            string scoreText = "Score\n" + i.ToString("N0");
            score.GetComponent<TextMeshProUGUI>().text = scoreText;

            yield return new WaitForSeconds(0.01f);
        }

        yield return null;
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

        EventManager.resetMainGame();

        gameRecord.InitRecord();
    }

    void ClickPause()
    {
        isPaused = !isPaused;
        blind.SetActive(isPaused);
    }

}
