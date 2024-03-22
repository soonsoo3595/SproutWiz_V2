using UnityEngine;
using UnityEngine.UI;
using Unity.Services.Authentication.PlayerAccounts;
using Unity.Services.Core;
using Unity.Services.Authentication;
using TMPro;
using DG.Tweening;
using System.Collections;
using System;
using System.Threading.Tasks;

#if UNITY_ANDROID
using GooglePlayGames.BasicApi;
using GooglePlayGames;
#endif

public class SignManager : MonoBehaviour
{
    [Header("Sign")]
    [SerializeField] GameObject signInPanel; // 로그인 패널
    [SerializeField] Button unityBtn;   // 유니티 로그인
    [SerializeField] Button googleBtn;  // 구글 로그인
    [SerializeField] Button guestBtn;   // 게스트 로그인

    [Header("Popup")]
    [SerializeField] GameObject userNamePopup;
    [SerializeField] TMP_InputField inputField;
    [Space]
    [SerializeField] GameObject warningPopup;
    [SerializeField] TextMeshProUGUI warningTxt;
    [SerializeField] Button okBtn;

    async void Awake()
    {
        await UnityServices.InitializeAsync();

#if UNITY_ANDROID
        var config = new PlayGamesClientConfiguration.Builder()
            .RequestServerAuthCode(false)
            .RequestIdToken()
            .Build();

        // GPGS 초기화
        PlayGamesPlatform.InitializeInstance(config);
        PlayGamesPlatform.DebugLogEnabled = true;
        // 구글플레이 게임 활성화
        PlayGamesPlatform.Activate();
#endif
    }

    void Start()
    {
        #region 이벤트 등록
        AuthenticationService.Instance.SignedIn += OnSignIn;
        AuthenticationService.Instance.SignInFailed += OnSignFailed;
#if UNITY_EDITOR
        PlayerAccountService.Instance.SignedIn += SignInWithUnity;
#endif

        okBtn.onClick.AddListener(() => GameManager.Instance.Exit());
        #endregion

        #region 기기에 맞는 로그인 버튼
#if UNITY_EDITOR
        googleBtn.gameObject.SetActive(false);
#elif UNITY_ANDROID
        unityBtn.gameObject.SetActive(false);
        guestBtn.gameObject.SetActive(false);
#endif
        #endregion

        StartGame();
    }

    
    void OnDisable()
    {
        AuthenticationService.Instance.SignedIn -= OnSignIn;
        AuthenticationService.Instance.SignInFailed -= OnSignFailed;
#if UNITY_EDITOR
        PlayerAccountService.Instance.SignedIn -= SignInWithUnity;
#endif
    }

    public void StartGame()
    {
        CheckSessionToken();
    }

    #region 닉네임 검사
    public async void CheckUserName()
    {
        // 닉네임 규칙 검사
        if (inputField.text.Length < 2 || inputField.text.Length > 10)
        {
            Debug.Log("닉네임은 2자 이상 10자 이하로 입력해주세요");
            inputField.text = "";
            return;
        }

        try
        {
            await AuthenticationService.Instance.UpdatePlayerNameAsync(inputField.text);

            DataManager.playerData.SetInitInfo(AuthenticationService.Instance.PlayerId, inputField.text);
        }
        catch (RequestFailedException ex)
        {
            Debug.LogError(ex);
            inputField.text = "";
            return;
        }

        HidePopup();
        EventManager.EnterGame();
    }

    private void ShowPopup() => userNamePopup.SetActive(true);

    private void HidePopup() => userNamePopup.SetActive(false);

    #endregion

    /// <summary>
    /// 토큰이 유효한지 체크한다
    /// 유효하다면 이 토큰으로 로그인하며 유효하지 않다면 로그인할 수 있게
    /// </summary>
    private async void CheckSessionToken()
    {
        bool hasToken = AuthenticationService.Instance.SessionTokenExists;

        if(hasToken)
        {
            Debug.Log("토큰이 남아있습니다");

            #region 토큰이 있는데 로그인 안 되어있는 경우 현재 토큰으로 로그인
            if (!AuthenticationService.Instance.IsSignedIn)
            {
                try
                {
                    await AuthenticationService.Instance.SignInAnonymouslyAsync();
                }
                catch(Exception err)
                {
                    Debug.LogException(err);
                    return;
                }

                await DataManager.LoadData();
            }
            #endregion
            else
            {
                EventManager.EnterGame();
            }

        }
        else
        {
            Debug.Log("토큰이 없습니다 로그인하세요");
            ShowSignInPanel(true);
        }
    }

