using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainGame : MonoBehaviour
{
    public Timer timer;
    public Button pauseBtn;

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
        }
        else
        {
            timer.PauseTimer();
        }
    }
}
