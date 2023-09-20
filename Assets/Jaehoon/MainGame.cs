using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainGame : MonoBehaviour
{
    public Timer timer;
    public Button pauseBtn;

    public GameObject blind;

    void Awake()
    {
        pauseBtn.onClick.AddListener(ClickPause);
    }

    void Start()
    {
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
