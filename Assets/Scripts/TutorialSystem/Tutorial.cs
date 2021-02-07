using UnityEngine;
using UnityEngine.Events;

public class Tutorial : MonoBehaviour, IObserver
{
    [SerializeField] private UnityEvent _progressResetterUnlocked;
    [SerializeField] private long _levelToUnlcokProgressReset;

    private PlayerData _playerData;

    public void Init(PlayerData playerData)
    {
        _playerData = playerData;
    }

    private void OnEnable()
    {
        _playerData.Attach(this);
        OnLevelChanged(_playerData.Level);
    }

    private void OnDisable()
    {
        _playerData.Detach(this);
    }

    private void OnLevelChanged(int newLevel)
    {
        if (newLevel == _levelToUnlcokProgressReset)
        {
            _progressResetterUnlocked?.Invoke();
        }
    }

    public void Fetch(ISubject subject)
    {
        PlayerData playerData = subject as PlayerData;
        OnLevelChanged(playerData.Level);
    }
}
