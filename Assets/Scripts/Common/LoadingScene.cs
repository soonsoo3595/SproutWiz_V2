using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadingScene : MonoBehaviour
{
    public static SceneType loadScene;

    void Start()
    {
        StartCoroutine(Loading());
    }
    
    public static void LoadScene(SceneType sceneType)
    {
        loadScene = sceneType;
        SceneManager.LoadScene((int)SceneType.Loading);
    }

    IEnumerator Loading()
    {
        yield return null;

        AsyncOperation asyncOperation;
        asyncOperation = SceneManager.LoadSceneAsync((int)loadScene);

        while(!asyncOperation.isDone)
        {
            yield return null;
        }
    }
}
