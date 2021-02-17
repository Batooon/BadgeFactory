using System;
using UnityEngine;
using UnityEngine.Events;

namespace Automations
{
    [Serializable]
    public class AutomationsUpgradeAvailable : UnityEvent<bool>
    {
    }

    public class AutomationsUpgradeAvailableChecker : MonoBehaviour, IObserver
    {
        [SerializeField] private AutomationsUpgradeAvailable _automationsUpgradeAvailable;

        private AutomationsData _automationsData;

        public void Init(AutomationsData automationsData)
        {
            _automationsData = automationsData;
        }

        private void OnEnable()
        {
            //_automationsData.Attach(this);

            //FetchUpgradeAvailability(_automationsData.CanUpgradeSomething);
        }

        private void OnDisable()
        {
            //_automationsData.Detach(this);
        }

        private void FetchUpgradeAvailability(bool canUpgradeSomething)
        {
            _automationsUpgradeAvailable?.Invoke(canUpgradeSomething);
        }

        public void Fetch(ISubject subject)
        {
            FetchUpgradeAvailability(_automationsData.CanUpgradeSomething);
        }
    }
}