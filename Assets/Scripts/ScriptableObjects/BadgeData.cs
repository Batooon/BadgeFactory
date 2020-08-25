using System;
using UnityEngine;

[Serializable]
public class BadgeData
{
    [SerializeField] private float _currentHp;
    [SerializeField] private long _maxHp;
    [SerializeField] private long _coinsReward;

    public event Action<float> HpChanged;
    public event Action<long> MaxHpChanged;

    public float CurrentHp { get => _currentHp; set { _currentHp = value; HpChanged?.Invoke(_currentHp); } }
    public long MaxHp { get => _maxHp; set { _maxHp = value; MaxHpChanged?.Invoke(_maxHp); } }
    public long CoinsReward { get => _coinsReward; set => _coinsReward = value; }

    private float _startingHp;
    private long _startingMaxHp;
    private long _startingCoinsReward;

    public void Init()
    {
        _currentHp = _startingHp;
        _startingMaxHp = _maxHp;
        _startingCoinsReward = _coinsReward;
    }

    public void FireAllChangedEvents()
    {
        HpChanged?.Invoke(_currentHp);
        MaxHpChanged?.Invoke(_maxHp);
    }

    public void ResetData()
    {
        CurrentHp = _startingHp;
        MaxHp = _startingMaxHp;
        CoinsReward = _startingCoinsReward;
    }
}
