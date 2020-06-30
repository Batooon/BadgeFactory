using Automation;
using System;
using System.IO;
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
    void FetchUpgradeButton(bool isInteractable);
}

public interface IAutomationBusinessInput
{
    void TryUpgradeAutomation(int automationId, IAutomation automation);
    void CheckIfUpgradeAvailable(int automationId, int goldValue);
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

    public void CheckIfUpgradeAvailable(int automationId, int goldValue)
    {
        CurrentPlayerAutomationData automationData = _automationDatabaseProvider.GetAutomationData(automationId);

        if (goldValue >= automationData.Cost && !automationData.CanUpgrade)
            automationData.CanUpgrade = true;

        else if (automationData.CanUpgrade && goldValue <= automationData.Cost)
            automationData.CanUpgrade = false;

        _automationOutput.FetchUpgradeButton(automationData.CanUpgrade);
    }

    public void TryUpgradeAutomation(int automationId, IAutomation automation)
    {
        Data playerData = _playerData.GetPlayerData();
        CurrentPlayerAutomationData automationData = _automationDatabaseProvider.GetAutomationData(automationId);

        if (playerData.GoldAmount >= automationData.Cost)
        {
            //TODO: сделать улучшение сразу же на несколько уровней
            playerData.GoldAmount -= automationData.Cost;
            automation.Upgrade(automationData,_automationDatabaseProvider.GetOverallAutomationsData());
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
    private static PlayerDataAccess _singleton;

    private const string _playerDataPath = "playerData.json";

    private SerializedPlayerData _playerData;

    public Data PlayerData
    {
        get
        {
            return _playerData.PlayerData;
        }
        set
        {
            _playerData.PlayerData = value;
        }
    }

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
        return PlayerData;
    }

    private void DeserializePlayerData()
    {
        _playerData = FileOperations.Deserialize<SerializedPlayerData>(_playerDataPath);
    }

    public void SerializePlayerData()
    {
        FileOperations.Serialize(_playerData, _playerDataPath);
    }
}

public class AutomationPresentator : IAutomationBusinessOutput
{
    private AutomationPresentation _automationPresentation;

    public AutomationPresentator(AutomationPresentation automationPresentation, CurrentPlayerAutomationData automationData)
    {
        _automationPresentation = automationPresentation;
        SetUpAutomation(automationData);
    }

    public void AutomationNotUpgraded()
    {
        _automationPresentation.OnAutomationNotUpgraded();
    }

    public void AutomationUpgraded(CurrentPlayerAutomationData automationData, bool isEnoughMoneyForNextUpgrade)
    {
        _automationPresentation.OnAutomationUpgraded(GetAutomationParams(automationData, isEnoughMoneyForNextUpgrade));
    }

    public void FetchUpgradeButton(bool isInteractable)
    {
        _automationPresentation.FetchUpgradeButton(isInteractable);
    }

    public void UnlockNewAutomation()
    {
        throw new NotImplementedException();
    }

    private void SetUpAutomation(CurrentPlayerAutomationData automationData)
    {
        _automationPresentation.SetUpAutomation(GetAutomationParams(automationData, automationData.CanUpgrade));
    }

    private AutomationViewModel GetAutomationParams(CurrentPlayerAutomationData automationData, bool isEnoughMoneyForNextUpgrade)
    {
        AutomationViewModel automationParams;
        automationParams.AutomationCost = automationData.Cost.ConvertValue();
        automationParams.AutomationDamage = automationData.DamagePerSecond.ConvertValue();
        automationParams.IsEnoughMoney = isEnoughMoneyForNextUpgrade;
        return automationParams;
    }
}
