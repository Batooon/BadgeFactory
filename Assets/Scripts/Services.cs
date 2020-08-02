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
    [SerializeField] private DefaultAutomationsData _defaultAutoamtionsData;
    [SerializeField] private Vibration _vibration;
    [SerializeField] private Settings _settings;
    [SerializeField] private Badge _badge;
    [SerializeField] private Automations _automations;
    [SerializeField] private PlayerStatsPresentation _playerStatsPresentation;
    [SerializeField] private GameResetter _gameResetter;
    [SerializeField] private OfflineProgress _offlineProgress;
    [SerializeField] private AdsManager _adsManager;
    [SerializeField] private FarmLevelButton _farmLevelButton;
#if UNITY_EDITOR
    [SerializeField] private GodMode _godMode;
#endif

    private PlayerData _playerData;
    private AutomationsData _automationsData;
    private BadgeData _badgeData;
    private SettingsData _settingsData;

    private void Awake()
    {
#if UNITY_ANDROID
        if (_deleteExistingDataOnDevice)
        {
            FileOperations.DeleteFile(_playerDataFileName);
            FileOperations.DeleteFile(_badgeDataFileName);
            FileOperations.DeleteFile(_automationsDataFileName);
            FileOperations.DeleteFile(_settingsDataFileName);
        }
#endif

        if (FileOperations.IsFileExist(_settingsDataFileName) == false)
        {
            _playerData = new PlayerData();
            _badgeData = new BadgeData();
            _automationsData = new AutomationsData();
            _settingsData = new SettingsData();

            _automationsData.Automations.Clear();

            for (int i = 0; i < _defaultAutoamtionsData.Automations.Count; i++)
                _automationsData.Automations.Add(_defaultAutoamtionsData.Automations[i]);

            SaveData();
        }
        else
            GetData();

#if UNITY_EDITOR
        _godMode.Init(_playerData);
#endif
        _settings.Init(_settingsData);
        _vibration.Init(_settingsData);
        _farmLevelButton.Init(_playerData);
        _adsManager.Init();
        _offlineProgress.Init(_playerData, _badgeData, _automationsData);
        _gameResetter.Init(
            _playerData,
            _automationsData,
            _badgeData);
        _playerStatsPresentation.Init(_playerData);
        _badge.Init(_playerData, _automationsData, _badgeData);
        _automations.Init(_playerData, _automationsData);
        _playerData.IsReturningPlayer = true;
        PlayGames.AuthenticateUser((bool value) => Debug.Log(value));
    }

    private void OnApplicationQuit()
    {
        SaveData();
    }

    private void OnApplicationPause(bool pause)
    {
        if (pause)
        {
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

    private void GetData()
    {
        _playerData = FileOperations.Deserialize<PlayerData>(_playerDataFileName);
        _badgeData = FileOperations.Deserialize<BadgeData>(_badgeDataFileName);
        _automationsData = FileOperations.Deserialize<AutomationsData>(_automationsDataFileName);
        _settingsData = FileOperations.Deserialize<SettingsData>(_settingsDataFileName);
    }
}
