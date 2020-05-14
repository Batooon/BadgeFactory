public class AutomationModel
{
    private CurrentPlayerAutomationData _automationData;
    public CurrentPlayerAutomationData PlayerAutomationData
    {
        get => _automationData;
        private set { }
    }

    private IPlayerDataProvider _playerData;
    public Data PlayerStats
    {
        get => _playerData.GetPlayerData();
        private set { }
    }

    public AutomationModel(CurrentPlayerAutomationData automationData, IPlayerDataProvider playerData)
    {
        _automationData = automationData;
        _playerData = playerData;
    }
}
