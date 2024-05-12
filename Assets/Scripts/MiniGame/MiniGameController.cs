using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class MiniGameController : MonoBehaviour
{
    public static MiniGameController Instance { get; private set; }

    [SerializeField] DrawLineGame drawLineGame;
    [SerializeField] GriffonGame griffonGame;
    [SerializeField] Timer Timer;

    [SerializeField] TutorialManager tutorialManager;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
            return;
        }
    }

    public void ExecuteMiniGame(IMiniGame miniGame)
    {
        if(!miniGame.IsActivate)
        {
            return;
        }    

        miniGame.Excute();
       
        // TODO: 상수 추출 필요.
        if(miniGame as DrawLineGame != null)
            StartCoroutine(ExitMiniGameAfter(miniGame, 5f));
    }

    public void ExecuteTutorialMinigame(EMinigameType type)
    {
        if (tutorialManager == null)
            return;

        switch (type)
        {
            case EMinigameType.DrawLine:
                drawLineGame.ExcuteTutorial();
                break;
            case EMinigameType.Griffon:
                griffonGame.ExcuteTutorial();
                break;
            default:
                break;
        }
    }

    IEnumerator ExitMiniGameAfter(IMiniGame miniGame, float delay)
    {
        yield return new WaitForSeconds(delay);

        ExitMiniGame(miniGame);
    }

    public void ExitMiniGame(IMiniGame miniGame)
    {
        if (!miniGame.IsRunnig)
            return;

        if(miniGame is DrawLineGame)
        {
            miniGame.Exit();
        }

        if (tutorialManager != null)
        {
            tutorialManager.ProceedStep();
            return;
        }

        Timer.ScheduleGame(miniGame.GetNextExcuteTime(), miniGame);
    }

    public void ActivateMiniGame(EMinigameType type, float runTime)
    {
        // TODO: 인터페이스로 추출.
        switch (type)
        {
            case EMinigameType.DrawLine:
                if (drawLineGame.GetAcivate()) return;

                Timer.ScheduleGame(drawLineGame.GetNextExcuteTime(), drawLineGame);
                drawLineGame.Activate(runTime);
                break;
            case EMinigameType.Griffon:
                if (griffonGame.GetAcivate()) return;

                Timer.ScheduleGame(griffonGame.GetNextExcuteTime(), griffonGame);
                griffonGame.Activate(runTime);
                break;
            default:
                break;
        }
    }

    public void GriffonSuccess()
    {
        tutorialManager.ProceedStep();
    }
}


