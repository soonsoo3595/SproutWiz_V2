using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using TMPro;
using UnityEngine.UI;
using System.IO;
using Unity.Services.Authentication;
using Unity.Services.Core;
using System.Threading.Tasks;

public class TitleScene : MonoBehaviour
{
    [Header("Object")]
    public GameObject logo;
    public TextMeshProUGUI guideTxt;
    public Button startBtn;
    public Transform start;
    public Transform end;

    [Header("Popup")]
    public GameObject userNamePopup;
    public TMP_InputField inputField;

    [Header("Login")]
    public GameObject unityLogin;
    public GameObject guestLogin;

    private void Start()
    {
        AuthenticationService.Instance.SignedIn += SignedIn;
        CheckToken();
    }

    private void OnDisable()
    {
        AuthenticationService.Instance.SignedIn -= SignedIn;
    }

    public async void CheckUserName()
    {
        // 닉네임 규칙 검사
        if(inputField.text.Length < 2 || inputField.text.Length > 10)
        {
            Debug.Log("닉네임은 2자 이상 10자 이하로 입력해주세요");
            inputField.text = "";
            return;
        }

        try
        {
            await AuthenticationService.Instance.UpdatePlayerNameAsync(inputField.text);
        }
        catch(RequestFailedException ex)
        {
            Debug.LogError(ex);
            inputField.text = "";
            return;
        }

        HidePopup();
        StartCoroutine(Animation());
    }


    private async void CheckToken()
    {
        bool hasToken = AuthenticationService.Instance.SessionTokenExists;

        if (hasToken)
        {
            Debug.Log("토큰이 남아있습니다");

            if (!AuthenticationService.Instance.IsSignedIn)
            {
                try
                {
                    await AuthenticationService.Instance.SignInAnonymouslyAsync();
                }
                catch (AuthenticationException ex)
                {
                    Debug.LogException(ex);
                }
                catch (RequestFailedException ex)
                {
                    Debug.LogException(ex);
                }
            }

            var identity = AuthenticationService.Instance.PlayerInfo.Identities;
            foreach (var item in identity)
            {
                Debug.Log(item.TypeId);
            }

            DataManager.LoadData();
        }
        else
        {
            Debug.Log("토큰이 없습니다 로그인하세요");
            ActivateSignIn(true);
        }
    }

    private void ShowPopup() => userNamePopup.SetActive(true);

    private void HidePopup() => userNamePopup.SetActive(false);

    private void ActivateSignIn(bool isActivate)
    {

#if UNITY_EDITOR
        unityLogin.SetActive(isActivate);
        guestLogin.SetActive(isActivate);
#else
        unityLogin.SetActive(isActivate);
        guestLogin.SetActive(isActivate);
#endif
    }

    private async void SignedIn()
    {
        ActivateSignIn(false);

        await AuthenticationService.Instance.GetPlayerNameAsync(false);

        if (CheckFirstPlay())
        {
            ShowPopup();
        }
        else
        {
            StartCoroutine(Animation());
        }
    }

    private bool CheckFirstPlay()
    {
        if(AuthenticationService.Instance.PlayerName == null)
        {
            return true;
        }
        else
        {
            return false; 
        }
    }

    IEnumerator Animation()
    {
        logo.transform.position = start.position;
        logo.transform.DOMove(end.position, 1.0f).SetEase(Ease.OutBounce);

        yield return new WaitForSeconds(1.5f);

        guideTxt.gameObject.SetActive(true);
        guideTxt.DOFade(1.0f, 1.0f).SetEase(Ease.InOutSine).SetLoops(-1, LoopType.Yoyo);

        startBtn.interactable = true;

        yield return null;
    }
}
