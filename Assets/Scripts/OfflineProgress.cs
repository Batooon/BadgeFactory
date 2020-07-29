﻿using UnityEngine;
using System;
using TMPro;

public class OfflineProgress : MonoBehaviour
{
    [SerializeField] private GameObject _whileYouWereAwayPanel;
    [SerializeField] private TextMeshProUGUI _goldText;
    private PlayerData _playerData;
    private BadgeData _badgeData;
    private AutomationsData _automationsData;
    private int _earnedGold;

    public void Init(PlayerData playerData, BadgeData badgeData, AutomationsData automationsData)
    {
        _playerData = playerData;
        _badgeData = badgeData;
        _automationsData = automationsData;

        if (_playerData.IsReturningPlayer && _automationsData.AutomationsPower > 0)
            InitPanel();

        if (_playerData.IsReturningPlayer == false)
            _playerData.IsReturningPlayer = true;
    }

    public void CollectGold()
    {
        _playerData.Gold += _earnedGold;
    }

    private void InitPanel()
    {
        _goldText.text = CalculateEarnings().ConvertValue();
        _whileYouWereAwayPanel.SetActive(true);
    }

    private int CalculateEarnings()
    {
        TimeSpan timeDifference = DateTime.Now - _playerData.LastTimeInGame;

        double secondsToCreateBadge = _badgeData.MaxHp / _automationsData.AutomationsPower;
        int createdBadges = (int)(timeDifference.TotalSeconds / secondsToCreateBadge);
        return _earnedGold = _badgeData.CoinsReward * createdBadges;
    }

    private void OnApplicationQuit()
    {
        SaveDate();
    }

    private void OnApplicationPause(bool pause)
    {
        if (pause)
        {
            SaveDate();
        }
    }

    private void SaveDate()
    {
        _playerData.LastTimeInGame = DateTime.Now;
    }
}
