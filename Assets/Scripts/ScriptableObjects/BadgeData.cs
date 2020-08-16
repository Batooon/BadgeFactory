using System;
using UnityEngine;

[Serializable]
public class BadgeData
{
    [SerializeField] private float _currentHp;
    [SerializeField] private long _maxHp = 10;
    [SerializeField] private long _coinsReward = 1;

    public event Action<float> HpChanged;
    public event Action<long> MaxHpChanged;

    public float CurrentHp { get => _currentHp; set { _currentHp = value; HpChanged?.Invoke(_currentHp); } }
    public long MaxHp { get => _maxHp; set { _maxHp = value; MaxHpChanged?.Invoke(_maxHp); } }
    public long CoinsReward { get => _coinsReward; set => _coinsReward = value; }

    public void Reset()
    {
        CurrentHp = 0;
        MaxHp = 10;
        CoinsReward = 1;
    }
}
