using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AutomationPresentation))]
public class AutomationLogic : MonoBehaviour //Automation controller
{
    private AutomationPresentation _automationPresentation;
    private AutomationData _automationData;

    public void InitializeAutomation(IAutomation automation, AutomationCreationParams automationParams, Data playerData)
    {
        _automationData = new AutomationData(automation, automationParams, playerData);
        _automationPresentation = GetComponent<AutomationPresentation>();
        _automationPresentation.InitAutomation(automationParams);

        _automationPresentation.Upgrade += OnUpgradeButtonPressed;

        _automationPresentation.FetchDamage(_automationData.AutomationParams.StartingDamagePerSecond, CanUpgrade());
        _automationPresentation.FetchCost(_automationData.AutomationParams.StartingCost);
    }

    public void OnUpgradeButtonPressed()
    {
        AutomationUpgradeParams automationUpgradeParams;
        automationUpgradeParams.startingDpsValue = _automationData.AutomationParams.StartingDamagePerSecond;
        automationUpgradeParams.Startingcost = _automationData.AutomationParams.StartingCost;
        _automationData.Automation.Upgrade(ref _automationData.AutomationParams.StartingLevel,
            ref _automationData.AutomationParams.StartingDamagePerSecond, ref _automationData.AutomationParams.StartingCost, automationUpgradeParams);

        _automationPresentation.FetchCost(_automationData.AutomationParams.StartingCost);
        _automationPresentation.FetchDamage(_automationData.AutomationParams.StartingDamagePerSecond, CanUpgrade());
    }

    private bool CanUpgrade()
    {
        return _automationData.PlayerStats.GoldAmount >= _automationData.AutomationParams.StartingCost;
    }
}
