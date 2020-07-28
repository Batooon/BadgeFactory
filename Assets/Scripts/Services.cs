using AutomationsImplementation;
using BadgeImplementation;
using UnityEngine;

public class Services : MonoBehaviour
{
    [SerializeField] private PlayerData _playerData = null;
    [SerializeField] private AutomationsData _automationsData = null;
    [SerializeField] private BadgeData _badgeData = null;
    [SerializeField] private PlayerData _defaultPlayerData = null;
    [SerializeField] private AutomationsData _defaultAutomationsData = null;
    [SerializeField] private BadgeData _defaultBadgeData = null;
    [SerializeField] private Badge _badge = null;
    [SerializeField] private Automations _automations = null;
    [SerializeField] private PlayerStatsPresentation _playerStatsPresentation = null;
    [SerializeField] private GameResetter _gameResetter = null;
    [SerializeField] private OfflineProgress _offlineProgress = null;
    [SerializeField] private AdsManager _adsManager = null;
    [SerializeField] private FarmLevelButton _farmLevelButton = null;

    private void Awake()
    {
        if (_playerData.IsReturningPlayer == false)
        {
            _playerData.BossCountdownTime = _defaultPlayerData.BossCountdownTime;
            _playerData.DamageBonus = _defaultPlayerData.DamageBonus;
            _playerData.Gold = _defaultPlayerData.Gold;
            _playerData.IsReturningPlayer = _defaultPlayerData.IsReturningPlayer;
            _playerData.LastTimeInGame = _defaultPlayerData.LastTimeInGame;
            _playerData.Level = _defaultPlayerData.Level;
            _playerData.LevelProgress = _defaultPlayerData.LevelProgress;
            _playerData.MaxLevelProgress = _defaultPlayerData.MaxLevelProgress;
            _playerData.NeedToIncreaseLevel = _defaultPlayerData.NeedToIncreaseLevel;

            _badgeData.CoinsReward = _defaultBadgeData.CoinsReward;
            _badgeData.CurrentHp = _defaultBadgeData.CurrentHp;
            _badgeData.MaxHp = _defaultBadgeData.MaxHp;

            _automationsData.AutomationsPower = _defaultAutomationsData.AutomationsPower;
            _automationsData.CanUpgradeSomething = _defaultAutomationsData.CanUpgradeSomething;
            _automationsData.ClickPower = _defaultAutomationsData.ClickPower;
            _automationsData.LevelsToUpgrade = _defaultAutomationsData.LevelsToUpgrade;

            for (int i = 0; i < _defaultAutomationsData.Automations.Count; i++)
            {
                if (_automationsData.Automations.Count - 1 <= i)
                {
                    Automation automation = _defaultAutomationsData.Automations[i];
                    _automationsData.Automations.Add(automation);
                    continue;
                }
                Automation automationData = _defaultAutomationsData.Automations[i];
                automationData.CanUpgrade = _defaultAutomationsData.Automations[i].CanUpgrade;
                automationData.CurrentCost = _defaultAutomationsData.Automations[i].StartingCost;
                automationData.CurrentDamage = _defaultAutomationsData.Automations[i].StartingDamage;
                automationData.Level = i == 0 ? 1 : 0;
                automationData.IsUnlocked = i == 0;
            }
        }

        _farmLevelButton.Init(_playerData);
        _adsManager.Init();
        _offlineProgress.Init(_playerData, _badgeData, _automationsData);
        _gameResetter.Init(
            _playerData,
            _automationsData,
            _badgeData,
            _defaultPlayerData,
            _defaultAutomationsData,
            _defaultBadgeData);
        _playerStatsPresentation.Init(_playerData);
        _badge.Init(_playerData, _automationsData, _badgeData);
        _automations.Init(_playerData, _automationsData);
        _playerData.IsReturningPlayer = true;
    }
}
