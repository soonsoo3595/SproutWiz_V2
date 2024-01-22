using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using TMPro;

public class MainGame : MonoBehaviour
{
    public GameObject blind;
    public Button pauseBtn;

    [Header("Timer")]
    public Timer timer;
    
    [Header("Goal")]
    public GoalSystem goalSystem;

    [Header("GameOver")] 
    public GameObject gameOverPopup;

    [Header("CountDown")]
    public GameObject countDown;
    public TextMeshProUGUI countDownTxt;

    [Header("System")]
    public RewardSystem rewardSystem;
    public ManaCollector manaCollector;
    public ReRollSystem rerollSystem;
    public GameRecord gameRecord;

    [Header("Status")]
    public bool isPaused = false;
    public bool isGameOver = false;
    public bool isMagicOn = false;
    public bool skipReady = false;

    void Awake()
    {
        pauseBtn.onClick.AddListener(Pause);
    }

    void Start()
    {
        GameStart();
    }

    public void GameStart()
    {
        isGameOver = false;
        StartCoroutine(Ready());
    }

    public void GameEnd()
    {
        GameManager.Instance.soundEffect.PlayOneShotSoundEffect("gameover");

        isGameOver = true;

        gameOverPopup.SetActive(true);

        EventManager.mainGameOver();
        EventManager.changeTileData(new GridPosition(-1, -1));
        GridManager.Instance.ResetDeployableGrid();
    }

    public void Retry()
    {
        EventManager.resetMainGame();

        gameOverPopup.SetActive(false);

        GameStart();
    }

    private IEnumerator Ready()
    {
        if (!skipReady)
        {
            blind.SetActive(true);

            yield return new WaitForSeconds(1f);
            countDown.SetActive(true);

            float countDownTime = 2f;

            GameManager.Instance.soundEffect.PlayOneShotSoundEffect("countdownStart");

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
        goalSystem.UpdateContainer();
    }

    private void Pause()
    {
        isPaused = !isPaused;
        blind.SetActive(isPaused);

        if(isPaused)
        {
            GameManager.Instance.soundEffect.Pause();
            manaCollector.audioSource.Pause();
            timer.audioSource.Pause();
        }
        else
        {
            GameManager.Instance.soundEffect.Resume();
            manaCollector.audioSource.UnPause();
            timer.audioSource.UnPause();
        }
    }

}
