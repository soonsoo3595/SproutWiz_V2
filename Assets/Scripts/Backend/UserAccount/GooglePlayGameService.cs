using GooglePlayGames;
using GooglePlayGames.BasicApi;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using Unity.Services.Authentication;
using Unity.Services.Core;
using UnityEngine;

public class GooglePlayGameService : MonoBehaviour
{
    void Awake()
    {
        var config = new PlayGamesClientConfiguration.Builder()
            .RequestServerAuthCode(false)
            .RequestIdToken()
            .Build();

        // GPGS 초기화
        PlayGamesPlatform.InitializeInstance(config);
        PlayGamesPlatform.DebugLogEnabled = true;
        // 구글플레이 게임 활성화
        PlayGamesPlatform.Activate();
    }

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
}
