
using AutomationImplementation;

public interface IAutomation
{
    void Upgrade(Automation automationData, AutomationsData automationsData);
    void RecalculateCost(int levelsToUpgrade, Automation automationData);
}
