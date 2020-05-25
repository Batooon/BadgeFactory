using Localization;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Audio;

public class VolumeSetter
{
    private const float _muted = -80f;
    private const float _unmuted = 0f;

    private AudioMixer _musicMixer;
    private string _volumeParam;

    public VolumeSetter(AudioMixer mixer, string volumeParam)
    {
        _musicMixer = mixer;
        _volumeParam = volumeParam;
    }

    public void SwitchVolumeOnOff(bool playMusic)
    {
        _musicMixer.SetFloat(_volumeParam, playMusic ? _unmuted : _muted);
    }
}

public interface ILanguageSetter
{
    void ChangeLanguage(int languageIndex);
}

public class LocalizationDropdownSetter : ILanguageSetter
{
    private LocalizationService _localizationService;
    private string[] _languages;
    private TMP_Dropdown _dropdown;

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
    [SerializeField]
    private AudioMixer _mixer;
    [SerializeField]
    private TMP_Dropdown _languageDropdown;

    [SerializeField]
    private string _musicVolumeParam;
    [SerializeField]
    private string _soundVolumeParam;

    private VolumeSetter _musicSwitcher;
    private VolumeSetter _soundsSwitcher;
    private ILanguageSetter _languageSetter;

    private string[] _languages =
    {
        "Russian",
        "English",
        "Ukrainian"
    };
    private string _currentLanguage;

    private void Awake()
    {
        _musicSwitcher = new VolumeSetter(_mixer, _musicVolumeParam);
        _soundsSwitcher = new VolumeSetter(_mixer, _soundVolumeParam);
        _languageSetter = new LocalizationDropdownSetter(_languages);
    }

    private void Start()
    {
        _languageDropdown.value = Array.BinarySearch(_languages, _currentLanguage);
        _languageDropdown.RefreshShownValue();
    }

    public void OnOffMusic(bool playMusic)
    {
        _musicSwitcher.SwitchVolumeOnOff(playMusic);
    }

    public void OnOffSounds(bool playSounds)
    {
        _soundsSwitcher.SwitchVolumeOnOff(playSounds);
    }

    public void ChangeLanguage(int dropdownIndex)
    {
        _languageSetter.ChangeLanguage(dropdownIndex);
    }
}
