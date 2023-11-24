using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiniGameController : MonoBehaviour
{
    [SerializeField] private Transform minigameUnit;


    Queue<MiniGameBase> MiniGameQueue;

    public static MiniGameController Instance { get; private set; }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            // DontDestroyOnLoad(gameObject);
        }
        else
        {
            Debug.Log("MiniGmaeController already exist");
            Destroy(gameObject);
            return;
        }


        MiniGameQueue = new Queue<MiniGameBase>();

    }

    private void Start()
    {
        MiniGameQueue.Enqueue(new Tornado());

    }

    public void ExecuteMiniGame()
    {
        if(MiniGameQueue.Count <= 0)
        {
            Debug.Log("대기중인 미니게임이 없습니다!");
            return;
        }


        MiniGameBase miniGame = MiniGameQueue.Dequeue();
        GridPosition[] AffectPositions = miniGame.Execute();

        foreach (GridPosition position in AffectPositions)
        {
            Instantiate(minigameUnit, GridManager.Instance.GetWorldPosition(position), Quaternion.identity);
        }
    }
}
