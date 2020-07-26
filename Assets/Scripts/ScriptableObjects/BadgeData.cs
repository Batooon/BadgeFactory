using System;
using UnityEngine;

[CreateAssetMenu(menuName ="BadgeData")]
public class BadgeData : ScriptableObject
{
    [SerializeField] private int _currentHp;
    [SerializeField] private int _maxHp;
    [SerializeField] private int _coinsReward;

    public event Action<int> HpChanged;

    public int CurrentHp { get => _currentHp; set { _currentHp = value; HpChanged?.Invoke(_currentHp); } }
    public int MaxHp { get => _maxHp; set => _maxHp = value; }
    public int CoinsReward { get => _coinsReward; set => _coinsReward = value; }
}
