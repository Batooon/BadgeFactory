using Localization;
using System;
using TMPro;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

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

    public void SwitchVolumeOnOff(bool offMusic)
    {
        _musicMixer.SetFloat(_volumeParam, offMusic ? _muted : _unmuted);
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
    //private TMP_Dropdown _dropdown;
    //private ToggleGroup _toggleGroup;

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
    [SerializeField] private TMP_Dropdown _languageDropdown;

    [SerializeField] private string _musicVolumeParam;
    [SerializeField] private string _soundVolumeParam;
    [SerializeField] private ToggleGroup _toggleGroup;

    private VolumeSetter _musicSwitcher;
    private VolumeSetter _soundsSwitcher;
    private ILanguageSetter _languageSetter;
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
        _settingsData = settingsData;
        _musicSwitcher = new VolumeSetter(_mixer, _musicVolumeParam);
        _soundsSwitcher = new VolumeSetter(_mixer, _soundVolumeParam);
        _languageSetter = new LocalizationDropdownSetter(_languages);
        _languageDropdown.value = Array.BinarySearch(_languages, _currentLanguage);
        _languageDropdown.RefreshShownValue();
        _settingsPresentation.Init(_settingsData);

        _settingsData.MusicChanged += _musicSwitcher.SwitchVolumeOnOff;
        _settingsData.SoundChanged += _soundsSwitcher.SwitchVolumeOnOff;
    }

    private void Start()
    {
        _musicSwitcher.SwitchVolumeOnOff(_settingsData.MusicOff);
        _soundsSwitcher.SwitchVolumeOnOff(_settingsData.SoundOff);
    }

    private void OnEnable()
    {
        if (_settingsData == null)
            return;

        _settingsData.MusicChanged += _musicSwitcher.SwitchVolumeOnOff;
        _settingsData.SoundChanged += _soundsSwitcher.SwitchVolumeOnOff;

        _musicSwitcher.SwitchVolumeOnOff(_settingsData.MusicOff);
        _soundsSwitcher.SwitchVolumeOnOff(_settingsData.SoundOff);
    }

    private void OnDisable()
    {
        _settingsData.MusicChanged -= _musicSwitcher.SwitchVolumeOnOff;
        _settingsData.SoundChanged -= _soundsSwitcher.SwitchVolumeOnOff;
    }

    public void OnOffMusic(bool isMusicOff)
    {
        _settingsData.MusicOff = isMusicOff;
        //_musicSwitcher.SwitchVolumeOnOff(isMusicOff);
    }

    public void OnOffSounds(bool isSoundsOff)
    {
        _settingsData.SoundOff = isSoundsOff;
        //_soundsSwitcher.SwitchVolumeOnOff(isSoundsOff);
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
