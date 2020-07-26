using System.Collections.Generic;
using TMPro;
using UnityEngine;

//Developer: Antoshka
public class UpgradeLevelsAmount : MonoBehaviour
{
    [SerializeField] private List<int> _levels = new List<int>();
    [SerializeField] private string _template = "x{0}";
    [SerializeField] private TextMeshProUGUI _levelsText;

    private AutomationsData _automationsData;
    private int _maxIndex;
    private int _currentLevelIndex;

    public void Init(AutomationsData automationsData)
    {
        _automationsData = automationsData;
        _maxIndex = _levels.Count - 1;
        _currentLevelIndex = _levels.IndexOf(_automationsData.LevelsToUpgrade);

        if (_currentLevelIndex > _maxIndex)
            _currentLevelIndex = _maxIndex;

        _levelsText.text = string.Format(_template, _levels[_currentLevelIndex]);
    }

    public void ChangeAmountOfLevelsToUpgrade()
    {
        if (_currentLevelIndex == _maxIndex)
            _currentLevelIndex = 0;
        else
            _currentLevelIndex += 1;

        _levelsText.text = string.Format(_template, _levels[_currentLevelIndex]);
        _automationsData.LevelsToUpgrade = _levels[_currentLevelIndex];
    }
}
