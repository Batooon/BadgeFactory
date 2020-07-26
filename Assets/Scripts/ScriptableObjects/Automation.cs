using System;
using UnityEngine;

[Serializable]
public class Automation
{
    [SerializeField] private bool _canUpgrade;
    [SerializeField] private int _startingDamage;
    [SerializeField] private int _startingCost;
    [SerializeField] private int _level;
    [SerializeField] private int _currentCost;
    [SerializeField] private int _currentDamage;
    [SerializeField] private bool _isUnlocked;

    public event Action<bool> CanUpgradeChanged;
    public event Action<int> LevelChanged;
    public event Action<int> CostChanged;
    public event Action<int> DamageChanged;
    public event Action<bool> UnlockChanged;

    public bool CanUpgrade
    {
        get => _canUpgrade;
        set
        {
            _canUpgrade = value;
            CanUpgradeChanged?.Invoke(_canUpgrade);
        }
    }
    public int StartingDamage { get => _startingDamage; set => _startingDamage = value; }
    public int StartingCost { get => _startingCost; set => _startingCost = value; }
    public int Level
    {
        get => _level; set
        {
            _level = value;
            LevelChanged?.Invoke(_level);
        }
    }
    public int CurrentCost
    {
        get => _currentCost; set
        {
            _currentCost = value;
            CostChanged?.Invoke(_currentCost);
        }
    }
    public int CurrentDamage
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

    public void RecalculateCost(int levelsToUpgrade)
    {
        float upgradeFactor = 1.07f;
        float costFactor = 1f;

        for (int i = 0; i < Level - 1 + levelsToUpgrade; i++)
            costFactor *= upgradeFactor;
        CurrentCost = (int)(StartingCost * costFactor);
    }
}
