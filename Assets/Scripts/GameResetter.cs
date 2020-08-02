using UnityEngine;
using UnityEngine.Events;

public class GameResetter : MonoBehaviour
{
    [SerializeField] private UnityEvent ResetGameEvent;
    private ProgressResetter _progressResetter;

    public void Init(
        PlayerData playerData,
        AutomationsData automationsData,
        BadgeData badgeData)
    {
        _progressResetter = new ProgressResetter(playerData,
                                                 badgeData,
                                                 automationsData);
    }

    public void ResetGame()
    {
        StopAllCoroutines();
        ResetGameEvent?.Invoke();
        _progressResetter.ResetProgress();
    }
}
