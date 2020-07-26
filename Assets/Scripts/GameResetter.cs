using UnityEngine;
using UnityEngine.Events;
using UnityEditor;

public class GameResetter : MonoBehaviour
{
    [SerializeField] private UnityEvent ResetGameEvent;
    private ProgressResetter _progressResetter;

    public void Init(
        PlayerData playerData,
        AutomationsData automationsData,
        BadgeData badgeData,
        PlayerData defaultPlayerData,
        AutomationsData defaultAutomationsData,
        BadgeData defaultBadgeData)
    {
        _progressResetter = new ProgressResetter(defaultPlayerData,
                                                 defaultBadgeData,
                                                 defaultAutomationsData,
                                                 playerData,
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

[CustomEditor(typeof(GameResetter))]
public class GameResetterEditor : Editor
{
    public override void OnInspectorGUI()
    {
        if(GUILayout.Button("Reset Game"))
        {
            GameResetter resetter = (GameResetter)target;
            resetter.ResetGame();
        }

        base.OnInspectorGUI();
    }
}
