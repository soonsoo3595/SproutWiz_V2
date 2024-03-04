using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TutorialUI : MonoBehaviour
{
    [SerializeField] TutorialManager tutorialManager;

    [SerializeField] GameObject backGround;

    [SerializeField] GameObject topText;
    [SerializeField] GameObject middleText;
    [SerializeField] GameObject bottomText;
    [SerializeField] GameObject TextBox;
    [SerializeField] GameObject DragText;
    [SerializeField] GameObject rerollText;
    [SerializeField] GameObject feverText;

    private Button button;
    private TutorialText tutorialText;


    void Awake()
    {
        button = GetComponent<Button>();

        if (button != null)
        {
            button.onClick.AddListener(delegate { OnButtonClick(); });
        }

        tutorialText = TextBox.GetComponent<TutorialText>();
    }

    private void OnButtonClick()
    {
        tutorialManager.ProceedStep();
    }

    public void EnableUI()
    {
        button.gameObject.SetActive(true);
    }

    public void DisableUI()
    {
        button.gameObject.SetActive(false);
    }

    public void ActivateButton(bool param)
    {
        button.enabled = param;
    }

    public void ActiveBackground(bool param)
    {
        backGround.SetActive(param);
    }

    public void ShowTimeAndBlockGuide(bool param)
    {
        middleText.SetActive(false);

        topText.SetActive(param);
        bottomText.SetActive(param);
    }

    public void EnableDragText(bool param)
    {
        DragText.SetActive(param);
    }

    public void EnableRerollText(bool param)
    {
        rerollText.SetActive(param);
    }

    public void EnableFeverText(bool param)
    {
        feverText.SetActive(param);
    }

    public void ShowTextBox(int index)
    {
        EnableUI();

        ActivateButton(true);

        middleText.SetActive(false);
        topText.SetActive(false);
        bottomText.SetActive(false);

        TextBox.SetActive(true);

        tutorialText.SetText(index);
    }

    public void HideTextBox()
    {
        TextBox.SetActive(false);
    }
}