    private void ShowSignInPanel(bool show) => signInPanel.SetActive(show);

    private void ShowWarning(string notify)
    {
        warningTxt.text = notify;
        warningPopup.SetActive(true);
    }

    private async void OnSignIn()
    {
        ShowSignInPanel(false);

        await AuthenticationService.Instance.GetPlayerNameAsync(false);

        if (CheckFirstPlay())
        {
            ShowPopup();
        }
        else
        {
            EventManager.EnterGame();
        }
    }

    private void OnSignFailed(RequestFailedException exception)
    {
        string notify = "로그인에 실패했습니다\n 게임을 다시 시작해주세요";
        ShowWarning(notify);
        AuthenticationService.Instance.ClearSessionToken();
    }

    private bool CheckFirstPlay()
    {
        if (AuthenticationService.Instance.PlayerName == null)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    #region 게스트 로그인
    // Click Guest Login
    public async void SignInAnonymouslyRequest()
    {
        try
        {
            await AuthenticationService.Instance.SignInAnonymouslyAsync();
        }
        catch (RequestFailedException ex)
        {
            Debug.LogError($"Sign in anonymously failed with error code: {ex.ErrorCode}");
        }
    }
    #endregion

    #region 유니티 로그인
    // Click Unity Login
    public async void SignInUnityRequest()
    {
        if (PlayerAccountService.Instance.IsSignedIn)
        {
            Debug.Log("Player is already signed in.");
            SignInWithUnity();
            return;
        }

        try
        {
            await PlayerAccountService.Instance.StartSignInAsync();
        }
        catch (RequestFailedException ex)
        {
            Debug.LogException(ex);
        }
    }

    // Unity Login Token -> Authentication Service Login
    private async void SignInWithUnity()
    {
        if (AuthenticationService.Instance.IsSignedIn)
        {
            Debug.Log("Already Signed in");
            return;
        }

        try
        {
            await AuthenticationService.Instance.SignInWithUnityAsync(PlayerAccountService.Instance.AccessToken);
        }
        catch (RequestFailedException ex)
        {
            Debug.LogException(ex);
        }
    }
    #endregion

    #region 구글 로그인
    public void LoginGoogle()
    {
        Social.localUser.Authenticate(OnGoogleLogin);
    }

    async void OnGoogleLogin(bool success)
    {
        if (success)
        {
            string idToken = ((PlayGamesLocalUser)Social.localUser).GetIdToken();
            Debug.Log("Login with Google done. IdToken: " + idToken);
            // Call Unity Authentication SDK to sign in or link with Google.
            await SignInWithGoogleAsync(idToken);
        }
        else
        {
            Debug.Log("Unsuccessful login");
        }
    }

    async Task SignInWithGoogleAsync(string idToken)
    {
        try
        {
            await AuthenticationService.Instance.SignInWithGoogleAsync(idToken);
            Debug.Log("SignIn is successful.");
        }
        catch (AuthenticationException ex)
        {
            // Compare error code to AuthenticationErrorCodes
            // Notify the player with the proper error message
            Debug.LogException(ex);
        }
        catch (RequestFailedException ex)
        {
            // Compare error code to CommonErrorCodes
            // Notify the player with the proper error message
            Debug.LogException(ex);
        }
    }
    #endregion

    #region 로그아웃
    public void SignOut()
    {
        // 데이터를 저장하고 게임 종료 후 재접속 유도
        GameManager.Instance.Save();

        string notify = "로그아웃 되었습니다\n 게임을 다시 시작해주세요";
        ShowWarning(notify);

        AuthenticationService.Instance.SignOut(true);
    }

    #endregion
}
