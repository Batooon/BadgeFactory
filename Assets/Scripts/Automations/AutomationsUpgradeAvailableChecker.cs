using System;
using UnityEngine;
using UnityEngine.Events;

namespace Automations
{
    [Serializable]
    public class AutomationsUpgradeAvailable : UnityEvent<bool> { }

    public class AutomationsUpgradeAvailableChecker : MonoBehaviour
    {
        [SerializeField] private AutomationsUpgradeAvailable _automationsUpgradeAvailable;

        private AutomationsData _automationsData;

        public void Init(AutomationsData automationsData)
        {
            _automationsData = automationsData;
        }

        private void OnEnable()
        {
            _automationsData.CanUpgradeSomethingChanged += FetchUpgradeAvailability;
            FetchUpgradeAvailability(_automationsData.CanUpgradeSomething);
        }

        private void OnDisable()
        {
            _automationsData.CanUpgradeSomethingChanged -= FetchUpgradeAvailability;
        }

        private void FetchUpgradeAvailability(bool canUpgradeSomething)
        {
            _automationsUpgradeAvailable?.Invoke(canUpgradeSomething);
        }
    }
}