using TMPro;
using Unity.Services.Authentication;
using UnityEngine;
using UnityEngine.UI;

public class AccountInfo : MonoBehaviour
{
    public TextMeshProUGUI playerName;
    public TextMeshProUGUI playerID;
    public Button signOut;

    private void OnEnable()
    {
        playerName.text = AuthenticationService.Instance.PlayerName;
        playerID.text = AuthenticationService.Instance.PlayerId;
    }

}
