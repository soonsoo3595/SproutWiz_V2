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

        exitBtn.onClick.AddListener(() => GameManager.Instance.Exit());
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
        GameManager.Instance.Save();
    }

    public void OnEscape()
    {
        exitPopup.Click();
    }

}
