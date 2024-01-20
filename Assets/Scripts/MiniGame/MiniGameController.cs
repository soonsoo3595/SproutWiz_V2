using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiniGameController : MonoBehaviour
{
    [SerializeField] DrawLineGame drawLineGame;

    Queue<IMiniGame> miniGameQueue;

    public static MiniGameController Instance { get; private set; }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Debug.Log("MiniGmaeController already exist");
            Destroy(gameObject);
            return;
        }


        miniGameQueue = new Queue<IMiniGame>();

    }

    private void Start()
    {
        //miniGameQueue.Enqueue(new DrawLineGame());

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

        Debug.Log("미니게임 실행");
    }
}

public enum EMinigameType
{
    DrawLine,
    Griffin,
    Meteor
}
