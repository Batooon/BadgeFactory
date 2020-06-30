using UnityEngine;
using UnityEngine.UI;
using Automation;

public class AutomationsUpgradeAvailableChecker : MonoBehaviour
{
    [SerializeField]
    private Image _upgradeAvailabilityImage;

    private AutomationDatabse _automationDatabase;

    private void Awake()
    {
        _automationDatabase = AutomationDatabse.GetAutomationDatabase();
    }

    private void Start()
    {
        _automationDatabase.GetOverallAutomationsData().UpgradeAvailable += FetchUpgradeAvailability;
        FetchUpgradeAvailability(_automationDatabase.CanUpgradeSomething());
    }

    private void FetchUpgradeAvailability(bool canUpgradeSomething)
    {
        _upgradeAvailabilityImage.gameObject.SetActive(canUpgradeSomething);
    }

    private void OnApplicationPause(bool pause)
    {
        if (pause)
        {
            _automationDatabase.GetOverallAutomationsData().UpgradeAvailable -= FetchUpgradeAvailability;
        }
    }
}
