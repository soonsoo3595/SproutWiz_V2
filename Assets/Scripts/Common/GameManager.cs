using System.Collections;
using System.Collections.Generic;
using Unity.Services.Authentication;
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

    [Header("Test")]
    public bool isTestOnPC;
    public bool dontSave;   // 세이브 안할거면 체크(다른 씬 먼저 테스트할때)
    public bool canRecord;  // 리더보드에 랭킹 올릴건지(에디터에선 끄세요)
    public int debugGold;

    [Header("For Tutorial")]
    public bool isTutorial;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            LoadGameData();
        }
        else if(Instance != this)
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {

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
        Debug.Log("게임 종료");
        Save();
    }

    public void Save()
    {
        if(CanSave())
        {
            DataManager.SaveData();
        }   
    }

    public void Exit()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }

    /// <summary>
    /// 현재 게임을 세이브 할 수 있는지
    /// </summary>
    /// <returns></returns>
    private bool CanSave()
    {
        if (!AuthenticationService.Instance.IsSignedIn)
        {
            Debug.Log("Not Signed In");
            return false;
        }

        if(SceneManager.GetActiveScene().buildIndex == (int)SceneType.Title)
        {
            return false;
        }

        if(dontSave)
        {
            return false;
        }

        return true;
    }

    /// <summary>
    /// 게임에 필요한 데이터를 로드
    /// </summary>
    private void LoadGameData()
    {
        DataManager.LoadGameData();
    }
}
