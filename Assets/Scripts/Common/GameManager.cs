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

    [Header("For Tutorial")]
    public bool isTutorial;

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
                if (SceneManager.GetActiveScene().buildIndex == (int)SceneType.Loading) return;

                if(sceneList.Peek() == SceneType.MainGame || sceneList.Peek() == SceneType.Tutorial || sceneList.Peek() == SceneType.Title)
                {
                    return;
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
        Debug.Log("���� ����");
        // ExitGame();
        if(!isDebugMode)
        {
            DataManager.SaveData();
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
    }
}
