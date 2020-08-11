using System;
using UnityEngine;

public class UsualAutomation : MonoBehaviour, IAutomation
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
            newCost += Mathf.FloorToInt(automationData.StartingCost * Mathf.Pow(_upgradeFactor, level - 1));
            level += 1;
        }

        automationData.CurrentCost = newCost;
    }

    public void Upgrade(Automation automationData, AutomationsData automationsData)
    {
        long newDamage = 0;

        if (automationData.Level != 0)
            automationsData.AutomationsPower -= automationData.CurrentDamage;

        for (int i = 0; i < automationsData.LevelsToUpgrade; i++)
        {
            automationData.Level += 1;
            newDamage += Mathf.RoundToInt(automationData.StartingDamage * _upgradeFactor * automationData.Level);
            if (automationData.Level >= 200 && automationData.Level % 25 == 0 && automationData.Level < 4000)
                newDamage *= 4;
            if (automationData.Level >= 1000 && automationData.Level % 1000 == 0 && automationData.Level < 4000)
                newDamage *= 10;
        }

        automationData.CurrentDamage = newDamage + (newDamage * (automationData.PowerUpPercentage / 100));
        automationsData.AutomationsPower += automationData.CurrentDamage;
        RecalculateCost(automationsData.LevelsToUpgrade, automationData);
    }
}
