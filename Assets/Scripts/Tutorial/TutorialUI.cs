using System.Collections;
using System.Collections.Generic;
using TMPro;
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

    public void EnableBackground()
    {
        button.gameObject.SetActive(true);
    }

    public void DisableBackground()
    {
        button.gameObject.SetActive(false);
    }

    public void ShowTimeAndBlockGuide()
    {
        middleText.SetActive(false);

        topText.SetActive(true);
        bottomText.SetActive(true);
    }

    public void ShowTextBox(int index)
    {
        EnableBackground();

        middleText.SetActive(false);
        topText.SetActive(false);
        bottomText.SetActive(false);

        TextBox.SetActive(true);


        TextMeshProUGUI TMP = TextBox.GetComponentInChildren<TextMeshProUGUI>();

        if (index == 1)
        {
            TMP.SetText($"식물에 취약한 속성의 스크롤 블록 적용 시 ... ");
        }
        else if(index == 2)
        {
            TMP.SetText($"목표는 단계에 따라 수확해야 하는 ... ");
        }
        else if (index == 3)
        {
            TMP.SetText($"리롤 버튼을 사용해서 ... ");
        }
        else if (index == 4)
        {
            TMP.SetText($"일정 점수에 도달하면 미니게임 ... ");
        }
        else if (index == 5)
        {
            TMP.SetText($"그리폰이 밭위를 ... ");
        }
        else if (index == 6)
        {
            TMP.SetText($"Test6 ... ");
        }
    }
}
