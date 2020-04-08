using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using Zenject;

/// <summary>
/// Contains All Logic
/// Adds Callbacks to View for Updates
/// Changes data in it's Model
/// Talks to other Controllers via Messages
/// Diew when its model dies
/// get View via GetComponent
/// </summary>

public delegate void BadgeInputCallback();
public delegate void CoinCreatedCallback(GameObject coin, int coinCost, float timetoLive);

public struct Badge
{
    public Sprite BadgeSprite;
    public bool IsRare;
}

[RequireComponent(typeof(BadgeView))]
public class BadgeController : MonoBehaviour
{
    #region Indirect Event Messages
    public event Action BadgeClicked;
    public event Action CreateNewBadge;
    public event Action LevelUp;
    public event Action<int> CoinCollected;
    #endregion

    private BadgeModel _badgeModel;
    private BadgeView _badgeView;

    [Inject]
    public void Construct(IPlayerData playerData)
    {
        _badgeModel = new BadgeModel(playerData);
        _badgeModel.HpChanged += OnHpChanged;

        _badgeView = GetComponent<BadgeView>();

        _badgeView.InputCallback = InputCallback;
        _badgeView.CoinCallback = CoinCreatedCallback;

        InitNewBadge();
    }

    public void OnHpChanged()
    {
        if (_badgeModel.CurrentHp >= _badgeModel.MaxHp)
        {
            int coinsAmount = UnityEngine.Random.Range(3, 5);
            int oneCoinCost;
            if (_badgeModel.CostReward >= coinsAmount)
            {
                oneCoinCost = Mathf.CeilToInt(_badgeModel.CostReward / coinsAmount);
            }
            else
            {
                coinsAmount = 1;
                oneCoinCost = _badgeModel.CostReward; 
            }
            LevelUp?.Invoke();
            _badgeView.SpawnCoins(coinsAmount, oneCoinCost);
            InitNewBadge();
        }
        else
        {
            _badgeView.UpdateHp(_badgeModel.MaxHp, _badgeModel.CurrentHp);
        }
    }

    public void InitNewBadge()
    {
        StopAllCoroutines();
        int hp = GetNewHp();
        int cost = GetNewCost(hp);
        if (_badgeModel.PlayerData.GetPlayerData().Level % 5 == 0 && _badgeModel.PlayerData.GetPlayerData().LevelProgress == 10) 
        {
            _badgeModel.InitNewBossData(hp, cost);
            _badgeView.SetupBossUI(_badgeModel.BossCountdown);
            _badgeView.SetupBadge(_badgeModel.CurrentBadge);
        }
        else
        {
            _badgeModel.InitNewBadgeData(hp, cost);
            _badgeView.SetupBadge(_badgeModel.CurrentBadge);
        }
        _badgeView.UpdateHp(_badgeModel.MaxHp, _badgeModel.CurrentHp);
    }

    private int GetNewHp()
    {
        float exponent = 1f;
        for (int i = 0; i < _badgeModel.PlayerData.GetPlayerData().Level - 1; i++)
            exponent *= 1.55f;

        int hp = (int)(10 * (_badgeModel.PlayerData.GetPlayerData().Level - 1 + exponent));
        if (_badgeModel.PlayerData.GetPlayerData().Level % 5 == 0 && _badgeModel.PlayerData.GetPlayerData().Level == 10)
            hp *= 10;

        return hp;
    }

    private int GetNewCost(int hp)
    {
        return Mathf.CeilToInt(hp * .0667f);
    }

    private void OnDestroy()
    {
        _badgeModel.Save();
    }

    private void InputCallback()
    {
        _badgeModel.IncreaseHp();
        _badgeView.IncreaseBadgeImageAlpha(_badgeModel.MaxHp, _badgeModel.CurrentHp);
    }

    private void CoinCreatedCallback(GameObject coin, int coinCost, float timeToLive)
    {
        CoinController coinController = coin.AddComponent<CoinController>();
        coinController.Init(coinCost, timeToLive);
        coin.GetComponent<CoinController>().CoinCollected += CollectCoin;
    }

    private void CollectCoin(int amount)
    {
        CoinCollected?.Invoke(amount);
    }
}
