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
    public bool dontSave;   // ���̺� ���ҰŸ� üũ(�ٸ� �� ���� �׽�Ʈ�Ҷ�)
    public bool canRecord;  // �������忡 ��ŷ �ø�����(�����Ϳ��� ������)
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
        Debug.Log("���� ����");
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
    /// ���� ������ ���̺� �� �� �ִ���
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
    /// ���ӿ� �ʿ��� �����͸� �ε�
    /// </summary>
    private void LoadGameData()
    {
        DataManager.LoadGameData();
    }
}
