using OdinSerializer;
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
        [OdinSerialize] private IAutomation _automation;
        [SerializeField] private UnityEvent _automationUnlocked;

        private AutomationsData _automationsData;
        private PlayerData _playerData;
        private Automation _automationData;

        private AutomationBusinessRules _automationBusinessRules;
        private AutomationPresentator _automationPresentator;
        private AutomationPresentation _automationPresentation;

        public int AutomationId => _automationId;

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

            _automationBusinessRules.CheckIfUpgradeAvailable(_automationId, _playerData.Gold);
            _playerData.GoldChanged += OnGoldAmountUpdated;
            _automationData.CostChanged += FetchCost;
        }

        private void Start()
        {
            _upgradeButton.onClick.AddListener(OnUpgradeButtonPressed);
            if (_automationData.IsUnlocked == false)
                gameObject.SetActive(false);
        }

        public void OnUpgradeButtonPressed()
        {
            _automationBusinessRules.TryUpgradeAutomation(_automationId, _automation);
        }

        public void OnGoldAmountUpdated(int goldAmount)
        {
            //Обновить кнопку улучшения
            _automationBusinessRules.CheckIfUpgradeAvailable(_automationId, goldAmount);
        }

        public void SetAutomationType(IAutomation automation)
        {
            _automation = automation;
        }

        private void FetchCost(int cost)
        {
            _automationPresentation.FetchCost(cost);
        }

        private void OnApplicationQuit()
        {
            _playerData.GoldChanged -= OnGoldAmountUpdated;
            _automationData.CostChanged -= FetchCost;
        }
    }
}