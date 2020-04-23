using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public interface IAutomationDatabase
{
    CurrentPlayerAutomationData GetAutomationData(int automationId);
    void SaveAutomationData(CurrentPlayerAutomationData automationData, int automationId);
    void Serialize();
}

public class AutomationDatabse : IAutomationDatabase
{
    private const string AutomationDataPath = "hfisorgniorbgoiebrogb";
    private List<CurrentPlayerAutomationData> AutomationData = new List<CurrentPlayerAutomationData>();

    public AutomationDatabse()
    {
        //Deserialize Automation Data
    }

    public CurrentPlayerAutomationData GetAutomationData(int automationId)
    {
        return AutomationData[automationId];
    }

    public void SaveAutomationData(CurrentPlayerAutomationData automationData, int automationID)
    {
        Debug.Log("Saving Automation Data(Actually no ✪ ω ✪)");
    }

    public void Serialize()
    {
        Debug.Log("Automations Serialized(no)");
    }

    ~AutomationDatabse()
    {
        Serialize();
    }
}

public class AutomationLogic : MonoBehaviour
{
    public event Action<CurrentPlayerAutomationData> AutomationUpgraded;

    private int _automationId;
    private IAutomation _automation;
    private IAutomationDatabase _automationDatabase;

    private void Awake()
    {
        _automationDatabase = new AutomationDatabse();
    }

    /* private CurrentPlayerAutomationData _defaultAutomaitonData;

     private AutomationPresentation _automationPresentation;
     private AutomationModel _automationModel;*/

    public void InitializeAutomation(CurrentPlayerAutomationData automationParams, IPlayerDataProvider playerData)
    {
       /* _automationModel = new AutomationModel(automationParams, playerData);
        _automationPresentation = GetComponent<AutomationPresentation>();
        _automationPresentation.InitAutomation(automationParams);

        _automationPresentation.Upgrade += OnUpgradeButtonPressed;*/
        /*
        _automationPresentation.FetchDamage(_automationData.AutomationParams.StartingDamagePerSecond, CanUpgrade());
        _automationPresentation.FetchCost(_automationData.AutomationParams.StartingCost);*/
    }

    public void OnUpgradeButtonPressed()
    {
        Debug.Log(_automation);
        CurrentPlayerAutomationData automationData = _automationDatabase.GetAutomationData(_automationId);
        _automation.Upgrade(ref automationData);
        _automationDatabase.SaveAutomationData(automationData, _automationId);

        AutomationUpgraded?.Invoke(automationData);
    }

    private bool CanUpgrade()
    {
        //return _automationData.PlayerStats.GoldAmount >= _automationData.AutomationParams.StartingCost;
        return true;
    }

    public void SetAutomationType(IAutomation automation)
    {
        _automation = automation;
    }
}