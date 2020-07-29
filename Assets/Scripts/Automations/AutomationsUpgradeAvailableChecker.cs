using UnityEngine;
using UnityEngine.UI;

public class AutomationsUpgradeAvailableChecker : MonoBehaviour
{
    [SerializeField] private Image _upgradeAvailabilityImage;

    private AutomationsData _automationsData;

    public void Init(AutomationsData automationsData)
    {
        _automationsData = automationsData;
        _automationsData.CanUpgradeSomethingChanged += FetchUpgradeAvailability;
        FetchUpgradeAvailability(_automationsData.CanUpgradeSomething);
    }

    private void OnEnable()
    {
        if (_automationsData == null)
            return;
        _automationsData.CanUpgradeSomethingChanged += FetchUpgradeAvailability;
    }

    private void OnDisable()
    {
        _automationsData.CanUpgradeSomethingChanged -= FetchUpgradeAvailability;
    }

    private void FetchUpgradeAvailability(bool canUpgradeSomething)
    {
        _upgradeAvailabilityImage.gameObject.SetActive(canUpgradeSomething);
    }

    private void OnApplicationPause(bool pause)
    {
        if (pause)
        {
            _automationsData.CanUpgradeSomethingChanged -= FetchUpgradeAvailability;
        }
    }
}
