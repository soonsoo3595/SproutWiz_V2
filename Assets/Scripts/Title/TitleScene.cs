using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using TMPro;
using UnityEngine.UI;

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
            Debug.Log("처음 플레이가 아닙니다");
            DataManager.LoadPlayerData();

            StartCoroutine(ReadyToStart());
        }
        else
        {
            Debug.Log("처음 플레이입니다");
            ShowPopup();

            GetComponentInChildren<SceneChange>().moveScene = SceneType.Tutorial;
        }
    }


    public void CheckUserName()
    {
        // 닉네임 규칙 검사
        if(inputField.text.Length < 2 || inputField.text.Length > 8)
        {
            Debug.Log("닉네임은 2자 이상 8자 이하로 입력해주세요");
            inputField.text = "";
            return;
        }

        // 중복 검사

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
