using AutomationsImplementation;
using BadgeImplementation;
using UnityEngine;

public class Services : MonoBehaviour
{
    [SerializeField] private bool _deleteExistingDataOnDevice;
    [SerializeField] private string _playerDataFileName;
    [SerializeField] private string _automationsDataFileName;
    [SerializeField] private string _badgeDataFileName;
    [SerializeField] private string _settingsDataFileName;
    [SerializeField] private DefaultAutomationsData _defaultAutomationsData;
    [SerializeField] private Settings _settings;
    [SerializeField] private Badge _badge;
    [SerializeField] private Automations _automations;
    [SerializeField] private PlayerStatsPresentation _playerStatsPresentation;
    [SerializeField] private GameResetter _gameResetter;
    [SerializeField] private OfflineProgress _offlineProgress;
    [SerializeField] private AdsManager _adsManager;
    [SerializeField] private FarmLevelButton _farmLevelButton;
    [SerializeField] private AudioService _audioService;
    [SerializeField] private PlayGamesAuthenticator _playGamesAuthenticator;
    [SerializeField] private bool _playGamesDebugMode;
    [SerializeField] private Tutorial _tutorial;
#if UNITY_EDITOR
    [SerializeField] private GodMode _godMode;
#endif

    [SerializeField]private PlayerData _playerData;
    [SerializeField]private AutomationsData _automationsData;
    [SerializeField]private BadgeData _badgeData;
    [SerializeField]private SettingsData _settingsData;

    private void Awake()
    {
#if UNITY_ANDROID
        if (_deleteExistingDataOnDevice)
            DeleteData();
#endif

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
        _automations.Init(_playerData, _automationsData);
        _playerData.IsReturningPlayer = true;
    }

    private void Start()
    {
        PlayGames.Initialize(_playGamesDebugMode);
        PlayGames.Authenticate((bool value) =>
        {
            Debug.Log(value);
        });
        _playGamesAuthenticator.Init();
    }

    private void OnApplicationQuit()
    {
        RememberPlayer();
        SaveData();
    }

    private void OnApplicationPause(bool pause)
    {
        if (pause)
        {
            RememberPlayer();
            SaveData();
        }
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
[System.Serializable]
public struct CloudSaveData
{
    public PlayerData Data;
    public AutomationsData AutomationData;
    public BadgeData Badge;

    public CloudSaveData(PlayerData playerData,AutomationsData automationsData,BadgeData badgeData)
    {
        Data = playerData;
        AutomationData = automationsData;
        Badge = badgeData;
    }
}
