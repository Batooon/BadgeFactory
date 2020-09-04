using UnityEngine;

public class LanguageChanger : MonoBehaviour
{
    [SerializeField] private Settings _settings;
    [SerializeField] private int _languageIndex;

    public int LanguageIndex => _languageIndex;

    public void ChangeLanguage(bool needToChangeThisLanguage)
    {
        if (needToChangeThisLanguage)
            _settings.ChangeLanguage(_languageIndex);
    }
}
