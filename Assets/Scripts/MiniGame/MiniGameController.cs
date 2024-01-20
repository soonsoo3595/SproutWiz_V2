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

        // TODO: ��� ���� �ʿ�.
        StartCoroutine(ExitMiniGameAfter(miniGame, 15f));
    }

    IEnumerator ExitMiniGameAfter(IMiniGame miniGame, float delay)
    {
        yield return new WaitForSeconds(delay);

        miniGame.Exit();
    }
}


