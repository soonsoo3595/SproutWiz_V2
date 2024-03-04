using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using TMPro;
using UnityEngine.UI;
using System.IO;

public class TitleScene : MonoBehaviour
{
    [Header("Object")]
    public GameObject logo;
    public GameObject guide;
    public TextMeshProUGUI guideTxt;
    public Button startBtn;
    public Transform start;
    public Transform end;

    [Header("Popup")]
    public GameObject registerPopup;
    public TMP_InputField inputField;

    private void Start()
    {
        CheckFirstPlay();
    }

    private void CheckFirstPlay()
    {
        if(PlayerPrefs.HasKey("FirstPlay"))
        {
            if(File.Exists(Application.persistentDataPath + "/playerData.json"))
                DataManager.LoadPlayerData();

        }
        else
        {
            PlayerPrefs.SetInt("FirstPlay", 1);
            // ShowPopup();
        }

        StartCoroutine(ReadyToStart());
    }


    public void CheckUserName()
    {
        // �г��� ��Ģ �˻�
        if(inputField.text.Length < 2 || inputField.text.Length > 8)
        {
            Debug.Log("�г����� 2�� �̻� 8�� ���Ϸ� �Է����ּ���");
            inputField.text = "";
            return;
        }

        // �ߺ� �˻�

        DataManager.playerData.userName = inputField.text;
        PlayerPrefs.SetInt("FirstPlay", 0);

        HidePopup();
        StartCoroutine(ReadyToStart());
    }

    public void InitData()
    {
        PlayerPrefs.DeleteAll();
        GameManager.Instance.ExitGame();
    }

    private void ShowPopup() => registerPopup.SetActive(true);

    private void HidePopup() => registerPopup.SetActive(false);

    IEnumerator ReadyToStart()
    {
        logo.transform.position = start.position;
        logo.transform.DOMove(end.position, 1.0f).SetEase(Ease.OutBounce);

        yield return new WaitForSeconds(1.5f);

        guide.SetActive(true);
        guideTxt.DOFade(1.0f, 1.0f).SetEase(Ease.InOutSine).SetLoops(-1, LoopType.Yoyo);

        startBtn.interactable = true;

        yield return null;
    }
}
