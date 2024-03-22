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
    [SerializeField] GameObject signInPanel; // �α��� �г�
    [SerializeField] Button unityBtn;   // ����Ƽ �α���
    [SerializeField] Button googleBtn;  // ���� �α���
    [SerializeField] Button guestBtn;   // �Խ�Ʈ �α���

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

        // GPGS �ʱ�ȭ
        PlayGamesPlatform.InitializeInstance(config);
        PlayGamesPlatform.DebugLogEnabled = true;
        // �����÷��� ���� Ȱ��ȭ
        PlayGamesPlatform.Activate();
#endif
    }

    void Start()
    {
        #region �̺�Ʈ ���
        AuthenticationService.Instance.SignedIn += OnSignIn;
        AuthenticationService.Instance.SignInFailed += OnSignFailed;
#if UNITY_EDITOR
        PlayerAccountService.Instance.SignedIn += SignInWithUnity;
#endif

        okBtn.onClick.AddListener(() => GameManager.Instance.Exit());
        #endregion

        #region ��⿡ �´� �α��� ��ư
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

    #region �г��� �˻�
    public async void CheckUserName()
    {
        // �г��� ��Ģ �˻�
        if (inputField.text.Length < 2 || inputField.text.Length > 10)
        {
            Debug.Log("�г����� 2�� �̻� 10�� ���Ϸ� �Է����ּ���");
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
    /// ��ū�� ��ȿ���� üũ�Ѵ�
    /// ��ȿ�ϴٸ� �� ��ū���� �α����ϸ� ��ȿ���� �ʴٸ� �α����� �� �ְ�
    /// </summary>
    private async void CheckSessionToken()
    {
        bool hasToken = AuthenticationService.Instance.SessionTokenExists;

        if(hasToken)
        {
            Debug.Log("��ū�� �����ֽ��ϴ�");

            #region ��ū�� �ִµ� �α��� �� �Ǿ��ִ� ��� ���� ��ū���� �α���
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
            Debug.Log("��ū�� �����ϴ� �α����ϼ���");
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
        string notify = "�α��ο� �����߽��ϴ�\n ������ �ٽ� �������ּ���";
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

    #region �Խ�Ʈ �α���
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

    #region ����Ƽ �α���
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

    #region ���� �α���
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

    #region �α׾ƿ�
    public void SignOut()
    {
        // �����͸� �����ϰ� ���� ���� �� ������ ����
        GameManager.Instance.Save();

        string notify = "�α׾ƿ� �Ǿ����ϴ�\n ������ �ٽ� �������ּ���";
        ShowWarning(notify);

        AuthenticationService.Instance.SignOut(true);
    }

    #endregion
}
