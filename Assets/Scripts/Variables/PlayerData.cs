using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

//Developer: Antoshka

public class PlayerData : MonoBehaviour
{
    [SerializeField]
    private List<AutomationBase> _automations = new List<AutomationBase>();

    [SerializeField]
    private FloatReference _dps;
    [SerializeField]
    private TextMeshProUGUI _dpsText;

    [SerializeField]
    private FloatReference _clickPower;
    [SerializeField]
    private TextMeshProUGUI _clickPowerText;

    [SerializeField]
    private FloatReference _goldAmount;
    [SerializeField]
    private TextMeshProUGUI _goldText;

    [SerializeField]
    private UpgradeLevelsAmount _levelsAmountToUpgradeController;

    public void SerializeAutomations()
    {
        TextAsset _excelData = Resources.Load<TextAsset>("AutomationsData");

        string[] data = _excelData.text.Split('\n');

        for (int i = 1, j = 0; i < data.Length - 1 && j < _automations.Count; i++, j++)
        {
            string[] row = data[i].Split(';');

            _automations[j].Name = row[0];
            _automations[j]._startingCost = float.Parse(row[1]);
            _automations[j]._startingDps = float.Parse(row[2]);
        }
    }

    public void CalculateDps(AutomationBase automation)
    {
        RecalculateDps();
        _dpsText.text = Mathf.Round(_dps.Value).ToString();
        _clickPowerText.text = Mathf.Round(_clickPower.Value).ToString();
        _goldText.text = Mathf.Round(_goldAmount.Value).ToString();

        foreach (var item in _automations)
        {
            if (item.gameObject.activeInHierarchy)
                item.CompareCost();
        }
    }

    private void RecalculateDps()
    {
        float _currentDps = 0f;
        float _currentClickPower = 0f;
        foreach (var item in _automations)
        {
            if(item is ClickPower)
            {
                _currentClickPower += item.GetDps();
                continue;
            }
            _currentDps += item.GetDps();
        }
        _dps.Variable.SetValue(_currentDps);
        _clickPower.Variable.SetValue(_currentClickPower);
    }

    private void Awake()
    {
        SerializeAutomations();
        Init();
    }

    public void CoinCollected(Coin coin)
    {
        _goldAmount.Variable.ApplyChange(coin.Cost);
        _goldText.text = Mathf.Round(_goldAmount.Value).ToString();

        foreach (var item in _automations)
        {
            if (item.gameObject.activeInHierarchy)
                item.CompareCost();
        }
    }

    private void Init()
    {
        for (int i = 0; i < _automations.Count; i++)
        {
            _automations[i].Subscribe();
            _automations[i].Upgrade = CalculateDps;
            _automations[i].Init();
            _levelsAmountToUpgradeController.UpgradeLevelsAmountChanged += _automations[i].RecalculateCostToLevelsAmount;
        }
        _dps.Variable.SetValue(PlayerPrefs.GetFloat("DPS", 0));
        _clickPower.Variable.SetValue(PlayerPrefs.GetFloat("CLICKPOWER", 1));
        _goldAmount.Variable.SetValue(PlayerPrefs.GetFloat("GOLD", 100000));
        _dpsText.text = Mathf.Round(_dps.Value).ToString();
        _clickPowerText.text = Mathf.Round(_clickPower.Value).ToString();
        _goldText.text = Mathf.Round(_goldAmount.Value).ToString();
    }

    private void OnDisable()
    {
        for (int i = 0; i < _automations.Count; i++)
        {
            _automations[i].Unsubscibe();
            _automations[i].Upgrade = null;
        }
        PlayerPrefs.SetFloat("DPS", _dps.Value);
        PlayerPrefs.SetFloat("CLICKPOWER", _clickPower.Value);
        PlayerPrefs.SetFloat("GOLD", _goldAmount.Value);
    }
}
