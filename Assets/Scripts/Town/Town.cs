using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Town : MonoBehaviour
{
    [Header("Popup")]
    public PopupBtn exitPopup;

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
