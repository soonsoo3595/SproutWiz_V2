using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneChange : MonoBehaviour
{
    Button btn;

    public SceneType moveScene;

    private void Awake()
    {
        btn = GetComponent<Button>();
        
    }

    private void Start()
    {
        // 인스펙터에서 활성, 비활성 체크 위해서 Start 작성.
        btn.onClick.AddListener(MoveScene);
    }

    public void MoveScene()
    {
        int ActiveSceneIndex = SceneManager.GetActiveScene().buildIndex;

        if (ActiveSceneIndex == (int)SceneType.MainGame)
        {
            EventManager.ClearMainGameEvents();
        }

        if(moveScene == SceneType.Tutorial)
        {
            GameManager.Instance.isTutorial = true;
        }
        else
        {
            GameManager.Instance.isTutorial = false;
        }


        GameManager.Instance.sceneList.Push(moveScene);
        BackMgr.instance.Clear();
        LoadingScene.LoadScene(moveScene);
    }

}
