using UnityEngine;

namespace Automations
{
    [RequireComponent(typeof(AutomationsPresentation))]
    public class AutomationsService : MonoBehaviour
    {
        [SerializeField] private AutomationsUpgradeAvailableChecker _upgradeAvailableChecker;
        [SerializeField] private UpgradeLevelsAmount _upgradeLevelsAmount;

        private PlayerData _playerData;
        private AutomationsData _automationsData;
        private AutomationsBusinessRules _automationsInput;
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

            foreach (Transform automation in transform)
            {
                AutomationLogic automationLogic = automation.GetComponent<AutomationLogic>();
                automationLogic.Init(_playerData, _automationsData, _automationsData.Automations[automationLogic.AutomationId]);
            }

            _upgradeLevelsAmount.Init(_automationsData);

            _upgradeAvailableChecker.Init(_automationsData);


        }

        private void OnEnable()
        {
            for (int i = 0; i < _automationsData.Automations.Count; i++)
                _automationsData.Automations[i].CanUpgradeChanged += OnUpgradeAvailabilityChanged;
            _playerData.GoldChanged += _automationsInput.TryUnlockNewAutomation;
            _automationsData.AutomationsPowerPercentageChanged += OnAutomationsPercentageChanged;
            _automationsData.ClickPowerPercentageChanged += OnClickPowerPercentageChanged;

            _automationsInput.TryUnlockNewAutomation(_playerData.Gold);
            for (int i = 0; i < _automationsData.Automations.Count; i++)
                OnUpgradeAvailabilityChanged(_automationsData.Automations[i].CanUpgrade);
        }

        private void OnDisable()
        {
            for (int i = 0; i < _automationsData.Automations.Count; i++)
                _automationsData.Automations[i].CanUpgradeChanged -= OnUpgradeAvailabilityChanged;
            _playerData.GoldChanged -= _automationsInput.TryUnlockNewAutomation;
            _automationsData.AutomationsPowerPercentageChanged -= OnAutomationsPercentageChanged;
            _automationsData.ClickPowerPercentageChanged -= OnClickPowerPercentageChanged;
        }

        private void OnUpgradeAvailabilityChanged(bool canUpgrade)
        {
            _automationsInput.CheckIfCanUpgradeSomething();
        }

        private void OnClickPowerPercentageChanged(float addedPercentage)
        {
            _automationsInput.RecalculateAutomationsPower(addedPercentage);
        }

        private void OnAutomationsPercentageChanged(float addedPercentage)
        {
            _automationsInput.RecalculateClickPower(addedPercentage);
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

        public void RecalculateAutomationsPower(float percentage)
        {
            _automationsData.AutomationsPower += Mathf.RoundToInt(_automationsData.AutomationsPower * percentage);
        }

        public void RecalculateClickPower(float percentage)
        {
            _automationsData.ClickPower += Mathf.RoundToInt(_automationsData.AutomationsPower * percentage);
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
