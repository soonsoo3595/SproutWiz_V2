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
        GameManager.Instance.sceneList.Push(moveScene);
        SceneManager.LoadScene((int)moveScene);
    }

}
