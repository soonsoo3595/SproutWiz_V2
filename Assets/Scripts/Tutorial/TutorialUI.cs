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
    private TextMeshProUGUI textMeshPro;


    void Awake()
    {
        button = GetComponent<Button>();

        if (button != null)
        {
            button.onClick.AddListener(delegate { OnButtonClick(); });
        }

        textMeshPro = TextBox.GetComponentInChildren<TextMeshProUGUI>();
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

        textMeshPro.fontSize = 40;
        textMeshPro.alignment = TextAlignmentOptions.Left;

        if (index == 1)
        {
            textMeshPro.fontSize = 45;

            textMeshPro.SetText(
                $"식물에 취약한 속성의 스크롤 블록 적용 시 식물이 죽고\n" +
                $"타일이 잠금 상태가 되므로 주의해야돼요!\n\n" +
                $"잠금 상태의 타일에는 스크롤 블록을 사용할 수 없어요");
        }
        else if (index == 3)
        {
            textMeshPro.fontSize = 48;

            textMeshPro.SetText(
                $"리롤 버튼을 터치해서 스크롤 블록을 교체해봅시다\n\n" +
                $"리롤 게이지가 한 칸 찰 때마다 1회 사용 가능합니다");
        }
        else if (index == 4)
        {
            textMeshPro.fontSize = 46;

            textMeshPro.SetText(
                $"일정 점수에 도달하면 미니게임이 열려요\n\n" +
                $"미니게임 클리어 시 일정향의 점수와 골드를 얻을 수 있습니다\n\n" +
                $"지금은 마나 맥 미니게임이 열렸어요\n" +
                $"마나가 사라지기 전에 드래그 해보세요!");
        }
        else if (index == 5)
        {
            textMeshPro.fontSize = 46;

            textMeshPro.SetText(
                $"그리폰이 밭 위를 날아다니며 농사를 방해하고 있어요\n\n" +
                $"그리핀을 터치하면 점수와 골드를 획득할 수 있습니다");
        }
        else if (index == 6)
        {
            textMeshPro.fontSize = 55;
            textMeshPro.alignment = TextAlignmentOptions.Center;

            textMeshPro.SetText(
                $"모든 설명이 끝났습니다!\n\n" +
                $"열심히 농사를 지어봅시다!");
        }
    }

    public void HideTextBox()
    {
        TextBox.SetActive(false);
    }
}
