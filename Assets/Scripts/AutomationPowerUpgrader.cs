using UnityEngine;

public class AutomationPowerUpgrader : MonoBehaviour, IUpgrade
{
    public void Upgrade(AutomationsData automationsData, int percentage, int automationIndex)
    {
        long automationDamage = automationsData.Automations[automationIndex].CurrentDamage;

        automationsData.AutomationsPower -= automationDamage;
        automationsData.Automations[automationIndex].PowerUpPercentage += percentage;
    }
}
