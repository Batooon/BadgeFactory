using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

//Developer: Antoshka
[RequireComponent(typeof(Button))]
public class UpgradeButton : MonoBehaviour
{
    private const float _multiplier = 1.15f;
    private const float _startingCost = 50f;

    [SerializeField]
    private GameEvent _clickPowerUpgraded;
    [SerializeField]
    private TextMeshProUGUI _upgradeCostText;
    [SerializeField]
    private FloatReference _upgradeCost;
    [SerializeField]
    private IntReference _level;
    [SerializeField]
    private FloatReference _score;

    private Button _button;

    private void OnEnable()
    {
        _button = GetComponent<Button>();
        _button.onClick.AddListener(UpgradeClickPower);
    }

    private void OnDisable()
    {
        _button.onClick.RemoveListener(UpgradeClickPower);
    }

    public void Init()
    {
        _upgradeCostText = GetComponentInChildren<TextMeshProUGUI>();
        _upgradeCostText.text = $"Upgrade ₴{_upgradeCost.Variable.Value}";
        CompareScoreValue();
    }

    public void CompareScoreValue()
    {
        _button.interactable = _score >= _upgradeCost;
    }

    public void UpgradeClickPower()
    {
        _upgradeCost.Variable.SetValue(_startingCost * Mathf.Pow(_multiplier, _level));
        _upgradeCostText.text = $"Upgrade ₴{_upgradeCost.Variable.Value}";
        _level.Variable.SetValue(_level.Value + 1);
        _clickPowerUpgraded.Raise();
        CompareScoreValue();
    }
}
