using UnityEngine;

namespace Automations
{
    public class AutomationsDamageUpgrader : MonoBehaviour, IUpgrade
    {
        public void Upgrade(AutomationsData automationsData, float percentage, int automationIndex)
        {
            automationsData.AutomationsPowerPercentageIncrease += percentage;
        }
    }
}