using System;
using UnityEngine;

[CreateAssetMenu(menuName ="PlayerData")]
public class PlayerData : ScriptableObject
{
    [SerializeField, HideInInspector] public int BadgePoints;
    [SerializeField] private int _level;
    [SerializeField] private int _gold;
    [SerializeField] private int _levelProgress;
    [SerializeField] private int _maxLevelProgress;
    [SerializeField] private bool _isReturningPlayer;
    [SerializeField] private int _bossCountdownTime;
    [SerializeField] private bool _needToIncreaseLevel;
    [SerializeField] private int _damageBonus;
    private DateTime _lastTimeInGame;

    public event Action<int> LevelChanged;
    public event Action<int> GoldChanged;
    public event Action<int> LevelProgressChanged;
    public event Action<bool> NeedToIncreaseLevelChanged;

    public int Level { get => _level; set { _level = value; LevelChanged?.Invoke(_level); } }
    public int Gold { get => _gold; set { _gold = value; GoldChanged?.Invoke(_gold); } }
    public int LevelProgress { get => _levelProgress; set { _levelProgress = value; LevelProgressChanged?.Invoke(_levelProgress); } }
    public int MaxLevelProgress { get => _maxLevelProgress; set => _maxLevelProgress = value; }
    public bool IsReturningPlayer { get => _isReturningPlayer; set => _isReturningPlayer = value; }
    public int BossCountdownTime { get => _bossCountdownTime; set => _bossCountdownTime = value; }
    public bool NeedToIncreaseLevel { get => _needToIncreaseLevel; set { _needToIncreaseLevel = value; NeedToIncreaseLevelChanged?.Invoke(_needToIncreaseLevel); } }
    public int DamageBonus { get => _damageBonus; set => _damageBonus = value; }
    public DateTime LastTimeInGame { get => _lastTimeInGame; set => _lastTimeInGame = value; }
}
