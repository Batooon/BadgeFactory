using UnityEngine;

namespace Automations
{

    public class ClickPowerUpgrader : IAutomationCommand
    {
        public void Execute(UpgradeComponent upgradeComponent, float percentageToUpgrade)
        {
            AutomationsData automationsData = upgradeComponent.AutomationsData;
            automationsData.ClickPowerPercentageIncrease += percentageToUpgrade;
            automationsData.ClickPower +=
                Mathf.RoundToInt(automationsData.AutomationsPower * percentageToUpgrade * .01f);
        }
    }
}