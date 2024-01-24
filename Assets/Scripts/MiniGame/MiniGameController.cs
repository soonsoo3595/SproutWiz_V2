using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiniGameController : MonoBehaviour
{
    public static MiniGameController Instance { get; private set; }

    [SerializeField] DrawLineGame drawLineGame;
    [SerializeField] GriffonGame griffonGame;
    [SerializeField] Timer Timer;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Debug.LogWarning("MiniGmaeController already exist");
            Destroy(gameObject);
            return;
        }
    }

    public void ExecuteMiniGame(IMiniGame miniGame)
    {
        if(!miniGame.IsActivate)
        {
            Debug.Log("미니게임 비활성화 상태!");
            return;
        }    

        miniGame.Excute();

        // TODO: 상수 추출 필요.
        StartCoroutine(ExitMiniGameAfter(miniGame, 15f));
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

        miniGame.Exit();

        Timer.ScheduleGame(miniGame.GetNextExcuteTime() + Timer.GetRunTime(), miniGame);
    }

    public void ActivateMiniGame(EMinigameType type, float runTime)
    {
        // TODO: 인터페이스로 추출.
        switch (type)
        {
            case EMinigameType.DrawLine:
                if (drawLineGame.GetAcivate()) return;

                Timer.ScheduleGame(drawLineGame.GetNextExcuteTime() + runTime, drawLineGame);
                drawLineGame.Activate(runTime);
                break;
            case EMinigameType.Griffon:
                if (griffonGame.GetAcivate()) return;

                Timer.ScheduleGame(griffonGame.GetNextExcuteTime() + runTime, griffonGame);
                griffonGame.Activate(runTime);
                break;
            default:
                break;
        }
    }
}


