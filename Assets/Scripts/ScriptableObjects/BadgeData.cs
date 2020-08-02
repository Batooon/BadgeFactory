using System;
using UnityEngine;

[Serializable]
public class BadgeData
{
    [SerializeField] private long _currentHp;
    [SerializeField] private long _maxHp = 10;
    [SerializeField] private long _coinsReward = 1;

    public event Action<long> HpChanged;

    public long CurrentHp { get => _currentHp; set { _currentHp = value; HpChanged?.Invoke(_currentHp); } }
    public long MaxHp { get => _maxHp; set => _maxHp = value; }
    public long CoinsReward { get => _coinsReward; set => _coinsReward = value; }
}
