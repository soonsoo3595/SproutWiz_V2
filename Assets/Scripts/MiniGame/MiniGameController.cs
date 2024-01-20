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
                Debug.Log("�̴ϰ��� ��� ť�� �Ѻױ׸��� �߰�");
                break;
            default: 
                break;
        }
    }

    public void ExecuteMiniGame()
    {
        if(miniGameQueue.Count <= 0)
        {
            Debug.Log("������� �̴ϰ����� �����ϴ�!");
            return;
        }

        IMiniGame miniGame = miniGameQueue.Dequeue();
        miniGame.Excute();

        Debug.Log("�̴ϰ��� ����");
    }
}

public enum EMinigameType
{
    DrawLine,
    Griffin,
    Meteor
}
