using System;
using UnityEngine;

[Serializable]
public class PlayerData
{
    [SerializeField, HideInInspector] public int BadgePoints;
    [SerializeField] private int _level = 1;
    [SerializeField] private long _gold;
    [SerializeField] private int _levelProgress = 0;
    [SerializeField] private int _maxLevelProgress = 10;
    [SerializeField] private bool _isReturningPlayer;
    [SerializeField] private int _bossCountdownTime = 30;
    [SerializeField] private bool _needToIncreaseLevel = true;
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

    public void SaveData(string fileName)
    {
        LastTimeInGame = DateTime.Now;
        FileOperations.Serialize(this, fileName);
    }

    public void Reset()
    {
        BossCountdownTime = 30;
        Gold = 0;
        Level = 1;
        LevelProgress = 0;
        MaxLevelProgress = 10;
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
        JsonDateTime jsonTime = new JsonDateTime();
        jsonTime.value = dateTime.ToFileTimeUtc();
        return jsonTime;
    }
}
