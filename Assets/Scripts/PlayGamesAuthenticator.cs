using UnityEngine;
using UnityEngine.UI;
using GooglePlayGames;
using TMPro;

public class PlayGamesAuthenticator : MonoBehaviour
{
    [SerializeField]
    private string _signedInText;
    [SerializeField]
    private string _signedOutText;
    [SerializeField]
    private Toggle __googlePlayToggle;
    [SerializeField]
    private TextMeshProUGUI _googlePlayGamesText;

    private void Awake()
    {
        UpdateText(PlayGamesPlatform.Instance.IsAuthenticated());

        __googlePlayToggle.isOn = PlayGamesPlatform.Instance.IsAuthenticated();
        __googlePlayToggle.onValueChanged.AddListener(OnTogglePressed);
    }

    private void OnTogglePressed(bool isAuthenticated)
    {
        if (isAuthenticated)
            PlayGames.AuthenticateUser(UpdateText);
        else
            PlayGames.LogOut();
    }

    private void UpdateText(bool isAuthenticated)
    {
        _googlePlayGamesText.text = isAuthenticated ? _signedInText : _signedOutText;
    }
}
