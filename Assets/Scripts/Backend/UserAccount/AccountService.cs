using System.Collections;
using System.Collections.Generic;
using Unity.Services.Authentication;
using Unity.Services.Authentication.PlayerAccounts;
using Unity.Services.Core;
using UnityEngine;

public class AccountService : MonoBehaviour
{
    async void Awake()
    {
        await UnityServices.InitializeAsync();

        PlayerAccountService.Instance.SignedIn += SignInWithUnity;
    }

    private void OnDisable()
    {
        PlayerAccountService.Instance.SignedIn -= SignInWithUnity;
    }

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
        if(AuthenticationService.Instance.IsSignedIn)
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




}
