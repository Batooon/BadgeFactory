using UnityEngine;

public class Vibration : MonoBehaviour
{
    private SettingsData _settingsData;

    public void Init(SettingsData settingsData)
    {
        _settingsData = settingsData;
    }

    public void Vibrate()
    {
        if (_settingsData.VibrationOff)
            return;

        Handheld.Vibrate();
    }
}
