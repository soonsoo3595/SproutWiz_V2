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
        btn.onClick.AddListener(MoveScene);
    }

    public void MoveScene()
    {
        if(SceneManager.GetActiveScene().buildIndex == (int)SceneType.MainGame)
        {
            EventManager.ClearMainGameEvents();
        }

        GameManager.Instance.sceneList.Push(moveScene);
        BackMgr.instance.Clear();
        LoadingScene.LoadScene(moveScene);
    }

}
