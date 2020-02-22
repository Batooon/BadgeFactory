using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEditor;

//Developer: Antoshka
[Serializable]
public class Save
{
    public int[] date = new int[6];
    public bool isNewPlayer = true;
}

public class PlayerData : MonoBehaviour
{
    Save saveData = new Save();

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
    private EnemyDataVariable _currentEnemy;

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
        _dpsText.text = Mathf.Round(_dps.Value).ConvertValue();
        _clickPowerText.text = Mathf.Round(_clickPower.Value).ConvertValue();
        _goldText.text = Mathf.Round(_goldAmount.Value).ConvertValue();

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

    private void Start()
    {
        SerializeData();
        CalculateAbsenseTime();
    }

    private void CalculateAbsenseTime()
    {
        if (!saveData.isNewPlayer)
        {
            DateTime lastTimeVisited = new DateTime(saveData.date[0], saveData.date[1], saveData.date[2],
                saveData.date[3], saveData.date[4], saveData.date[5]);
            TimeSpan timeDifference = DateTime.Now - lastTimeVisited;

            float gainedGold = ((int)timeDifference.TotalSeconds * _dps.Value) / _currentEnemy.EnemyDataVar.Hp;
            _goldAmount.Variable.ApplyChange(gainedGold);
            _goldText.text = _goldAmount.Value.ConvertValue();
            print($"Вы отсутствовали {timeDifference.TotalSeconds}, и получили {gainedGold} золота");
        }
    }

    private void OnApplicationPause(bool pause)
    {
        if (pause)
        {
            RememberDate();
        }
    }

    private void OnApplicationQuit()
    {
        RememberDate();
    }

    private void RememberDate()
    {/*
        saveData.date[0] = DateTime.Now.Year;
        saveData.date[1] = DateTime.Now.Month;
        saveData.date[2] = DateTime.Now.Day;
        saveData.date[3] = DateTime.Now.Hour;
        saveData.date[4] = DateTime.Now.Minute;
        saveData.date[5] = DateTime.Now.Second;
        saveData.isNewPlayer = false;*/
        PlayerPrefs.SetInt("YEAR", DateTime.Now.Year);
        PlayerPrefs.SetInt("MONTH", DateTime.Now.Month);
        PlayerPrefs.SetInt("DAY", DateTime.Now.Day);
        PlayerPrefs.SetInt("HOUR", DateTime.Now.Hour);
        PlayerPrefs.SetInt("MINUTE", DateTime.Now.Minute);
        PlayerPrefs.SetInt("SECOND", DateTime.Now.Second);
        PlayerPrefs.SetInt("NEWPLAYER", 0);
    }

    private void SerializeData()
    {
        saveData.date[0] = PlayerPrefs.GetInt("YEAR", 0);
        saveData.date[1] = PlayerPrefs.GetInt("MONTH", 0);
        saveData.date[2] = PlayerPrefs.GetInt("DAY", 0);
        saveData.date[3] = PlayerPrefs.GetInt("HOUR", 0);
        saveData.date[4] = PlayerPrefs.GetInt("MINUTE", 0);
        saveData.date[5] = PlayerPrefs.GetInt("SECOND", 0);
        saveData.isNewPlayer = Convert.ToBoolean(PlayerPrefs.GetInt("NEWPLAYER", 1));
    }

    public void CoinCollected(Coin coin)
    {
        _goldAmount.Variable.ApplyChange(coin.Cost);
        _goldText.text = Mathf.Round(_goldAmount.Value).ConvertValue();

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
        _dpsText.text = Mathf.Round(_dps.Value).ConvertValue();
        _clickPowerText.text = Mathf.Round(_clickPower.Value).ConvertValue();
        _goldText.text = Mathf.Round(_goldAmount.Value).ConvertValue();
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
