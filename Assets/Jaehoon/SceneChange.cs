using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneChange : MonoBehaviour
{
    Button btn;

    public string moveSceneName;

    private void Awake()
    {
        btn = GetComponent<Button>();
        btn.onClick.AddListener(MoveScene);
    }

    public void MoveScene()
    {
        SceneManager.LoadScene(moveSceneName);
    }

}
