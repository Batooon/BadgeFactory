using UnityEngine;

namespace Automations
{
    public class ClickPowerUpgrader : MonoBehaviour, IUpgrade
    {
        public void Upgrade(AutomationsData automationsData, float percentage, int automationIndex)
        {
            automationsData.ClickPowerPercentageIncrease += percentage;
        }
    }
}