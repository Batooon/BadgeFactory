using UnityEngine;

namespace Automations
{
    public interface IAutomationCommand
    {
        void Execute(UpgradeComponent upgradeComponent, float percentageToUpgrade);
    }

    public class ClickPowerAutomationUpgrader : IAutomationCommand
    {
        public void Execute(UpgradeComponent upgradeComponent, float percentageToUpgrade)
        {
            Automation data = upgradeComponent.AutomationData;
            data.PowerUpPercentage += percentageToUpgrade;
            data.CurrentDamage += Mathf.RoundToInt(data.CurrentDamage * percentageToUpgrade * .01f);
        }
    }
}