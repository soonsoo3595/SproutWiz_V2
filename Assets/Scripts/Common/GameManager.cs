using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    [Header("Audio")]
    public SoundBGM soundBGM;
    public SoundEffect soundEffect;

    [Header("Scene")]
    public Stack<SceneType> sceneList = new Stack<SceneType>();

    [Header("For Debug")]
    public bool isDebugMode;
    public int debugGold;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            LoadData();
        }
        else if(Instance != this)
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        if(isDebugMode)
        {
            DataManager.playerData.gold = debugGold;
        }
    }

    void Update()
    {
        if(Input.GetKeyUp(KeyCode.Escape))
        {
            if(BackMgr.instance != null && BackMgr.instance.GetCount() > 0)
            {
                BackMgr.instance.Pop();
            }
            else
            {
                if (sceneList.Count == 0) return;
                if (SceneManager.GetActiveScene().buildIndex == (int)SceneType.Loading) return;

                if(sceneList.Peek() == SceneType.MainGame)
                {
                    
                }
                else if(sceneList.Peek() == SceneType.Town)
                {
                    EventManager.exitGame?.Invoke();
                }
                else
                {
                    sceneList.Pop();
                    LoadingScene.LoadScene(sceneList.Peek());
                }
            }
        }
    }

    private void OnApplicationQuit()
    {
        Debug.Log("게임 종료");

        if(!isDebugMode)
        {
            DataManager.SavePlayerData();
        }
    }

    public void ExitGame()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
                Application.Quit();
#endif
    }

    private void LoadData()
    {
        DataManager.LoadGameData();
        DataManager.playerData = new PlayerData("Default");
    }
}
