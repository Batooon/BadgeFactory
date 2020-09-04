using UnityEngine;
using UnityEngine.UI;

public class SettingsPresentation : MonoBehaviour
{
    [SerializeField] private Toggle _musicToggle;
    [SerializeField] private Toggle _soundToggle;
    [SerializeField] private Toggle _vibrationToggle;

    public void Init(SettingsData settingsData)
    {
        _musicToggle.isOn = settingsData.MusicOff;
        _soundToggle.isOn = settingsData.SoundOff;
        _vibrationToggle.isOn = settingsData.VibrationOff;
    }
}
