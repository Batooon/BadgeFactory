using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

//Developer: Antoshka
[RequireComponent(typeof(Button))]
public class UpgradeLevelsAmount : MonoBehaviour
{
    public event Action<int> UpgradeLevelsAmountChanged;

    [SerializeField]
    private int _amountOfLevels = 4;
    [SerializeField]
    private List<int> _levels = new List<int>();
    private int _currentIndex;
    private Button _button;
    private TextMeshProUGUI _levelsText;

    private void Awake()
    {
        _currentIndex = PlayerPrefs.GetInt("LEVELSTOUPGRADEINDEX", 0);
        foreach (var item in _levels)
        {
            if (item == 0)
                Debug.LogError("Levels not filled, please solve this problem in the inspector!");
        }

        if (_levels.Count > _amountOfLevels)
        {
            for (int i = _levels.Count; i == _amountOfLevels; i--)
                _levels.RemoveAt(i);
        }
        _button = GetComponent<Button>();
        _button.onClick.AddListener(ChangeAmountOfLevel);
        _levelsText = GetComponentInChildren<TextMeshProUGUI>();
        _levelsText.text = $"x{_levels[_currentIndex]}";
    }

    private void ChangeAmountOfLevel()
    {
        _currentIndex = _currentIndex == _levels.Count - 1 ? 0 : _currentIndex + 1;

        _levelsText.text = $"x{_levels[_currentIndex]}";

        UpgradeLevelsAmountChanged?.Invoke(_levels[_currentIndex]);
    }

    private void OnDisable()
    {
        PlayerPrefs.SetInt("LEVELSTOUPGRADEINDEX", _currentIndex);
    }
}
