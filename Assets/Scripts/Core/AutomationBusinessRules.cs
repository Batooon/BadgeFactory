using System;

public interface IAutomationUpgrader
{
    void UpgradeAutomation(Automation automationData);
}

public interface IAutomationBusinessOutput
{
    void AutomationUpgraded(Automation autoamtionData, bool isEnougshMoney);
    void AutomationNotUpgraded();
    void UnlockNewAutomation();
    void FetchUpgradeButton(bool isInteractable);
}

public interface IAutomationBusinessInput
{
    void TryUpgradeAutomation(int automationId, IAutomation automation);
    void CheckIfUpgradeAvailable(int automationId, long goldValue);
}

public struct AutomationUpgradeParams
{
    public CurrentPlayerAutomationData AutomationData;
    public IAutomation Automation;
}

public class AutomationBusinessRules : IAutomationBusinessInput
{
    private Automation _automation;
    private PlayerData _playerData;
    private AutomationsData _automationsData;
    private IAutomationBusinessOutput _automationOutput;

    public AutomationBusinessRules(IAutomationBusinessOutput automationBusinessOutput,
        PlayerData playerData,
        Automation automation,
        AutomationsData automationsData)
    {
        _automation = automation;
        _playerData = playerData;
        _automationOutput = automationBusinessOutput;
        _automationsData = automationsData;
    }

    public void CheckIfUpgradeAvailable(int automationId, long goldValue)
    {
        _automation.CanUpgrade = goldValue >= _automation.CurrentCost;

        _automationOutput.FetchUpgradeButton(_automation.CanUpgrade);
    }

    public void TryUpgradeAutomation(int automationId, IAutomation automation)
    {
        if (_playerData.Gold >= _automation.CurrentCost)
        {
            _playerData.Gold -= _automation.CurrentCost;
            automation.Upgrade(_automation,_automationsData);
            if (_automation.Level % 2000 == 0)
                _playerData.BadgePoints += 1;
            _automationOutput.AutomationUpgraded(_automation, _playerData.Gold >= _automation.CurrentCost);
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

    public AutomationPresentator(AutomationPresentation automationPresentation, Automation automationData)
    {
        _automationPresentation = automationPresentation;
        SetUpAutomation(automationData);
    }

    public void AutomationNotUpgraded()
    {
        _automationPresentation.OnAutomationNotUpgraded();
    }

    public void AutomationUpgraded(Automation automationData, bool isEnoughMoneyForNextUpgrade)
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

    private void SetUpAutomation(Automation automationData)
    {
        _automationPresentation.SetUpAutomation(GetAutomationParams(automationData, automationData.CanUpgrade));
    }

    private AutomationViewModel GetAutomationParams(Automation automationData, bool isEnoughMoneyForNextUpgrade)
    {
        AutomationViewModel automationParams;
        automationParams.AutomationCost = automationData.CurrentCost.ConvertValue();
        automationParams.AutomationDamage = automationData.CurrentDamage.ConvertValue();
        automationParams.IsEnoughMoney = isEnoughMoneyForNextUpgrade;
        return automationParams;
    }
}
