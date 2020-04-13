using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AutomationPresentation))]
public class AutomationLogic : MonoBehaviour, IAutomationLogic //Automation controller
{
    private AutomationPresentation _automationPresentation;
    private AutomationModel _automationData;
    private IAutomation automation;

    public void InitializeAutomation(IAutomation automation, AutomationEditorParams automationParams, IPlayerData playerData)
    {
        _automationData = new AutomationModel(automation, automationParams, playerData);
        _automationPresentation = GetComponent<AutomationPresentation>();
        _automationPresentation.InitAutomation(automationParams);

        _automationPresentation.Upgrade += OnUpgradeButtonPressed;
        /*
        _automationPresentation.FetchDamage(_automationData.AutomationParams.StartingDamagePerSecond, CanUpgrade());
        _automationPresentation.FetchCost(_automationData.AutomationParams.StartingCost);*/
    }

    public void OnUpgradeButtonPressed()
    {/*
        AutomationUpgradeParams automationUpgradeParams;
        automationUpgradeParams.startingDpsValue = _automationData.AutomationParams.StartingDamagePerSecond;
        automationUpgradeParams.Startingcost = _automationData.AutomationParams.StartingCost;
        _automationData.Automation.Upgrade(ref _automationData.AutomationParams.StartingLevel,
            ref _automationData.AutomationParams.StartingDamagePerSecond, ref _automationData.AutomationParams.StartingCost, automationUpgradeParams);

        _automationPresentation.FetchCost(_automationData.AutomationParams.StartingCost);
        _automationPresentation.FetchDamage(_automationData.AutomationParams.StartingDamagePerSecond, CanUpgrade());*/
    }

    public void SetAutomationType(IAutomation automation)
    {
        this.automation = automation;
        Debug.Log(automation);
    }

    private bool CanUpgrade()
    {
        //return _automationData.PlayerStats.GoldAmount >= _automationData.AutomationParams.StartingCost;
        return false;
    }
}