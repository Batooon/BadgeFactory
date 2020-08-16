using UnityEngine;
using UnityEngine.Events;

public class GameResetter : MonoBehaviour
{
    [SerializeField] private UnityEvent ResetGameEvent;
    private ProgressResetter _progressResetter;
    private PlayerData _playerData;
    private BadgeData _badgeData;
    private AutomationsData _automationsData;

    public void Init(
        PlayerData playerData,
        AutomationsData automationsData,
        BadgeData badgeData,
        DefaultAutomationsData defaultAutomations)
    {
        _progressResetter = new ProgressResetter(defaultAutomations);
        _playerData = playerData;
        _automationsData = automationsData;
        _badgeData = badgeData;
    }

    public void ResetGame()
    {
        StopAllCoroutines();
        ResetGameEvent?.Invoke();
        _progressResetter.ResetProgress(ref _playerData, ref _badgeData, ref _automationsData);
    }
}
