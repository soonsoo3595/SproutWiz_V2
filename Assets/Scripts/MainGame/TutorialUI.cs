using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TutorialUI : MonoBehaviour
{
    [SerializeField] TutorialManager tutorialManager;

    [SerializeField] GameObject topText;
    [SerializeField] GameObject middleText;
    [SerializeField] GameObject bottomText;
    [SerializeField] GameObject TextBox;

    private Button button;

    void Awake()
    {
        button = GetComponent<Button>();

        if (button != null)
        {
            button.onClick.AddListener(delegate { OnButtonClick(); });
        }
    }

    private void OnButtonClick()
    {
        tutorialManager.ProceedStep();
    }

    public void EnableButton()
    {
        button.gameObject.SetActive(true);
    }

    public void DisableButton()
    {
        button.gameObject.SetActive(false);
    }

    public void ShowTimeAndBlockGuide()
    {
        middleText.SetActive(false);

        topText.SetActive(true);
        bottomText.SetActive(true);
    }

    public void ShowTextBox()
    {
        button.gameObject.SetActive(true);

        middleText.SetActive(false);
        topText.SetActive(false);
        bottomText.SetActive(false);

        TextBox.SetActive(true);
    }
}
