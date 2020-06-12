using OdinSerializer;
using UnityEngine;
using UnityEngine.UI;

namespace Automation
{
    [RequireComponent(typeof(AutomationPresentation))]
    public class AutomationLogic : SerializedMonoBehaviour
    {
        [SerializeField]
        private Button _upgradeButton;

        [SerializeField]
        private int _automationId;

        [OdinSerialize]
        private IAutomation _automation;

        private IAutomationBusinessInput _automationInput;
        private AutomationPresentator _automationPresentator;
        private PlayerDataAccess _playerData;
        private AutomationDatabse _automationDatabase;
        private AutomationPresentation _automationPresentation;

        private void Awake()
        {
            _automationDatabase = AutomationDatabse.GetAutomationDatabase();
            _automationPresentation = GetComponent<AutomationPresentation>();
            _automationPresentator = new AutomationPresentator(_automationPresentation, _automationDatabase.GetAutomationData(_automationId));
            _playerData = PlayerDataAccess.GetPlayerDatabase();

            _automationInput = new AutomationBusinessRules(
                _automationPresentator,
                _playerData,
                _automationDatabase);

            _automationInput.CheckIfUpgradeAvailable(_automationId, _playerData.GetPlayerData().GoldAmount);
            _playerData.PlayerData.GoldAmountChanged += OnGoldAmountUpdated;
        }

        private void Start()
        {
            _upgradeButton.onClick.AddListener(OnUpgradeButtonPressed);
            if (!_automationDatabase.GetAutomationData(_automationId).IsUnlocked)
                gameObject.SetActive(false);
        }

        public void OnUpgradeButtonPressed()
        {
            _automationInput.TryUpgradeAutomation(_automationId, _automation);
        }

        public void OnGoldAmountUpdated(int goldAmount)
        {
            //Обновить кнопку улучшения
            _automationInput.CheckIfUpgradeAvailable(_automationId, goldAmount);
        }

        public void SetAutomationType(IAutomation automation)
        {
            _automation = automation;
        }

        private void OnApplicationQuit()
        {
            _playerData.PlayerData.GoldAmountChanged -= OnGoldAmountUpdated;
        }
    }
}