using Automation;
using UnityEngine;

namespace Automations
{
    public class Automations : MonoBehaviour
    {
        private PlayerDataAccess _playerData;
        private IAutomationsBusinessInput _automationsInput;
        private AutomationsPresentation _automationPresentation;

        public void OnGoldAmountChanged()
        {

        }

        private void Awake()
        {
            _playerData = PlayerDataAccess.GetPlayerDatabase();
            _automationPresentation = GetComponent<AutomationsPresentation>();

            _automationsInput = new AutomationsBusinessRules(
                AutomationDatabse.GetAutomaitonDatabase(),
                new AutomationsPresentator(_automationPresentation),
                _playerData);
        }

        private void OnEnable()
        {
            _playerData.GoldAmountChanged += OnGoldAmountChanged;
        }

        private void OnDisable()
        {
            _playerData.GoldAmountChanged -= OnGoldAmountChanged;
        }
    }

    public interface IAutomationsBusinessInput
    {
        void TryUnlockNewAutomation();
    }

    public class AutomationsBusinessRules : IAutomationsBusinessInput
    {
        private IPlayerDataProvider _playerData;
        private IAutomationDatabase _automationDatabase;
        private IAutomationsBusinessOutput _automationsOutput;

        public AutomationsBusinessRules(IAutomationDatabase automationDatabase,
                                        IAutomationsBusinessOutput automationOutput,
                                        IPlayerDataProvider playerData)
        {
            _playerData = playerData;
            _automationDatabase = automationDatabase;
            _automationsOutput = automationOutput;
        }

        public void TryUnlockNewAutomation()
        {
            int lastUnlockedAutomationId = _automationDatabase.GetLastUnlockedAutomationId();
            int newAutomationId = lastUnlockedAutomationId + 1;
            CurrentPlayerAutomationData newAutomationData = _automationDatabase.GetAutomationData(newAutomationId);
            CurrentPlayerAutomationData currentAutomation = _automationDatabase.GetAutomationData(lastUnlockedAutomationId);
            Data playerData = _playerData.GetPlayerData();

            if(newAutomationData.IsUnlocked)
                return;

            if (playerData.GoldAmount >= currentAutomation.Cost)
            {
                if (_automationDatabase.GetAutomationsLength() >= newAutomationId)
                    _automationsOutput.UnlockNewAutomation(newAutomationId); 
            }
        }
    }

    public interface IAutomationsBusinessOutput
    {
        void UnlockNewAutomation(int newAutomationId);
    }

    public class AutomationsPresentator : IAutomationsBusinessOutput
    {
        private AutomationsPresentation _automationPresentation;

        public AutomationsPresentator(AutomationsPresentation automationPresentation)
        {
            _automationPresentation = automationPresentation;
        }

        public void UnlockNewAutomation(int newAutomationId)
        {
            _automationPresentation.UnlockNewAutomation(newAutomationId);
        }
    }
}
