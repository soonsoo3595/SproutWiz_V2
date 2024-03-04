using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Town : MonoBehaviour
{
    [Header("Exit")]
    public Button exitBtn;

    [Header("Popup")]
    public PopupBtn exitPopup;
    public GameObject firstTutorial;

    private void Start()
    {
        if(PlayerPrefs.GetInt("FirstPlay") == 1)
        {
            firstTutorial.SetActive(true);
            PlayerPrefs.SetInt("FirstPlay", 0);
        }

        exitBtn.onClick.AddListener(() => GameManager.Instance.ExitGame());
    }

    private void OnEnable()
    {
        EventManager.exitGame += OnEscape;
    }

    private void OnDisable()
    {
        EventManager.exitGame -= OnEscape;
    }

    public void OnEscape()
    {
        exitPopup.Click();
    }
}
