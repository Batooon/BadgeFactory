using System;
using UnityEngine;

[CreateAssetMenu(menuName ="Settings Data")]
public class SettingsData : ScriptableObject
{
    [SerializeField] private bool _vibrationOff;
    [SerializeField] private bool _soundOff;
    [SerializeField] private bool _musicOff;

    public event Action<bool> VibrationChanged;
    public event Action<bool> SoundChanged;
    public event Action<bool> MusicChanged;

    public bool VibrationOff
    {
        get => _vibrationOff;
        set
        {
            _vibrationOff = value;
            VibrationChanged?.Invoke(value);
        }
    }
    public bool SoundOff
    {
        get => _soundOff;
        set
        {
            _soundOff = value;
            SoundChanged?.Invoke(value);
        }
    }
    public bool MusicOff
    {
        get => _musicOff;
        set
        {
            _musicOff = value;
            MusicChanged?.Invoke(value);
        }
    }

}
