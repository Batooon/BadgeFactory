namespace AutomationImplementation
{
    public interface IAutomationDatabase
    {
        CurrentPlayerAutomationData GetAutomationData(int automationId);
        int GetAutomationsLength();
        void SaveAutomationData(CurrentPlayerAutomationData automationData, int automationId);
        void Serialize();
        int GetLastUnlockedAutomationId();
        OverallAutomationsData GetOverallAutomationsData();
        bool CanUpgradeSomething();
    }
}