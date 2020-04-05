
public interface IAutomation
{
    void Upgrade(ref int currentLevel, ref int currentDpsValue, ref int currentCost, AutomationUpgradeParams automationUpgradeParams);
    //void BuyNewAbility();
}
