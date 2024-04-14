using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using Unity.Services.Authentication;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

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

    private GameObject networkPopup;
    private Button networkBtn;

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

    private void Start()
    {
        if (SceneObject.Instance == null)
            return;

        networkPopup = SceneObject.Instance.networkPopup;
        networkBtn = SceneObject.Instance.networkBtn;
    }

    public async void Save()
    {
        if(CanSave())
        {
            await DataManager.SaveData();
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

    public void RetryCheckNetwork()
    {
        networkPopup.SetActive(false);
        CheckNetwork();
    }

    public bool CheckNetwork()
    {
        if(Application.internetReachability == NetworkReachability.NotReachable)
        {
            networkPopup.SetActive(true);
            return false;
        }
        else 
        {
            return true; 
        }
        
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
