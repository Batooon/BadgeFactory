using UnityEngine;

namespace Automations
{

    public class AutomationsDamageUpgrader : IAutomationCommand
    {
        public void Execute(UpgradeComponent upgradeComponent, float percentageToUpgrade)
        {
            AutomationsData automationsData = upgradeComponent.AutomationsData;
            automationsData.AutomationsPowerPercentageIncrease += percentageToUpgrade;
            automationsData.AutomationsPower +=
                Mathf.RoundToInt(automationsData.AutomationsPower * percentageToUpgrade * .01f);
        }
    }
}