using System;
using UnityEngine;

[Serializable]
public class BadgeData
{
    [SerializeField] private long _currentHp;
    [SerializeField] private long _maxHp = 10;
    [SerializeField] private long _coinsReward = 1;

    public event Action<long> HpChanged;
    public event Action<long> MaxHpChanged;

    public long CurrentHp { get => _currentHp; set { _currentHp = value; HpChanged?.Invoke(_currentHp); } }
    public long MaxHp { get => _maxHp; set { _maxHp = value; MaxHpChanged?.Invoke(_maxHp); } }
    public long CoinsReward { get => _coinsReward; set => _coinsReward = value; }
}
