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
    [SerializeField] private AudioMixer _mixer;
    [SerializeField] private TMP_Dropdown _languageDropdown;

    [SerializeField] private string _musicVolumeParam;
    [SerializeField] private string _soundVolumeParam;
    [SerializeField] private ToggleGroup _toggleGroup;

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

    public void OnOffMusic(bool offMusic)
    {
        _musicSwitcher.SwitchVolumeOnOff(offMusic);
    }

    public void OnOffSounds(bool offSounds)
    {
        _soundsSwitcher.SwitchVolumeOnOff(offSounds);
    }

    public void ChangeLanguage(int dropdownIndex)
    {
        _languageSetter.ChangeLanguage(dropdownIndex);
    }
}
