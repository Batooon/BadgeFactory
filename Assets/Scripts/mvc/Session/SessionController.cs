using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;
using System;

public interface IPlayerData
{
    Data GetPlayerData();
}

public delegate void QuitGameCallback();

[RequireComponent(typeof(SessionView))]
public class SessionController : MonoInstaller, IPlayerData
{
    #region Events
    public event Action<int> GoldChanged;
    public event Action<int> LevelChanged;
    public event Action<int> LevelProgressChanged;
    #endregion

    [SerializeField]
    private GameObject _badgeButton;
    [SerializeField]
    private GameObject _automationsPanel;

    #region Controllers
    private BadgeController _badgeController;
    #endregion

    public SessionModel _sessionModel;
    private SessionView _sessionView;

    public override void InstallBindings()
    {
        Container.Bind<IPlayerData>().FromInstance(this);
        _badgeController = (BadgeController)Container.InstantiateComponent(typeof(BadgeController), _badgeButton);
        #region Subscription to another controller events
        _badgeController.LevelUp += OnLevelUp;
        _badgeController.CoinCollected += OnCoinCollected;
        #endregion
        Container.InstantiateComponent(typeof(AutomationsController), _automationsPanel);
    }

    [Inject]
    public void Construct()
    {
        _sessionModel = new SessionModel();

        _sessionView = GetComponent<SessionView>();
        _sessionView.QuitCallback = OnQuitCallback;

        _sessionView.FetchAllComponents(_sessionModel.PlayerData);
    }

    private void OnQuitCallback()
    {
        _sessionModel.Save();
        Application.Quit();
    }

    private void OnLevelUp()
    {
        if (_sessionModel.PlayerData.LevelProgress == 10)
        {
            _sessionModel.PlayerData.LevelProgress = 0;
            _sessionModel.PlayerData.Level += 1;
            _sessionView.FetchLevel(_sessionModel.PlayerData.Level);
            _sessionView.FetchLevelProgress(_sessionModel.PlayerData.LevelProgress);
            LevelChanged?.Invoke(_sessionModel.PlayerData.Level);
            LevelProgressChanged?.Invoke(_sessionModel.PlayerData.LevelProgress);
        }
        else
        {
            _sessionModel.PlayerData.LevelProgress += 1;
            _sessionView.FetchLevelProgress(_sessionModel.PlayerData.LevelProgress);
            LevelProgressChanged?.Invoke(_sessionModel.PlayerData.LevelProgress);
        }
    }

    private void OnCoinCollected(int amount)
    {
        _sessionModel.PlayerData.GoldAmount += amount;
        _sessionView.FetchGold(_sessionModel.PlayerData.GoldAmount);
    }

    public Data GetPlayerData()
    {
        return _sessionModel.PlayerData;
    }
}
