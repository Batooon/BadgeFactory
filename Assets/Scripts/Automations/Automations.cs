using AutomationImplementation;
using UnityEngine;

namespace AutomationsImplementation
{
    [RequireComponent(typeof(AutomationsPresentation))]
    public class Automations : MonoBehaviour
    {
        [SerializeField] private AutomationsUpgradeAvailableChecker _upgradeAvailableChecker = null;
        [SerializeField] private UpgradeLevelsAmount _upgradeLevelsAmount = null;

        private PlayerData _playerData;
        private AutomationsData _automationsData;
        private IAutomationsBusinessInput _automationsInput;
        private AutomationsPresentation _automationPresentation;
        private IAutomationsBusinessOutput _automationsOutput;

        public void Init(PlayerData playerData, AutomationsData automationsData)
        {
            _playerData = playerData;
            _automationsData = automationsData;
            _automationPresentation = GetComponent<AutomationsPresentation>();
            _automationPresentation.Init(_automationsData);
            _automationsOutput = new AutomationsPresentator(_automationPresentation, _automationsData);

            _automationsInput = new AutomationsBusinessRules(
                automationsData,
                _automationsOutput,
                playerData);

            for (int i = 0; i < _automationsData.Automations.Count; i++)
            {
                _automationsData.Automations[i].CanUpgradeChanged += OnUpgradeAvailabilityChanged;
            }

            foreach (Transform automation in transform)
            {
                AutomationLogic automationLogic = automation.GetComponent<AutomationLogic>();
                automationLogic.Init(_playerData, _automationsData, _automationsData.Automations[automationLogic.AutomationId]);
            }

            _upgradeLevelsAmount.Init(_automationsData);

            _upgradeAvailableChecker.Init(_automationsData);

            _playerData.GoldChanged += _automationsInput.TryUnlockNewAutomation;

            _automationsInput.TryUnlockNewAutomation(_playerData.Gold);
        }

        private void OnEnable()
        {
            if (_playerData == null)
                return;

            if (_automationsData == null)
                return;

            _playerData.GoldChanged += _automationsInput.TryUnlockNewAutomation;

            _automationsInput.TryUnlockNewAutomation(_playerData.Gold);
        }

        private void OnDisable()
        {
            if (_playerData == null)
                return;
            if (_automationsData == null)
                return;

            _playerData.GoldChanged -= _automationsInput.TryUnlockNewAutomation;
        }

        private void OnUpgradeAvailabilityChanged(bool canUpgrade)
        {
            _automationsInput.CheckIfCanUpgradeSomething();
        }
    }

    public interface IAutomationsBusinessInput
    {
        void TryUnlockNewAutomation(long newGoldAmount);
        void CheckIfCanUpgradeSomething();
    }

    public class AutomationsBusinessRules : IAutomationsBusinessInput
    {
        private PlayerData _playerData;
        private AutomationsData _automationsData;
        private IAutomationsBusinessOutput _automationsOutput;

        public AutomationsBusinessRules(AutomationsData automationsData,
                                        IAutomationsBusinessOutput automationOutput,
                                        PlayerData playerData)
        {
            _playerData = playerData;
            _automationsData = automationsData;
            _automationsOutput = automationOutput;
        }

        public void CheckIfCanUpgradeSomething()
        {
            for (int i = 0; i < _automationsData.Automations.Count; i++)
            {
                if (_automationsData.Automations[i].CanUpgrade)
                {
                    _automationsData.CanUpgradeSomething = true;
                    return;
                }
            }
            _automationsData.CanUpgradeSomething = false;
        }

        public void TryUnlockNewAutomation(long newGoldAmount)
        {
            int lastUnlockedAutomationId = _automationsData.GetLastUnlockedAutomationId();
            int newAutomationId = lastUnlockedAutomationId + 1;

            if (newAutomationId >= _automationsData.Automations.Count)
                return;

            Automation newAutomationData = _automationsData.Automations[newAutomationId];
            Automation currentAutomation = _automationsData.Automations[lastUnlockedAutomationId];

            if (newGoldAmount >= currentAutomation.CurrentCost)
            {
                newAutomationData.IsUnlocked = true;
                _automationsOutput.UnlockNewAutomation(newAutomationId);
            }
        }
    }

    public interface IAutomationsBusinessOutput
    {
        void UnlockNewAutomation(int newAutomationId);
        void ClickPowerUpdated(long newPower);
        void AutomationsPowerUpdated(long newPower);
    }

    public class AutomationsPresentator : IAutomationsBusinessOutput
    {
        private AutomationsPresentation _automationsPresentation;

        public AutomationsPresentator(AutomationsPresentation automationsPresentation, AutomationsData automationsData)
        {
            _automationsPresentation = automationsPresentation;
            _automationsPresentation.SetUIValues(automationsData.ClickPower.ConvertValue(), automationsData.AutomationsPower.ConvertValue());
        }

        public void AutomationsPowerUpdated(long newPower)
        {
            //_automationsPresentation.UpdateAutomationsPower(newPower.ConvertValue());
        }

        public void ClickPowerUpdated(long newPower)
        {
            //_automationsPresentation.UpdateClickPower(newPower.ConvertValue());
        }

        public void UnlockNewAutomation(int newAutomationId)
        {
            _automationsPresentation.UnlockNewAutomation(newAutomationId);
        }
    }
}
