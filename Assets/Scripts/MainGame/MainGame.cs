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
    public bool isStart = false;
    public bool isPaused = false;
    public bool isGameOver = false;
    public bool isMagicOn = false;
    public bool skipReady = false;

    [Header("Data")]
    [SerializeField] private GameData gameData;

    void Awake()
    {
        pauseBtn.onClick.AddListener(Pause);
    }

    void Start()
    {
        GameStart();
    }

    private void OnApplicationPause(bool pause)
    {
        if (pause)
        {
            if (!isPaused && !isGameOver)
            {
                Pause();
            }
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (!isPaused && !isGameOver)
            {
                Pause();
            }
        }
    }

    public void GameStart()
    {
        isGameOver = false;
        StartCoroutine(Ready());
    }

    public void GameEnd()
    {
        GameManager.Instance.soundEffect.PlayOneShotSoundEffect("gameover");

        isStart = false;
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

        if(pausePopup != null)
            pausePopup.SetActive(false);

        GameManager.Instance.soundEffect.Resume();
        manaCollector.audioSource.UnPause();
        timer.audioSource.UnPause();
    }

    public GameData GetData() => gameData;

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

            yield return new WaitForSeconds(0.5f);

            startEffect.SetActive(true);

            yield return new WaitForSeconds(1f);

            blind.SetActive(false);
        }
        else
        {
            goalSystem.StartCoroutine(goalSystem.StartAnimation(skipReady));
        }

        yield return new WaitForSeconds(1f);

        isStart = true;

        if(!GameManager.Instance.isTutorial)
        {
            timer.StartTimer();
        }

        castingCancel.ChargeStart();
        goalSystem.UpdateContainer();
    }

    private void Pause()
    {
        if (!isStart)
            return;

        isPaused = true;

        if (pausePopup != null)
            pausePopup.SetActive(true);

        GameManager.Instance.soundEffect.Pause();
        manaCollector.audioSource.Pause();
        timer.audioSource.Pause();
    }

}
