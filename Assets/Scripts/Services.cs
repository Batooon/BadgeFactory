using AutomationsImplementation;
using BadgeImplementation;
using UnityEngine;

public class Services : MonoBehaviour
{
    [SerializeField] private Vibration _vibration;
    [SerializeField] private Settings _settings;
    [SerializeField] private SettingsData _settingsData;
    [SerializeField] private PlayerData _playerData;
    [SerializeField] private AutomationsData _automationsData;
    [SerializeField] private BadgeData _badgeData;
    [SerializeField] private PlayerData _defaultPlayerData;
    [SerializeField] private AutomationsData _defaultAutomationsData;
    [SerializeField] private BadgeData _defaultBadgeData;
    [SerializeField] private Badge _badge;
    [SerializeField] private Automations _automations;
    [SerializeField] private PlayerStatsPresentation _playerStatsPresentation;
    [SerializeField] private GameResetter _gameResetter;
    [SerializeField] private OfflineProgress _offlineProgress;
    [SerializeField] private AdsManager _adsManager;
    [SerializeField] private FarmLevelButton _farmLevelButton;

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

        _settings.Init(_settingsData);
        _vibration.Init(_settingsData);
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
