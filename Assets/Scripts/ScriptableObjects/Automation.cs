using System;
using UnityEngine;

[Serializable]
public class Automation
{
    [SerializeField] private bool _canUpgrade;
    [SerializeField] private long _startingDamage;
    [SerializeField] private long _startingCost;
    [SerializeField] private int _level;
    [SerializeField] private long _currentCost;
    [SerializeField] private long _currentDamage;
    [SerializeField] private bool _isUnlocked;
    [SerializeField] private int _powerUpPercentage;

    public event Action<bool> CanUpgradeChanged;
    public event Action<int> LevelChanged;
    public event Action<long> CostChanged;
    public event Action<long> DamageChanged;
    public event Action<bool> UnlockChanged;
    public event Action<int> PowerUpPercentageChanged;

    public bool CanUpgrade
    {
        get => _canUpgrade;
        set
        {
            _canUpgrade = value;
            CanUpgradeChanged?.Invoke(_canUpgrade);
        }
    }
    public long StartingDamage { get => _startingDamage; set => _startingDamage = value; }
    public long StartingCost { get => _startingCost; set => _startingCost = value; }
    public int Level
    {
        get => _level; set
        {
            _level = value;
            LevelChanged?.Invoke(_level);
        }
    }
    public long CurrentCost
    {
        get => _currentCost; set
        {
            _currentCost = value;
            CostChanged?.Invoke(_currentCost);
        }
    }
    public long CurrentDamage
    {
        get => _currentDamage; set
        {
            _currentDamage = value;
            DamageChanged?.Invoke(_currentDamage);
        }
    }
    public bool IsUnlocked
    {
        get => _isUnlocked; set
        {
            _isUnlocked = value;
            UnlockChanged?.Invoke(_isUnlocked);
        }
    }

    public int PowerUpPercentage
    {
        get => _powerUpPercentage;
        set
        {
            PowerUpPercentageChanged?.Invoke(value - _powerUpPercentage);
            _powerUpPercentage = value;
        }
    }
}
