using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;
using OdinSerializer;
using OdinSerializer.Utilities;

public interface IAutomationDatabase
{
    CurrentPlayerAutomationData GetAutomationData(int automationId);
    void SaveAutomationData(CurrentPlayerAutomationData automationData, int automationId);
    void Serialize();
}

public class AutomationDatabse : IAutomationDatabase
{
    private List<CurrentPlayerAutomationData> AutomationData = new List<CurrentPlayerAutomationData>();

    public AutomationDatabse()
    {
        //Deserialize Automation Data
    }

    public CurrentPlayerAutomationData GetAutomationData(int automationId)
    {
        return new CurrentPlayerAutomationData();
        //return AutomationData[automationId];
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

public class AutomationLogic : SerializedMonoBehaviour
{
    public event Action<CurrentPlayerAutomationData> AutomationUpgraded;

    [SerializeField]
    private Button _upgradeButton;

    private int _automationId;

    [OdinSerialize]
    private IAutomation _automation;

    private IAutomationBusinessInput _automationInput;

    private void Awake()
    {
        AutomationPresentation automationPresentation=GetComponent<AutomationPresentation>();
        _automationInput = new AutomationBusinessRules(new AutomationPresentator(automationPresentation),
            new PlayerDataAccess(),
            new AutomationDatabse());
    }

    private void Start()
    {
        _upgradeButton.onClick.AddListener(OnUpgradeButtonPressed);
    }

    public void OnUpgradeButtonPressed()
    {
        _automationInput.TryUpgradeAutomation(_automationId, _automation);
    }

    public void SetAutomationType(IAutomation automation)
    {
        _automation = automation;
    }
}