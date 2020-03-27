using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;
using System;

public delegate void QuitGameCallback();

[RequireComponent(typeof(SessionView))]
public class SessionController : MonoInstaller
{
    [SerializeField]
    private GameObject _badgeButton;
    [SerializeField]
    private GameObject _automationsPanel;

    #region Controllers
    private BadgeController _badgeController;
    #endregion

    public event Action<Data> PlayerDataChange;

    public SessionModel _sessionModel;
    private SessionView _sessionView;

    public override void InstallBindings()
    {
        Container.Bind<Data>().FromInstance(_sessionModel.PlayerData);
        _badgeController = (BadgeController)Container.InstantiateComponent(typeof(BadgeController), _badgeButton);
        _badgeController.LevelUp += OnLevelUp;
        _badgeController.CoinCollected += OnCoinCollected;
        PlayerDataChange += _badgeController.OnPlayerDataChange;
        Container.InstantiateComponent(typeof(AutomationsController), _automationsPanel);
    }

    [Inject]
    public void Construct()
    {
        Debug.Log("FHUEIOFUIEB");
        _sessionModel = new SessionModel();
        _sessionModel.PlayerDataChanged += OnPlayerDataChange;

        _sessionView = GetComponent<SessionView>();
        _sessionView.QuitCallback = OnQuitCallback;

        OnDataLoaded(_sessionModel.PlayerData);
    }

    private void OnPlayerDataChange(Data playerData)
    {
        PlayerDataChange?.Invoke(playerData);
    }

    private void OnDataLoaded(Data playerData)
    {
        _sessionView.FetchUI(playerData);
    }

    private void OnQuitCallback()
    {
        _sessionModel.Save();
        Application.Quit();
    }

    private void OnLevelUp()
    {
        if (_sessionModel.PlayerData.levelProgress == 10)
        {
            _sessionModel.PlayerData.levelProgress = 0;
            _sessionModel.PlayerData.Level += 1;
            //_sessionView.FetchUI(_sessionModel.PlayerData);
        }
        else
        {
            _sessionModel.PlayerData.levelProgress += 1;
        }
        _sessionView.FetchUI(_sessionModel.PlayerData);
        //_sessionView.LevelUp(_sessionModel.PlayerData.levelProgress);
        PlayerDataChange?.Invoke(_sessionModel.PlayerData);
    }

    private void OnCoinCollected(int amount)
    {
        _sessionModel.PlayerData.GoldAmount += amount;
        _sessionView.FetchUI(_sessionModel.PlayerData);
    }
}
