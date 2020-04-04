using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AutomationPresentation))]
public class AutomationLogic : MonoBehaviour
{
    private AutomationPresentation _automationPresentation;
    private AutomationData _automationData;

    public void InitializeAutomation(IAutomation automation,AutomationCreationParams automationParams,Data playerData)
    {
        _automationData = new AutomationData(automation, automationParams, playerData);
        _automationPresentation = GetComponent<AutomationPresentation>();
        _automationPresentation.InitAutomation(automationParams);

        _automationPresentation.Upgrade += OnUpgradeButtonPressed;
    }

    public void OnUpgradeButtonPressed()
    {
        _automationData.Upgrade();
    }
}
