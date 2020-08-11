using UnityEngine;
using UnityEngine.UI;
using GooglePlayGames;

public class PlayGamesAuthenticator : MonoBehaviour
{
    [SerializeField] private Image _referenceImage;
    [SerializeField] private Sprite _authenticatedIcon;
    [SerializeField] private Sprite _notAuthenticatedIcon;
    [SerializeField] private Toggle __googlePlayToggle;

    public void Init()
    {
        PlayGames.LoggedIn += UpdateState;
        UpdateState(PlayGamesPlatform.Instance.IsAuthenticated());

        __googlePlayToggle.isOn = PlayGamesPlatform.Instance.IsAuthenticated();
        __googlePlayToggle.onValueChanged.AddListener(OnTogglePressed);
    }

    private void OnDisable()
    {
        PlayGames.LoggedIn -= UpdateState;
    }

    private void OnTogglePressed(bool isAuthenticated)
    {
        if (isAuthenticated)
            PlayGames.AuthenticateUser(UpdateState);
        else
            PlayGames.LogOut();
    }

    private void UpdateState(bool isAuthenticated)
    {
        _referenceImage.sprite = isAuthenticated ? _authenticatedIcon : _notAuthenticatedIcon;
    }
}
