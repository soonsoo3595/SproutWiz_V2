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
        // �ν����Ϳ��� Ȱ��, ��Ȱ�� üũ ���ؼ� Start �ۼ�.
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
