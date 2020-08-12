using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutomationsDamageUpgrader : MonoBehaviour, IUpgrade
{
    public void Upgrade(AutomationsData automationsData, float percentage, int automationIndex)
    {
        automationsData.AutomationsPowerPercentageIncrease += percentage;
    }
}
