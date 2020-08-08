using UnityEngine;

public class ClickPowerUpgrader : MonoBehaviour, IUpgrade
{
    public void Upgrade(AutomationsData automationsData, int percentage, int automationIndex)
    {
        automationsData.ClickPower += automationsData.ClickPower * (percentage / 100);
        automationsData.Automations[automationIndex].PowerUpPercentage += percentage;
    }
}
