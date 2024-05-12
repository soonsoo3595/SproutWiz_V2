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
        int ActiveSceneIndex = SceneManager.GetActiveScene().buildIndex;

        if (ActiveSceneIndex == (int)SceneType.MainGame ||
            ActiveSceneIndex == (int)SceneType.Tutorial)
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
