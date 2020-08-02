using System;
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

    private int _automationIndex = 0;

    public event Action<long> ClickPowerChanged;
    public event Action<long> AutomationsPowerChanged;
    public event Action<bool> CanUpgradeSomethingChanged;
    public event Action<int> LevelsToUpgradeChanged;

    public long ClickPower { get => _clickPower; set { _clickPower = value; ClickPowerChanged?.Invoke(_clickPower); } }
    public long AutomationsPower { get => _automationsPower; set { _automationsPower = value; AutomationsPowerChanged?.Invoke(_automationsPower); } }
    public bool CanUpgradeSomething { get => _canUpgradeSomething; set { _canUpgradeSomething = value; CanUpgradeSomethingChanged?.Invoke(_canUpgradeSomething); } }
    public int LevelsToUpgrade { get => _levelsToUpgrade; set { _levelsToUpgrade = value; LevelsToUpgradeChanged?.Invoke(_levelsToUpgrade); } }
    public int AutomationIndex => _automationIndex;
    public List<Automation> Automations => _automations;

    public int GetLastUnlockedAutomationId()
    {
        for (int i = _automations.Count - 1; i >= 0; i--)
        {
            if (_automations[i].IsUnlocked)
                return i;
        }

        throw new ArgumentNullException("Нет открытых автомаций! Должна быть как минимум одна!");
    }
}
