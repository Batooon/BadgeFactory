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
    [SerializeField] private float _powerUpPercentage;
    [SerializeField] private UpgradeComponentData[] _upgradeComponents;
    //[SerializeField] private bool[] _isPowerUpUnlocked;

    public event Action<bool> CanUpgradeChanged;
    public event Action<int> LevelChanged;
    public event Action<long> CostChanged;
    public event Action<long> DamageChanged;
    public event Action<bool> UnlockChanged;
    public event Action<float> PowerUpPercentageChanged;
    public event Action<bool, int> IsPowerUpUnlockedChanged;

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

    public float PowerUpPercentage
    {
        get => _powerUpPercentage;
        set
        {
            float addedPercentage = value - _powerUpPercentage;
            _powerUpPercentage = value;
            PowerUpPercentageChanged?.Invoke(addedPercentage);
        }
    }

    //public bool[] IsPowerUpUnlocked => _isPowerUpUnlocked;

    public UpgradeComponentData[] UpgradeComponents => _upgradeComponents;

    /*public bool this[int index]
    {
        set
        {
            _isPowerUpUnlocked[index] = value;
            IsPowerUpUnlockedChanged?.Invoke(value, index);
        }
    }*/

    public void Reset(Automation defaultAutomation)
    {
        CanUpgrade = false;
        Level = 0;
        CurrentCost = _startingCost;
        CurrentDamage = _startingDamage;
        PowerUpPercentage = 0;
        IsUnlocked = defaultAutomation.IsUnlocked;
    }
}
