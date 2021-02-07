﻿using UnityEngine;

namespace Automations
{
    public class UsualAutomation : IAutomation
    {
        private const float UpgradeFactor = 1.07f;

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
                newCost += Mathf.FloorToInt(automationData.StartingCost * Mathf.Pow(UpgradeFactor, level - 1));
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
                newDamage += Mathf.RoundToInt(automationData.StartingDamage * UpgradeFactor * automationData.Level);
                if (automationData.Level >= 200 && automationData.Level % 25 == 0 && automationData.Level < 4000)
                    newDamage *= 4;
                if (automationData.Level >= 1000 && automationData.Level % 1000 == 0 && automationData.Level < 4000)
                    newDamage *= 10;

                foreach (var item in automationData.UpgradeComponents)
                {
                    if (item.IsUpgradeComponentPurchased)
                        newDamage += Mathf.RoundToInt(newDamage * item.Percentage / 100);
                }
                automationData.CurrentDamage = newDamage;
            }

            automationsData.AutomationsPower += automationData.CurrentDamage;
            automationsData.AutomationsPower += Mathf.RoundToInt(automationsData.AutomationsPower * automationsData.AutomationsPowerPercentageIncrease);
            RecalculateCost(automationsData.LevelsToUpgrade, automationData);
        }
    }
}