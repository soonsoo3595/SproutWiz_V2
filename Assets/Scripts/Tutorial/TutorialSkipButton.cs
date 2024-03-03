using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class TutorialSkipButton : MonoBehaviour
{
    [SerializeField] GameObject questionWindow;

    private UnityEngine.UI.Button button;

    private void Awake()
    {
        button = GetComponent<UnityEngine.UI.Button>();
    }

    private void Start()
    {
        questionWindow.SetActive(false);
        button.onClick.AddListener(OpenWindow);
    }

    private void OpenWindow()
    {
        questionWindow.SetActive(true);
    }

    public void CloseWindow()
    {
        questionWindow.SetActive(false);
    }
}
