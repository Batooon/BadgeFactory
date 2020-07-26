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

    private void Awake()
    {
        _adsManager.Init();
        //_offlineProgress.Init(_playerData, _badgeData, _automationsData);
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
    }
}
