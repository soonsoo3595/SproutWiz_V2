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
                $"�Ĺ��� ����� �Ӽ��� ��ũ�� ��� ���� �� �Ĺ��� �װ�\n" +
                $"Ÿ���� ��� ���°� �ǹǷ� �����ؾߵſ�!\n\n" +
                $"��� ������ Ÿ�Ͽ��� ��ũ�� ����� ����� �� �����");
        }
        else if (index == 3)
        {
            textMeshPro.fontSize = 48;

            textMeshPro.SetText(
                $"���� ��ư�� ��ġ�ؼ� ��ũ�� ����� ��ü�غ��ô�\n\n" +
                $"���� �������� �� ĭ �� ������ 1ȸ ��� �����մϴ�");
        }
        else if (index == 4)
        {
            textMeshPro.fontSize = 46;

            textMeshPro.SetText(
                $"���� ������ �����ϸ� �̴ϰ����� ������\n\n" +
                $"�̴ϰ��� Ŭ���� �� �������� ������ ��带 ���� �� �ֽ��ϴ�\n\n" +
                $"������ ���� �� �̴ϰ����� ���Ⱦ��\n" +
                $"������ ������� ���� �巡�� �غ�����!");
        }
        else if (index == 5)
        {
            textMeshPro.fontSize = 46;

            textMeshPro.SetText(
                $"�׸����� �� ���� ���ƴٴϸ� ��縦 �����ϰ� �־��\n\n" +
                $"�׸����� ��ġ�ϸ� ������ ��带 ȹ���� �� �ֽ��ϴ�");
        }
        else if (index == 6)
        {
            textMeshPro.fontSize = 55;
            textMeshPro.alignment = TextAlignmentOptions.Center;

            textMeshPro.SetText(
                $"��� ������ �������ϴ�!\n\n" +
                $"������ ��縦 ����ô�!");
        }
    }

    public void HideTextBox()
    {
        TextBox.SetActive(false);
    }
}
