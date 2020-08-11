﻿using OdinSerializer;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace AutomationImplementation
{
    [RequireComponent(typeof(AutomationPresentation))]
    public class AutomationLogic : SerializedMonoBehaviour
    {
        [SerializeField] private Button _upgradeButton;
        [SerializeField] private int _automationId;
        [SerializeField] private List<UpgradeComponent> _upgradeComponents;
        [SerializeField, RequireInterface(typeof(IAutomation))] private Object _automation;
        [SerializeField] private UnityEvent _automationUnlocked;

        private AutomationsData _automationsData;
        private PlayerData _playerData;
        private Automation _automationData;

        private AutomationBusinessRules _automationBusinessRules;
        private AutomationPresentator _automationPresentator;
        private AutomationPresentation _automationPresentation;

        public int AutomationId => _automationId;

        public IAutomation Automation => _automation as IAutomation;

        public void Init(PlayerData playerData, AutomationsData automationsData, Automation automationData)
        {
            _playerData = playerData;
            _automationsData = automationsData;
            _automationData = automationData;

            _automationPresentation = GetComponent<AutomationPresentation>();
            _automationPresentation.Init(_automationData);
            _automationPresentator = new AutomationPresentator(_automationPresentation, _automationData);

            _automationBusinessRules = new AutomationBusinessRules(
                _automationPresentator,
                _playerData,
                _automationData,
                _automationsData);

            foreach (var component in _upgradeComponents)
                component.Init(_playerData, _automationsData, _automationData, _automationId);

            _automationBusinessRules.CheckIfUpgradeAvailable(_automationId, _playerData.Gold);

            _playerData.GoldChanged += OnGoldAmountUpdated;
            _automationData.CostChanged += FetchCost;
            _automationsData.LevelsToUpgradeChanged += RecalculateCost;
            _automationData.PowerUpPercentageChanged += OnAutomationPowerChanged;

            OnGoldAmountUpdated(_playerData.Gold);
            FetchCost(_automationData.CurrentCost);
            RecalculateCost(_automationsData.LevelsToUpgrade);
        }

        private void OnEnable()
        {
            if (_playerData == null)
                return;
            if (_automationsData == null)
                return;
            if (_automationData == null)
                return;

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
            _upgradeButton.onClick.AddListener(OnUpgradeButtonPressed);
            if (_automationData.IsUnlocked == false)
                gameObject.SetActive(false);
        }

        public void OnUpgradeButtonPressed()
        {
            _automationBusinessRules.TryUpgradeAutomation(_automationId, Automation, _automationUnlocked);
        }

        public void OnGoldAmountUpdated(long goldAmount)
        {
            if (_automationBusinessRules == null)
                return;

            _automationBusinessRules.CheckIfUpgradeAvailable(_automationId, goldAmount);
        }

        private void OnAutomationPowerChanged(int percentage)
        {
            _automationBusinessRules.RecalculateAutomationPower(_automationId, percentage);
        }

        private void FetchCost(long cost)
        {
            _automationPresentation.FetchCost(cost);
        }

        private void RecalculateCost(int levelsToUpgrade)
        {
            Automation.RecalculateCost(levelsToUpgrade, _automationData);
            if (_automationBusinessRules == null)
                return;
            _automationBusinessRules.CheckIfUpgradeAvailable(_automationId, _playerData.Gold);
        }

        public void SetAutomationType(IAutomation automation)
        {
            //Automation = automation;
        }
    }
}