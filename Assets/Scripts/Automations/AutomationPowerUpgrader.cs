using UnityEngine;

namespace Automations
{
    public class AutomationPowerUpgrader : IAutomationCommand
    {
        public void Execute(UpgradeComponent upgradeComponent, float percentageToUpgrade)
        {
            Automation data = upgradeComponent.AutomationData;
            data.PowerUpPercentage += percentageToUpgrade;
            data.CurrentDamage += Mathf.RoundToInt(data.CurrentDamage * percentageToUpgrade * .01f);
        }
    }
}