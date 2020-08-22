﻿using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class AutomationsData
{
    [SerializeField] private List<Automation> _automations = new List<Automation>();
    [SerializeField] private long _clickPower = 1;
    [SerializeField] private long _automationsPower;
    [SerializeField] private bool _canUpgradeSomething;
    [SerializeField] private int _levelsToUpgrade = 1;
    [SerializeField] private float _clickPowerPercentageIncrease;
    [SerializeField] private float _automationsPowerPercentageIncrease;
    [SerializeField] private float _clickPowerCriticalHitChance;
    [SerializeField] private float _criticalPowerIncreasePercentage;

    public event Action<long> ClickPowerChanged;
    public event Action<long> AutomationsPowerChanged;
    public event Action<bool> CanUpgradeSomethingChanged;
    public event Action<int> LevelsToUpgradeChanged;
    public event Action<float> ClickPowerPercentageChanged;
    public event Action<float> AutomationsPowerPercentageChanged;

    private int _automationIndex = 0;
    public long ClickPower { get => _clickPower; set { _clickPower = value; ClickPowerChanged?.Invoke(_clickPower); } }
    public long AutomationsPower { get => _automationsPower; set { _automationsPower = value; AutomationsPowerChanged?.Invoke(_automationsPower); } }
    public bool CanUpgradeSomething { get => _canUpgradeSomething; set { _canUpgradeSomething = value; CanUpgradeSomethingChanged?.Invoke(_canUpgradeSomething); } }
    public int LevelsToUpgrade { get => _levelsToUpgrade; set { _levelsToUpgrade = value; LevelsToUpgradeChanged?.Invoke(_levelsToUpgrade); } }
    public int AutomationIndex => _automationIndex;
    public List<Automation> Automations => _automations;

    public float ClickPowerPercentageIncrease
    {
        get => _clickPowerPercentageIncrease;
        set
        {
            _clickPowerPercentageIncrease = value;
            ClickPowerPercentageChanged?.Invoke(value);
        }
    }
    public float AutomationsPowerPercentageIncrease
    {
        get => _automationsPowerPercentageIncrease;
        set
        {
            _automationsPowerPercentageIncrease = value;
            AutomationsPowerPercentageChanged?.Invoke(value);
        }
    }
    public float ClickPowerCriticalHitChance { get => _clickPowerCriticalHitChance; set => _clickPowerCriticalHitChance = value; }
    public float CriticalPowerIncreasePercentage { get => _criticalPowerIncreasePercentage; set => _criticalPowerIncreasePercentage = value; }

    public int GetLastUnlockedAutomationId()
    {
        for (int i = _automations.Count - 1; i >= 0; i--)
        {
            if (_automations[i].IsUnlocked)
                return i;
        }

        throw new ArgumentNullException("Нет открытых автомаций! Должна быть как минимум одна!");
    }

    public void Reset(DefaultAutomationsData defaultAutomations)
    {
        ClickPower = 1;
        AutomationsPower = 0;
        ClickPowerPercentageIncrease = 0;
        AutomationsPowerPercentageIncrease = 0;
        CanUpgradeSomething = false;
        CriticalPowerIncreasePercentage = 0;
        ClickPowerCriticalHitChance = 0;
        for (int i = 0; i < _automations.Count; i++)
            _automations[i].Reset(defaultAutomations.Automations[i]);
    }

    public void FireAllChangedEvents()
    {
        ClickPowerChanged?.Invoke(_clickPower);
        AutomationsPowerChanged?.Invoke(_automationsPower);
        CanUpgradeSomethingChanged?.Invoke(_canUpgradeSomething);
        LevelsToUpgradeChanged?.Invoke(_levelsToUpgrade);
        ClickPowerPercentageChanged?.Invoke(_clickPowerPercentageIncrease);
        AutomationsPowerPercentageChanged?.Invoke(_automationsPowerPercentageIncrease);
    }
}
