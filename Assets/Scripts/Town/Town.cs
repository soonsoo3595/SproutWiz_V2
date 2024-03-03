using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Town : MonoBehaviour
{
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
