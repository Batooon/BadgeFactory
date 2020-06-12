
using Automation;

public interface IAutomation
{
    void Upgrade(CurrentPlayerAutomationData automationData,OverallAutomationsData overallAutomationsData);
    //void Upgrade(ref int currentLevel, ref int currentDpsValue, ref int currentCost, AutomationUpgradeParams automationUpgradeParams);
    //void BuyNewAbility();
}
