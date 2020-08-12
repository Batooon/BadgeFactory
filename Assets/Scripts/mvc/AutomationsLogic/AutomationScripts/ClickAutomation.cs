using System;
using UnityEngine;

public class ClickAutomation : MonoBehaviour, IAutomation
{
    private const float _upgradeFactor = 1.07f;

    public void RecalculateCost(int levelsToUpgrade, Automation automationData)
    {
        int level = automationData.Level;
        long newCost = 0;

        if (automationData.Level == 0 && levelsToUpgrade == 1)
        {
            automationData.CurrentCost = automationData.StartingCost;
            return;
        }

        for (int i = 0; i < levelsToUpgrade; i++)
        {
            if (level < 15)
            {
                newCost += Mathf.FloorToInt((5 + level) * Mathf.Pow(_upgradeFactor, level - 1));
                level += 1;
            }
            else
            {
                newCost += Mathf.FloorToInt(20 * Mathf.Pow(_upgradeFactor, level - 1));
                level += 1;
            }
        }
        automationData.CurrentCost = newCost;
    }

    public void Upgrade(Automation automationData, AutomationsData automationsData)
    {
        long newDamage = 0;

        if (automationData.Level != 1)
            automationsData.ClickPower -= automationData.CurrentDamage;

        for (int i = 0; i < automationsData.LevelsToUpgrade; i++)
        {
            automationData.Level += 1;
            newDamage += Mathf.RoundToInt(automationData.StartingDamage * _upgradeFactor * automationData.Level);
            newDamage += newDamage * Mathf.RoundToInt(automationData.PowerUpPercentage / 100);
        }

        automationData.CurrentDamage = newDamage;
        automationsData.ClickPower += automationData.CurrentDamage;
        RecalculateCost(automationsData.LevelsToUpgrade, automationData);
    }
}
