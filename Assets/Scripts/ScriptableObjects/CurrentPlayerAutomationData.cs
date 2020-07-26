using System;
using UnityEngine;

[Serializable]
public class SerializedCurrentPLayerAutomationData : ISerializationCallbackReceiver
{
    public bool CanUpgrade;

    public CurrentPlayerAutomationData playerAutomationData = new CurrentPlayerAutomationData();

    public void OnAfterDeserialize()
    {
        playerAutomationData.CanUpgrade = CanUpgrade;
    }

    public void OnBeforeSerialize()
    {
        CanUpgrade = playerAutomationData.CanUpgrade;
    }
}

[Serializable]
public class CurrentPlayerAutomationData
{
    public event Action UpgradeAvailabilityChanged;
    public event Action<int> CostChanged;
    public int StartingCost;
    public int StartingDamage;
    public int Level;
    public int Cost;
    public int DamagePerSecond;
    public bool IsUnlocked;

    public bool CanUpgrade
    {
        get => _canUpgrade;
        set
        {
            if (_canUpgrade != value)
            {
                _canUpgrade = value;
                UpgradeAvailabilityChanged?.Invoke();
            }
        }
    }

    private bool _canUpgrade;

    public void RecalculateCost(int levelsToUpgrade)
    {
        float upgradeFactor = 1.07f;
        float costFactor = 1f;

        for (int i = 0; i < Level - 1 + levelsToUpgrade; i++)
            costFactor *= upgradeFactor;
        Cost = (int)(StartingCost * costFactor);
        CostChanged?.Invoke(Cost);
    }
}