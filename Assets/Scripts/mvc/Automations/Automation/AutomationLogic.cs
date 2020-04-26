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
    private const string AutomationDataPath = "hfisorgniorbgoiebrogb";
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
    public IAutomation _automation;
    private IAutomationDatabase _automationDatabase;

    private IAutomationBusinessInput _automationInput;

    private void Awake()
    {
        _automationDatabase = new AutomationDatabse();
        AutomationPresentation automationPresentation=GetComponent<AutomationPresentation>();
        _automationInput = new AutomationBusinessRules(new AutomationPresentator(automationPresentation));
    }

    private void Start()
    {
        _upgradeButton.onClick.AddListener(OnUpgradeButtonPressed);
    }

    public void OnUpgradeButtonPressed()
    {
        CurrentPlayerAutomationData automationData = _automationDatabase.GetAutomationData(_automationId);

        AutomationUpgradeParams automationUpgradeParams;
        automationUpgradeParams.Automation = _automation;
        automationUpgradeParams.AutomationData = automationData;

        _automationInput.TryUpgradeAutomation(automationUpgradeParams);

        _automationDatabase.SaveAutomationData(automationData, _automationId);
    }

    public void SetAutomationType(IAutomation automation)
    {
        _automation = automation;
    }
}