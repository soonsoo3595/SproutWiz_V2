using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    [Header("Audio")]
    public SoundEffect soundEffect;

    [Header("For Debug")]
    public bool isDebugMode;
    public int debugGold;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else if(Instance != this)
        {
            Destroy(gameObject);
        }

        DataManager.LoadData();
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
            if(BackMgr.instance != null && BackMgr.instance.st.Count > 0)
            {
                BackMgr.instance.Pop();
            }
            else
            {
#if UNITY_EDITOR
                UnityEditor.EditorApplication.isPlaying = false;
#else
                Application.Quit();
#endif
            }
        }
    }
}
