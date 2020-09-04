using UnityEngine;

namespace Automations
{
    public class AutomationPowerUpgrader : MonoBehaviour, IUpgrade
    {
        public void Upgrade(AutomationsData automationsData, float percentage, int automationIndex)
        {
            long automationDamage = automationsData.Automations[automationIndex].CurrentDamage;

            automationsData.AutomationsPower -= automationDamage;
            automationsData.Automations[automationIndex].PowerUpPercentage += percentage;
            automationsData.AutomationsPower += automationsData.Automations[automationIndex].CurrentDamage;
        }
    }
}