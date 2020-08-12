using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickPowerUpgrader : MonoBehaviour, IUpgrade
{
    public void Upgrade(AutomationsData automationsData, float percentage, int automationIndex)
    {
        automationsData.ClickPowerPercentageIncrease += percentage;
    }
}
