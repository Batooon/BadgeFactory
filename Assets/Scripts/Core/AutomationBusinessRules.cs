using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public interface IAutomationUpgrader
{
    void UpgradeAutomation(CurrentPlayerAutomationData automationData);
}

public interface IAutomationBusinessOutput
{
    void AutomationUpgraded(CurrentPlayerAutomationData autoamtionData, bool isEnougshMoney);
    void AutomationNotUpgraded();
}

public interface IAutomationBusinessInput
{
    void TryUpgradeAutomation(int automationId, IAutomation automation);
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
    private Data _playerData;

    public PlayerDataAccess()
    {
        DeserializePlayerData();
    }

    public Data GetPlayerData()
    {
        return _playerData;
    }

    public void SavePlayerData(Data playerData)
    {
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
}
