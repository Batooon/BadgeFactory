using UnityEngine;
using UnityEngine.UI;
using Localization;
using TMPro;

//Developer: Antoshka
public enum Languages
{
    Ukrainian,
    English,
    Russian
}

public class LanguageChanger : MonoBehaviour
{
    //private Languages _currentLanguage;

    public TMP_Dropdown LanguageSwitcher;

    private int _currentLanguageIndex;

    public void ChangeLanguage()
    {
        switch (LanguageSwitcher.value)
        {
            case 0:
                LocalizationService.Instance.Localization = "Ukrainian";
                break;
            case 1:
                LocalizationService.Instance.Localization = "English";
                break;
            case 2:
                LocalizationService.Instance.Localization = "Russian";
                break;
        }
    }

    private void Start()
    {
        _currentLanguageIndex = PlayerPrefs.GetInt("Language", 0);
        LanguageSwitcher.value = _currentLanguageIndex;
        ChangeLanguage();
    }

    private void OnApplicationQuit()
    {
        PlayerPrefs.SetInt("Language", LanguageSwitcher.value);
    }
}
