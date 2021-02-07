using Localization;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class VolumeSetter
{
    private const float Muted = -80f;

    private AudioMixer _musicMixer;
    private string _volumeParam;
    private float _unmuted;

    public VolumeSetter(AudioMixer mixer, string volumeParam)
    {
        _musicMixer = mixer;
        _volumeParam = volumeParam;
        _musicMixer.GetFloat(_volumeParam, out _unmuted);
    }

    public void SwitchVolumeOnOff(bool offMusic)
    {
        _musicMixer.SetFloat(_volumeParam, offMusic ? Muted : _unmuted);
    }
}

public class LocalizationDropdownSetter
{
    private LocalizationService _localizationService;
    private string[] _languages;

    public LocalizationDropdownSetter(string[] languages)
    {
        _localizationService = LocalizationService.Instance;
        _languages = languages;
    }

    public void ChangeLanguage(int languageIndex)
    {
        try
        {
            _localizationService.Localization = _languages[languageIndex];
        }
        catch(Exception)
        {
            Debug.LogError("В компоненте Dropdown содержится больше языков, чем в базе данных локализации!");
        }
    }
}

public class Settings : MonoBehaviour
{
    [SerializeField] private SettingsPresentation _settingsPresentation;
    [SerializeField] private AudioMixer _mixer;
    [SerializeField] private List<Toggle> _toggleLanguages;

    private VolumeSetter _musicSwitcher;
    private VolumeSetter _soundsSwitcher;
    private LocalizationDropdownSetter _languageSetter;
    private SettingsData _settingsData;

    private string[] _languages =
    {
        "Russian",
        "English",
        "Ukrainian"
    };
    private string _currentLanguage;

    public void Init(SettingsData settingsData)
    {
        _currentLanguage = LocalizationService.Instance.Localization;
        _settingsData = settingsData;

        _musicSwitcher = new VolumeSetter(_mixer, Config.MusicVolumeParam);
        _soundsSwitcher = new VolumeSetter(_mixer, Config.SoundsVolumeParam);

        _languageSetter = new LocalizationDropdownSetter(_languages);
        int currentLanguageIndex = Array.BinarySearch(_languages, _currentLanguage);
        for (int i = 0; i < _toggleLanguages.Count; i++)
        {
            LanguageChanger languageChanger = _toggleLanguages[i].GetComponent<LanguageChanger>();
            if (languageChanger != null)
                _toggleLanguages[i].isOn = languageChanger.LanguageIndex == currentLanguageIndex;
        }
        _settingsPresentation.Init(_settingsData);
        ChangeLanguage(currentLanguageIndex);
    }

    private void Start()
    {
        _musicSwitcher.SwitchVolumeOnOff(_settingsData.MusicOff);
        _soundsSwitcher.SwitchVolumeOnOff(_settingsData.SoundOff);
    }

    public void OnOffMusic(bool isMusicOff)
    {
        _settingsData.MusicOff = isMusicOff;
        _musicSwitcher.SwitchVolumeOnOff(isMusicOff);
    }

    public void OnOffSounds(bool isSoundsOff)
    {
        _settingsData.SoundOff = isSoundsOff;
        _soundsSwitcher.SwitchVolumeOnOff(isSoundsOff);
    }

    public void OnOffVibration(bool isVibrationOff)
    {
        _settingsData.VibrationOff = isVibrationOff;
    }

    public void ChangeLanguage(int dropdownIndex)
    {
        _languageSetter.ChangeLanguage(dropdownIndex);
    }
}

public static class Config
{
    public static readonly string MusicVolumeParam = "_musicVolume";
    public static readonly string SoundsVolumeParam = "_soundsVolume";
}
