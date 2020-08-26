using AutomationsImplementation;
using BadgeImplementation;
using UnityEngine;
using GooglePlayGames.BasicApi.SavedGame;

public class Services : MonoBehaviour
{
    [SerializeField] private bool _deleteExistingDataOnDevice;
    [SerializeField] private string _playerDataFileName;
    [SerializeField] private string _automationsDataFileName;
    [SerializeField] private string _badgeDataFileName;
    [SerializeField] private string _settingsDataFileName;
    [SerializeField] private DefaultAutomationsData _defaultAutomationsData;
    [SerializeField] private Vibration _vibration;
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
        if (_playerData.IsReturningPlayer == false)
        {
            DeleteData();
        }

        if (_deleteExistingDataOnDevice)
        {
            DeleteData();
        }
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
        _vibration.Init(_settingsData);
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
    }

    private void Start()
    {
        PlayGames.Initialize(_playGamesDebugMode, new CloudSavesUI(3, true, true));
        PlayGames.Authenticate((bool value) =>
        {
            Debug.Log(value);
            if (_playerData.IsReturningPlayer)
            {
                /*
                PlayGames.ReadSavedData(PlayGames.DefaultFileName, (status, data) =>
                {
                    if (status == SavedGameRequestStatus.Success && data.Length > 0)
                    {
                        LoadCloudData(data);
                        Debug.Log("Cloud Data has been loaded");
                    }
                });*/
            }
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

    private void SaveCloudData()
    {
        CloudSaveData cloudData = new CloudSaveData(_playerData, _automationsData, _badgeData);
        byte[] data = FileOperations.GetBytes(cloudData);
        PlayGames.WriteSavedGame(data);
    }

    private void LoadCloudData(byte[] data)
    {
        CloudSaveData cloudData;
        cloudData = FileOperations.GetDataFromBytes<CloudSaveData>(data);
        _playerData = cloudData.Data;
        _badgeData = cloudData.Badge;
        _automationsData = cloudData.AutomationData;
    }

    private void GetData()
    {
        _playerData = FileOperations.Deserialize<PlayerData>(_playerDataFileName);
        _badgeData = FileOperations.Deserialize<BadgeData>(_badgeDataFileName);
        _automationsData = FileOperations.Deserialize<AutomationsData>(_automationsDataFileName);
        _settingsData = FileOperations.Deserialize<SettingsData>(_settingsDataFileName);
    }

    public void OpenSavesUIMenu()
    {
        PlayGames.ShowSavesUI((status, data) =>
        {
            if (status == SavedGameRequestStatus.Success && data.Length > 0)
            {
                LoadCloudData(data);
            }
        }, () => SaveCloudData());
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
