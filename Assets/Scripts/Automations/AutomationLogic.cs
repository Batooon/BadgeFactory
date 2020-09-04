using OdinSerializer;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Automations
{
    [RequireComponent(typeof(AutomationPresentation)), RequireComponent(typeof(IAutomation))]
    public class AutomationLogic : SerializedMonoBehaviour
    {
        [SerializeField] private int _automationId;
        [SerializeField] private List<UpgradeComponent> _upgradeComponents;
        [SerializeField] private UnityEvent _automationUnlocked;
        [SerializeField] private UnityEvent _automationUpgraded;

        private AutomationsData _automationsData;
        private PlayerData _playerData;
        private Automation _automationData;

        private AutomationBusinessRules _automationBusinessRules;
        private AutomationPresentator _automationPresentator;
        private AutomationPresentation _automationPresentation;
        private IAutomation _automation;

        public int AutomationId => _automationId;

        public void Init(PlayerData playerData, AutomationsData automationsData, Automation automationData)
        {
            _automation = GetComponent<IAutomation>();
            _playerData = playerData;
            _automationsData = automationsData;
            _automationData = automationData;

            _automationPresentation = GetComponent<AutomationPresentation>();
            _automationPresentator = new AutomationPresentator(_automationPresentation, _automationData);

            _automationBusinessRules = new AutomationBusinessRules(
                _automationPresentator,
                _playerData,
                _automationData,
                _automationsData);

            for (int i = 0; i < _upgradeComponents.Count; i++)
                _upgradeComponents[i].Init(_playerData, _automationsData, _automationData, _automationData.UpgradeComponents[i], _automationId);
            _automationPresentation.Init(_automationData);
        }

        private void OnEnable()
        {
            _playerData.GoldChanged += OnGoldAmountUpdated;
            _automationData.CostChanged += FetchCost;
            _automationsData.LevelsToUpgradeChanged += RecalculateCost;
            _automationData.PowerUpPercentageChanged += OnAutomationPowerChanged;

            OnGoldAmountUpdated(_playerData.Gold);
            FetchCost(_automationData.CurrentCost);
            RecalculateCost(_automationsData.LevelsToUpgrade);
        }

        private void OnDisable()
        {
            _playerData.GoldChanged -= OnGoldAmountUpdated;
            _automationData.CostChanged -= FetchCost;
            _automationsData.LevelsToUpgradeChanged -= RecalculateCost;
            _automationData.PowerUpPercentageChanged -= OnAutomationPowerChanged;
        }

        private void Start()
        {
            if (_automationData.IsUnlocked == false)
                gameObject.SetActive(false);
        }

        public void OnUpgradeButtonPressed()
        {
            _automationBusinessRules.TryUpgradeAutomation(_automationId, _automation, _automationUnlocked, _automationUpgraded);
        }

        public void OnGoldAmountUpdated(long goldAmount)
        {
            _automationBusinessRules.CheckIfUpgradeAvailable(_automationId, goldAmount);
        }

        private void OnAutomationPowerChanged(float percentage)
        {
            _automationBusinessRules.RecalculateAutomationPower(_automationId, percentage);
        }

        private void FetchCost(long cost)
        {
            _automationPresentation.FetchCost(cost);
        }

        private void RecalculateCost(int levelsToUpgrade)
        {
            _automation.RecalculateCost(levelsToUpgrade, _automationData);
            _automationBusinessRules.CheckIfUpgradeAvailable(_automationId, _playerData.Gold);
        }
    }
}