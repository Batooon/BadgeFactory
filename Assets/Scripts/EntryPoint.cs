using System.Collections.Generic;
using Automations;
using Badge;
using UnityEngine;

public enum SkillType
{
    AutomationClickPower,
    AutomationsPower,
    AutomationPower,
    ClickPower
}

public enum AutomationType
{
    ClickAutomation,
    UsualAutomation
}

public class EntryPoint : MonoBehaviour
{
    [Header("Automation Parameters")] 
    [SerializeField] private AutomationPresentation _automationPrefab;
    [SerializeField] private Transform _automationsParent;
    [SerializeField] private AutomationData[] _automationData;
    [Space(10)]
    [SerializeField] private bool _deleteExistingDataOnDevice;
    [SerializeField] private string _playerDataFileName;
    [SerializeField] private string _automationsDataFileName;
    [SerializeField] private string _badgeDataFileName;
    [SerializeField] private string _settingsDataFileName;
    [SerializeField] private DefaultAutomationsData _defaultAutomationsData;
    [SerializeField] private Settings _settings;
    [SerializeField] private BadgeService _badge;
    [SerializeField] private AutomationsService _automationsService;
    [SerializeField] private PlayerStatsPresentation _playerStatsPresentation;
    [SerializeField] private GameResetter _gameResetter;
    [SerializeField] private OfflineProgress _offlineProgress;
    [SerializeField] private AdsManager _adsManager;
    [SerializeField] private FarmLevelButton _farmLevelButton;
    [SerializeField] private AudioService _audioService;
    [SerializeField] private Tutorial _tutorial;
#if UNITY_EDITOR
    [SerializeField] private GodMode _godMode;
#endif

    [SerializeField]private PlayerData _playerData;
    [SerializeField]private AutomationsData _automationsData;
    [SerializeField]private BadgeData _badgeData;
    [SerializeField]private SettingsData _settingsData;

    private readonly Dictionary<AutomationType, IAutomation> _automationType = new Dictionary<AutomationType, IAutomation>();
    private readonly Dictionary<SkillType, IAutomationCommand> _skillType = new Dictionary<SkillType, IAutomationCommand>();

    private void Awake()
    {
        _automationType.Add(AutomationType.ClickAutomation, new ClickAutomation());
        _automationType.Add(AutomationType.UsualAutomation, new UsualAutomation());
        
        _skillType.Add(SkillType.AutomationPower,new AutomationPowerUpgrader());
        _skillType.Add(SkillType.AutomationsPower, new AutomationsDamageUpgrader());
        _skillType.Add(SkillType.ClickPower, new ClickPowerUpgrader());
        _skillType.Add(SkillType.AutomationClickPower, new ClickPowerAutomationUpgrader());

        if (_deleteExistingDataOnDevice)
            DeleteData();

        if (FileOperations.IsFileExist(_settingsDataFileName) == false)
        {
            _playerData.Init();
            _badgeData.Init();
            _automationsData.Init();
            SaveData();
        }
        else
            GetData();

#if UNITY_EDITOR
        _godMode.Init(_playerData);
#endif

        var builder = new AutomationBuilder(_playerData, _automationsData, _automationPrefab);
        foreach (var automationData in _automationData)
        {
            builder.InstantiateAutomation(_automationsParent, automationData.Data);
            builder.SetAutomationType(_automationType[automationData.AutomationType]);
            var skillsData = automationData.Data.UpgradeComponents;
            foreach (var skill in skillsData)
            {
                builder.AddSkill(_skillType[skill.Skill], skill);
            }
        }

        _tutorial.Init(_playerData);
        _audioService.Init();
        _settings.Init(_settingsData);
        Vibration.Init(_settingsData);
        _farmLevelButton.Init(_playerData);
        _adsManager.Init();
        _offlineProgress.Init(_playerData, _badgeData, _automationsData);
        _gameResetter.Init(
            _playerData,
            _automationsData,
            _badgeData,
            _defaultAutomationsData);
        _playerStatsPresentation.Init(_playerData);
        _badge.Init(_playerData, _automationsData, _badgeData);
        _automationsService = new AutomationsService(_playerData, _automationsData);
        //_automationsService.Init(_playerData, _automationsData);
        _playerData.IsReturningPlayer = true;
    }

    private void Start()
    {
        TinySauce.OnGameStarted();
    }

    private void OnApplicationQuit()
    {
        SavePlayerProgress();
    }

    private void OnApplicationPause(bool pause)
    {
        if (pause)
        {
            SavePlayerProgress();
        }
    }

    private void SavePlayerProgress()
    {
        RememberPlayer();
        SaveData();
        TinySauce.OnGameFinished(_playerData.Level.ToString(), _playerData.LevelProgress);
    }

    private void SaveData()
    {
        _playerData.SaveData(_playerDataFileName);
        FileOperations.Serialize(_badgeData, _badgeDataFileName);
        FileOperations.Serialize(_automationsData, _automationsDataFileName);
        FileOperations.Serialize(_settingsData, _settingsDataFileName);
    }

    private void DeleteData()
    {
        FileOperations.DeleteFile(_playerDataFileName);
        FileOperations.DeleteFile(_badgeDataFileName);
        FileOperations.DeleteFile(_automationsDataFileName);
        FileOperations.DeleteFile(_settingsDataFileName);
    }

    private void RememberPlayer()
    {
        if (_playerData.IsReturningPlayer == false)
            _playerData.IsReturningPlayer = true;
    }

    private void GetData()
    {
        _playerData = FileOperations.Deserialize<PlayerData>(_playerDataFileName);
        _badgeData = FileOperations.Deserialize<BadgeData>(_badgeDataFileName);
        _automationsData = FileOperations.Deserialize<AutomationsData>(_automationsDataFileName);
        _settingsData = FileOperations.Deserialize<SettingsData>(_settingsDataFileName);
    }
}
