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
    
    [Header("Goal")]
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
        isGameOver = false;
        StartCoroutine(Ready());
    }

    public void EndGame()
    {
        isGameOver = true;
        feverSystem.GameOver();
        
        StartCoroutine(GameOver());

        EventManager.changeTileData(new GridPosition(-1, -1));
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

            GameManager.Instance.soundEffect.PlayOneShotSoundEffect("countdownStart");

            while (countDownTime > 0f)
            {
                countDownTime -= Time.deltaTime;
                countDownTxt.text = Mathf.CeilToInt(countDownTime).ToString();
                yield return null;
            }

            GameManager.Instance.soundEffect.PlayOneShotSoundEffect("countdownEnd");

            countDownTxt.text = "Game Start!";
            yield return new WaitForSeconds(1.5f);

            countDown.SetActive(false);
            blind.SetActive(false);
        }

        timer.StartTimer();
        feverSystem.StartCoolTime();
        goalContainer.UpdateContainer();
    }

    IEnumerator GameOver()
    {
        GameManager.Instance.soundEffect.PlayOneShotSoundEffect("gameover");

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
            string scoreText = i.ToString("N0");
            score.GetComponent<TextMeshProUGUI>().text = scoreText;

            yield return new WaitForSeconds(0.001f);
        }

        yield return null;
    }

    public void Retry()
    {
        ResetGame();

        gameOverPopup.SetActive(false);
        
        StartGame();
    }

    private void ResetGame()
    {
        EventManager.resetMainGame();
    }

    void ClickPause()
    {
        isPaused = !isPaused;
        blind.SetActive(isPaused);

        if(isPaused)
        {
            GameManager.Instance.soundEffect.Pause();
        }
        else
        {
            GameManager.Instance.soundEffect.Resume();
        }
    }

}
