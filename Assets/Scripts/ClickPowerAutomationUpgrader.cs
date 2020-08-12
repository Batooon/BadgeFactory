using UnityEngine;

public class ClickPowerAutomationUpgrader : MonoBehaviour, IUpgrade
{
    public void Upgrade(AutomationsData automationsData, float percentage, int automationIndex)
    {
        long automationDamage = automationsData.Automations[automationIndex].CurrentDamage;

        automationsData.ClickPower -= automationDamage;

        automationsData.Automations[automationIndex].PowerUpPercentage += percentage;

        automationsData.ClickPower += automationsData.Automations[automationIndex].CurrentDamage;
    }
}
