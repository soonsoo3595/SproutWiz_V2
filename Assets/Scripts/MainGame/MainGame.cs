using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using TMPro;

public class MainGame : MonoBehaviour
{
    public GameObject startEffect;
    public GameObject blind;

    [Header("Pause")]
    public GameObject pausePopup;
    public Button pauseBtn;

    [Header("Timer")]
    public Timer timer;
    
    [Header("Goal")]
    public GoalSystem goalSystem;

    [Header("GameOver")] 
    public GameObject gameOverPopup;

    [Header("System")]
    public RewardSystem rewardSystem;
    public ManaCollector manaCollector;
    public CastingCancel castingCancel;
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

    public void Continue()
    {
        isPaused = false;
        pausePopup.SetActive(false);

        GameManager.Instance.soundEffect.Resume();
        manaCollector.audioSource.UnPause();
        timer.audioSource.UnPause();
    }

    private IEnumerator Ready()
    {
        if (!skipReady)
        {
            blind.SetActive(true);

            yield return new WaitForSeconds(1f);

            goalSystem.StartCoroutine(goalSystem.StartAnimation());

            float countDownTime = 2f;

            GameManager.Instance.soundEffect.PlayOneShotSoundEffect("countdownStart");

            while (countDownTime > 0f)
            {
                countDownTime -= Time.deltaTime;
                yield return null;
            }

            startEffect.SetActive(true);

            yield return new WaitForSeconds(1f);

            blind.SetActive(false);
        }

        timer.StartTimer();
        castingCancel.ChargeStart();
        goalSystem.UpdateContainer();
    }

    private void Pause()
    {
        isPaused = true;
        pausePopup.SetActive(true);

        GameManager.Instance.soundEffect.Pause();
        manaCollector.audioSource.Pause();
        timer.audioSource.Pause();
    }

}
