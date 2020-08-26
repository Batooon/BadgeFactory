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
        if (automationData.Level != 1)
            automationsData.ClickPower -= automationData.CurrentDamage;

        for (int i = 0; i < automationsData.LevelsToUpgrade; i++)
        {
            long newDamage = 0;
            if (automationData.Level == 20)
            {
                automationsData.CriticalPowerIncreasePercentage += .1f;
                automationsData.ClickPowerCriticalHitChance += .1f;
            }
            automationData.Level += 1;
            newDamage += Mathf.RoundToInt(automationData.StartingDamage * _upgradeFactor * automationData.Level);

            foreach (var item in automationData.UpgradeComponents)
            {
                if (item.IsUpgradeComponentPurchased)
                    newDamage += Mathf.RoundToInt(newDamage * item.Percentage / 100);
            }
            automationData.CurrentDamage = newDamage;
        }
        automationsData.ClickPower += automationData.CurrentDamage;
        automationsData.ClickPower += Mathf.RoundToInt(automationsData.ClickPower * automationsData.ClickPowerPercentageIncrease);
        RecalculateCost(automationsData.LevelsToUpgrade, automationData);
    }
}
