using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SceneObject : MonoBehaviour
{
    public static SceneObject Instance;

    public GameObject networkPopup;
    public Button networkBtn;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        networkBtn.onClick.AddListener(() =>
        {
            GameManager.Instance.RetryCheckNetwork();
        });
    }
}
