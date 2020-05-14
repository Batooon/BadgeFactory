using System;
using OdinSerializer;
using UnityEngine;
using UnityEngine.UI;

namespace Automation
{
    public class AutomationLogic : SerializedMonoBehaviour
    {
        [SerializeField]
        private Button _upgradeButton;

        private int _automationId;

        [OdinSerialize]
        private IAutomation _automation;

        private IAutomationBusinessInput _automationInput;
        private AutomationPresentator _automationPresentator;
        private PlayerDataAccess _playerData;

        private void Awake()
        {
            AutomationPresentation automationPresentation = GetComponent<AutomationPresentation>();
            _automationPresentator = new AutomationPresentator(automationPresentation);
            _playerData = PlayerDataAccess.GetPlayerDatabase();

            _automationInput = new AutomationBusinessRules(
                _automationPresentator,
                _playerData,
                AutomationDatabse.GetAutomaitonDatabase());

            _automationInput.CheckIfUpgradeAvailable(_automationId);
        }

        private void OnEnable()
        {
            _playerData.GoldAmountChanged += OnGoldAmountUpdated;
        }

        private void OnDisable()
        {
            _playerData.GoldAmountChanged -= OnGoldAmountUpdated;
        }

        private void Start()
        {
            _upgradeButton.onClick.AddListener(OnUpgradeButtonPressed);
        }

        public void OnUpgradeButtonPressed()
        {
            _automationInput.TryUpgradeAutomation(_automationId, _automation);
        }

        public void OnGoldAmountUpdated()
        {
            //Обновить кнопку улучшения
            _automationInput.CheckIfUpgradeAvailable(_automationId);
        }

        public void SetAutomationType(IAutomation automation)
        {
            _automation = automation;
        }
    }
}