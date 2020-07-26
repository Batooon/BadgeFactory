using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName ="AutomationsData")]
public class AutomationsData : ScriptableObject
{
    [SerializeField, HideInInspector] private List<Automation> _automations;

    [SerializeField] private int _clickPower;
    [SerializeField] private int _automationsPower;
    [SerializeField] private bool _canUpgradeSomething;
    [SerializeField] private int _levelsToUpgrade;
    [SerializeField] private Automation _automation;

    private int _automationIndex = 0;

    public event Action<int> ClickPowerChanged;
    public event Action<int> AutomationsPowerChanged;
    public event Action<bool> CanUpgradeSomethingChanged;
    public event Action<int> LevelsToUpgradeChanged;

    public int ClickPower { get => _clickPower; set { _clickPower = value; ClickPowerChanged?.Invoke(_clickPower); } }
    public int AutomationsPower { get => _automationsPower; set { _automationsPower = value; AutomationsPowerChanged?.Invoke(_automationsPower); } }
    public bool CanUpgradeSomething { get => _canUpgradeSomething; set { _canUpgradeSomething = value; CanUpgradeSomethingChanged?.Invoke(_canUpgradeSomething); } }
    public int LevelsToUpgrade { get => _levelsToUpgrade; set { _levelsToUpgrade = value; LevelsToUpgradeChanged?.Invoke(_levelsToUpgrade); } }
    public int AutomationIndex => _automationIndex;
    public List<Automation> Automations => _automations;

    public void AddAutomation()
    {
        if (_automations == null)
            _automations = new List<Automation>();

        _automation = new Automation();
        _automations.Add(_automation);
        _automationIndex = _automations.Count - 1;
    }

    public void RemoveAutomation()
    {
        if (_automationIndex > 0)
        {
            _automation = _automations[--_automationIndex];
            _automations.RemoveAt(++_automationIndex);
        }
        else
        {
            _automations.Clear();
            _automation = null;
        }
    }

    public Automation GetNext()
    {
        if (_automationIndex < _automations.Count - 1)
            _automationIndex++;

        _automation = this[_automationIndex];
        return _automation;
    }

    public Automation GetPrevious()
    {
        if (_automationIndex > 0)
            _automationIndex--;

        _automation = this[_automationIndex];
        return _automation;
    }

    public void ClearAutomations()
    {
        _automations.Clear();
        _automations.Add(new Automation());
        _automation = _automations[0];
        _automationIndex = 0;
    }

    public int GetLastUnlockedAutomationId()
    {
        for (int i = _automations.Count-1; i >= 0; i--)
        {
            if (_automations[i].IsUnlocked)
                return i;
        }

        throw new ArgumentNullException("Нет открытых автомаций! Должна быть как минимум одна!");
    }

    public Automation this[int index]
    {
        get
        {
            if (_automations != null && _automationIndex >= 0 && _automationIndex < _automations.Count)
                return _automations[index];
            return null;
        }
        set
        {
            if (_automations == null)
                _automations = new List<Automation>();

            if (_automationIndex >= 0 && _automationIndex < _automations.Count && value != null)
                _automations[index] = value;
            else
                Debug.LogError("Выход за границы массива или переданное значение null");
        }
    }
}
