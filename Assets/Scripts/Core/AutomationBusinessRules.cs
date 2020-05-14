using Automation;
using Automations;
using GooglePlayGames.BasicApi.Multiplayer;
using System;
using UnityEngine;

public interface IAutomationUpgrader
{
    void UpgradeAutomation(CurrentPlayerAutomationData automationData);
}

public interface IAutomationBusinessOutput
{
    void AutomationUpgraded(CurrentPlayerAutomationData autoamtionData, bool isEnougshMoney);
    void AutomationNotUpgraded();
    void UnlockNewAutomation();
}

public interface IAutomationBusinessInput
{
    void TryUpgradeAutomation(int automationId, IAutomation automation);
    void CheckIfUpgradeAvailable(int automationId);
}

public struct AutomationUpgradeParams
{
    public CurrentPlayerAutomationData AutomationData;
    public IAutomation Automation;
}

public class AutomationBusinessRules : IAutomationBusinessInput
{
    private IPlayerDataProvider _playerData;
    private IAutomationDatabase _automationDatabaseProvider;
    private IAutomationBusinessOutput _automationOutput;

    public AutomationBusinessRules(IAutomationBusinessOutput automationBusinessOutput,
        IPlayerDataProvider playerDataProvider,
        IAutomationDatabase automationDatabaseProvider)
    {
        _automationOutput = automationBusinessOutput;
        _playerData = playerDataProvider;
        _automationDatabaseProvider = automationDatabaseProvider;
    }

    public void CheckIfUpgradeAvailable(int automationId)
    {
        Data playerData = _playerData.GetPlayerData();
        CurrentPlayerAutomationData automationData = _automationDatabaseProvider.GetAutomationData(automationId);

        if (playerData.GoldAmount >= automationData.Cost && !automationData.CanUpgrade)
        {
            automationData.CanUpgrade = true;
            //Активировать кнопку улучшения
        }
        else if (automationData.CanUpgrade && playerData.GoldAmount <= automationData.Cost)
        {
            automationData.CanUpgrade = false;
            //выключить кнопку улучшения
        }
    }

    /*public void TryUnlockNewAutomation(int newAutomationId)
    {
        CurrentPlayerAutomationData automationData = _automationDatabaseProvider.GetAutomationData(newAutomationId);
        Data playerData = _playerData.GetPlayerData();

        if(automationData.IsUnlocked)
            return;

        if (automationData.Cost <= playerData.GoldAmount)
        {
            if (_automationDatabaseProvider.GetAutomationsLength() >= newAutomationId)
                _automationOutput.UnlockNewAutomation();
        }
    }*/

    public void TryUpgradeAutomation(int automationId, IAutomation automation)
    {
        Data playerData = _playerData.GetPlayerData();
        CurrentPlayerAutomationData automationData = _automationDatabaseProvider.GetAutomationData(automationId);

        if (playerData.GoldAmount >= automationData.Cost)
        {
            //TODO: сделать улучшение сразу же на несколько уровней
            playerData.GoldAmount -= automationData.Cost;
            automation.Upgrade(ref automationData);

            _automationDatabaseProvider.SaveAutomationData(automationData, automationId);
            _playerData.SavePlayerData(playerData);

            _automationOutput.AutomationUpgraded(automationData, playerData.GoldAmount >= automationData.Cost);
        }
        else
        {
            _automationOutput.AutomationNotUpgraded();
        }
    }
}

public class PlayerDataAccess : IPlayerDataProvider
{
    public event Action GoldAmountChanged;

    private static PlayerDataAccess _singleton;

    private Data _playerData;

    public static PlayerDataAccess GetPlayerDatabase()
    {
        if (_singleton == null)
            return _singleton = new PlayerDataAccess();

        return _singleton;
    }

    private PlayerDataAccess()
    {
        DeserializePlayerData();
    }

    public Data GetPlayerData()
    {
        return _playerData;
    }

    public void SavePlayerData(in Data playerData)
    {
        if (_playerData.GoldAmount != playerData.GoldAmount)
            GoldAmountChanged?.Invoke();
        _playerData = playerData;
    }

    private void DeserializePlayerData()
    {
    }

    private void SerializePlayerData()
    {
    }
}

public class AutomationPresentator : IAutomationBusinessOutput
{
    private AutomationPresentation _automationPresentation;

    public AutomationPresentator(AutomationPresentation automationPresentation)
    {
        _automationPresentation = automationPresentation;
    }

    public void AutomationNotUpgraded()
    {
        _automationPresentation.OnAutomationNotUpgraded();
    }

    public void AutomationUpgraded(CurrentPlayerAutomationData automationData, bool isEnoughMoney)
    {
        AutomationViewModel automationParams;
        automationParams.AutomationCost = automationData.Cost.ConvertValue();
        automationParams.AutomationDamage = automationData.DamagePerSecond.ConvertValue();
        automationParams.IsEnoughMoney = isEnoughMoney;
        _automationPresentation.OnAutomationUpgraded(automationParams);
    }

    public void UnlockNewAutomation()
    {
        throw new NotImplementedException();
    }
}
