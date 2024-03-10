using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.Services.Authentication;
using UnityEngine;
using UnityEngine.UI;

public class Town : MonoBehaviour
{
    [Header("Exit")]
    public Button exitBtn;
    public Button signOut;

    [Header("Popup")]
    public PopupBtn exitPopup;
    public GameObject firstTutorial;

    private void Start()
    {
        if(DataManager.playerData.isFirstPlay)
        {
            firstTutorial.SetActive(true);
        }

        exitBtn.onClick.AddListener(() => GameManager.Instance.ExitGame());
        signOut.onClick.AddListener(OnSignOut);
    }

    private void OnEnable()
    {
        EventManager.exitGame += OnEscape;
    }

    private void OnDisable()
    {
        EventManager.exitGame -= OnEscape;
    }

    public void OnClickTutorial()
    {
        DataManager.playerData.isFirstPlay = false;
    }

    public void OnEscape()
    {
        exitPopup.Click();
    }

    private void OnSignOut()
    {
        AuthenticationService.Instance.SignOut(true);
        DataManager.SaveData();
        DataManager.playerData = new PlayerData();
    }
}
