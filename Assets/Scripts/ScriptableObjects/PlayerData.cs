using BadgeImplementation.BusinessRules;
using System;
using UnityEngine;

[Serializable]
public class PlayerData
{
    [SerializeField, HideInInspector] public int BadgePoints;
    [SerializeField, HideInInspector] public bool IsProgressResetting;
    [SerializeField] private int _level;
    [SerializeField] private long _gold;
    [SerializeField] private int _levelProgress;
    [SerializeField] private int _maxLevelProgress;
    [SerializeField] private bool _isReturningPlayer;
    [SerializeField] private int _bossCountdownTime;
    [SerializeField] private bool _needToIncreaseLevel;
    [SerializeField] private int _damageBonus;
    [SerializeField] private JsonDateTime _jsonDateTime = new JsonDateTime();

    public event Action<int> LevelChanged;
    public event Action<long> GoldChanged;
    public event Action<int> LevelProgressChanged;
    public event Action<bool> NeedToIncreaseLevelChanged;

    public int Level { get => _level; set { _level = value; LevelChanged?.Invoke(_level); } }
    public long Gold { get => _gold; set { _gold = value; GoldChanged?.Invoke(_gold); } }
    public int LevelProgress { get => _levelProgress; set { _levelProgress = value; LevelProgressChanged?.Invoke(_levelProgress); } }
    public int MaxLevelProgress { get => _maxLevelProgress; set => _maxLevelProgress = value; }
    public bool IsReturningPlayer { get => _isReturningPlayer; set => _isReturningPlayer = value; }
    public int BossCountdownTime { get => _bossCountdownTime; set => _bossCountdownTime = value; }
    public bool NeedToIncreaseLevel { get => _needToIncreaseLevel; set { _needToIncreaseLevel = value; NeedToIncreaseLevelChanged?.Invoke(_needToIncreaseLevel); } }
    public int DamageBonus { get => _damageBonus; set => _damageBonus = value; }
    public DateTime LastTimeInGame { get => _jsonDateTime; set => _jsonDateTime = value; }

    private int _startingLevel;
    private long _startingGold;
    private int _startingBossCountdownTime;
    private int _startingLevelProgress;
    private int _startingMaxLevelProgress;

    public void Init()
    {
        _startingLevel = _level;
        _startingGold = _gold;
        _startingBossCountdownTime = _bossCountdownTime;
        _startingLevelProgress = _levelProgress;
        _startingMaxLevelProgress = _maxLevelProgress;
    }

    public void SaveData(string fileName)
    {
        LastTimeInGame = DateTime.Now;
        FileOperations.Serialize(this, fileName);
    }

    public void FireAllChangedEvents()
    {
        LevelChanged?.Invoke(_level);
        GoldChanged?.Invoke(_gold);
        LevelProgressChanged?.Invoke(_levelProgress);
        NeedToIncreaseLevelChanged?.Invoke(_needToIncreaseLevel);
    }

    public void ResetData()
    {
        Level = _startingLevel;
        Gold = _startingGold;
        BossCountdownTime = _startingBossCountdownTime;
        LevelProgress = _startingLevelProgress;
        MaxLevelProgress = _startingMaxLevelProgress;
    }
}

[Serializable]
public struct JsonDateTime
{
    public long value;

    public static implicit operator DateTime(JsonDateTime jsonDateTime)
    {
        return DateTime.FromFileTimeUtc(jsonDateTime.value);
    }

    public static implicit operator JsonDateTime(DateTime dateTime)
    {
        JsonDateTime jsonTime = new JsonDateTime
        {
            value = dateTime.ToFileTimeUtc()
        };
        return jsonTime;
    }
}
