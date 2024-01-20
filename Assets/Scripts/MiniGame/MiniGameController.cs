using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiniGameController : MonoBehaviour
{
    public static MiniGameController Instance { get; private set; }

    [SerializeField] DrawLineGame drawLineGame;

    Queue<IMiniGame> miniGameQueue;

   
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

        miniGameQueue = new Queue<IMiniGame>();
    }

    public void AddMiniGameQueue(EMinigameType type)
    {
        switch(type)
        {
            case EMinigameType.DrawLine:
                miniGameQueue.Enqueue(drawLineGame);
                Debug.Log("미니게임 대기 큐에 한붓그리기 추가");
                break;
            default: 
                break;
        }
    }

    public void ExecuteMiniGame()
    {
        if(miniGameQueue.Count <= 0)
        {
            Debug.Log("대기중인 미니게임이 없습니다!");
            return;
        }

        IMiniGame miniGame = miniGameQueue.Dequeue();
        miniGame.Excute();

        // TODO: 상수 추출 필요.
        StartCoroutine(ExitMiniGameAfter(miniGame, 15f));
    }

    IEnumerator ExitMiniGameAfter(IMiniGame miniGame, float delay)
    {
        yield return new WaitForSeconds(delay);

        miniGame.Exit();
    }
}


