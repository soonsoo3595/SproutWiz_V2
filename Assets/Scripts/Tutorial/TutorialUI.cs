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
            TMP.SetText($"�Ĺ��� ����� �Ӽ��� ��ũ�� ��� ���� �� ... ");
        }
        else if(index == 2)
        {
            TMP.SetText($"��ǥ�� �ܰ迡 ���� ��Ȯ�ؾ� �ϴ� ... ");
        }
        else if (index == 3)
        {
            TMP.SetText($"���� ��ư�� ����ؼ� ... ");
        }
        else if (index == 4)
        {
            TMP.SetText($"���� ������ �����ϸ� �̴ϰ��� ... ");
        }
        else if (index == 5)
        {
            TMP.SetText($"�׸����� ������ ... ");
        }
        else if (index == 6)
        {
            TMP.SetText($"Test6 ... ");
        }
    }
}
