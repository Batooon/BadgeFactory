using Automation;
using UnityEngine;

namespace Automations
{
    [RequireComponent(typeof(AutomationsPresentation))]
    public class Automations : MonoBehaviour
    {
        private PlayerDataAccess _playerData;
        private AutomationDatabse _automationsDatabase;
        private IAutomationsBusinessInput _automationsInput;
        private AutomationsPresentation _automationPresentation;
        private IAutomationsBusinessOutput _automationsOutput;

        public void OnGoldAmountChanged(int newAmount)
        {
            //_automationsInput.CheckIfCanUpgradeSomething();
        }

        private void OnUpgradeAvailabilityChanged()
        {
            _automationsInput.CheckIfCanUpgradeSomething();
        }

        private void Awake()
        {
            _automationsDatabase = AutomationDatabse.GetAutomationDatabase();
            _playerData = PlayerDataAccess.GetPlayerDatabase();
            _automationPresentation = GetComponent<AutomationsPresentation>();
            _automationsOutput = new AutomationsPresentator(_automationPresentation, _automationsDatabase.GetOverallAutomationsData());

            _automationsInput = new AutomationsBusinessRules(
                _automationsDatabase,
                _automationsOutput,
                _playerData);

            _playerData.PlayerData.GoldAmountChanged += OnGoldAmountChanged;
            _playerData.PlayerData.GoldAmountChanged += _automationsInput.TryUnlockNewAutomation;
            _automationsDatabase.GetOverallAutomationsData().AutomationsPowerChanged += _automationsOutput.AutomationsPowerUpdated;
            _automationsDatabase.GetOverallAutomationsData().ClickPowerChanged += _automationsOutput.ClickPowerUpdated;

            for (int i = 0; i < _automationsDatabase.GetAutomationsLength(); i++)
            {
                _automationsDatabase.GetAutomationData(i).UpgradeAvailabilityChanged += OnUpgradeAvailabilityChanged;
            }
        }

        private void OnApplicationQuit()
        {
            AutomationDatabse.GetAutomationDatabase().Serialize();
            _playerData.PlayerData.GoldAmountChanged -= _automationsInput.TryUnlockNewAutomation;
            _automationsDatabase.GetOverallAutomationsData().AutomationsPowerChanged -= _automationsOutput.AutomationsPowerUpdated;
            _automationsDatabase.GetOverallAutomationsData().ClickPowerChanged -= _automationsOutput.ClickPowerUpdated;
        }

        private void OnApplicationPause(bool pause)
        {
            if (pause)
            {
                AutomationDatabse.GetAutomationDatabase().Serialize();
                _playerData.PlayerData.GoldAmountChanged -= _automationsInput.TryUnlockNewAutomation;
                _automationsDatabase.GetOverallAutomationsData().AutomationsPowerChanged -= _automationsOutput.AutomationsPowerUpdated;
                _automationsDatabase.GetOverallAutomationsData().ClickPowerChanged -= _automationsOutput.ClickPowerUpdated;
                _playerData.PlayerData.GoldAmountChanged -= OnGoldAmountChanged;
            }
        }
    }

    public interface IAutomationsBusinessInput
    {
        void TryUnlockNewAutomation(int newGoldAmount);
        void CheckIfCanUpgradeSomething();
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

        public void CheckIfCanUpgradeSomething()
        {
            for (int i = 0; i < _automationDatabase.GetAutomationsLength(); i++)
            {
                if (_automationDatabase.GetAutomationData(i).CanUpgrade)
                {
                    _automationDatabase.GetOverallAutomationsData().CanUpgradeSomething = true;
                    return;
                }
            }
            _automationDatabase.GetOverallAutomationsData().CanUpgradeSomething = false;
        }

        public void TryUnlockNewAutomation(int newGoldAmount)
        {
            int lastUnlockedAutomationId = _automationDatabase.GetLastUnlockedAutomationId();
            int newAutomationId = lastUnlockedAutomationId + 1;
            if (newAutomationId == _automationDatabase.GetAutomationsLength())
                return;
            CurrentPlayerAutomationData newAutomationData = _automationDatabase.GetAutomationData(newAutomationId);
            CurrentPlayerAutomationData currentAutomation = _automationDatabase.GetAutomationData(lastUnlockedAutomationId);

            if(newAutomationData.IsUnlocked)
                return;

            if (newGoldAmount >= currentAutomation.Cost)
            {
                if (_automationDatabase.GetAutomationsLength() > newAutomationId)
                {
                    Data playerData = _playerData.GetPlayerData();
                    newAutomationData.IsUnlocked = true;
                    playerData.AutomationsAmountUnlocked += 1;
                    _automationsOutput.UnlockNewAutomation(newAutomationId);
                    //_automationDatabase.SaveAutomationData(newAutomationData, newAutomationId);
                }
            }
        }
    }

    public interface IAutomationsBusinessOutput
    {
        void UnlockNewAutomation(int newAutomationId);
        void ClickPowerUpdated(int newPower);
        void AutomationsPowerUpdated(int newPower);
    }

    public class AutomationsPresentator : IAutomationsBusinessOutput
    {
        private AutomationsPresentation _automationsPresentation;

        public AutomationsPresentator(AutomationsPresentation automationsPresentation, OverallAutomationsData overallAutomationsData)
        {
            _automationsPresentation = automationsPresentation;
            _automationsPresentation.SetUIValues(overallAutomationsData.ClickPower.ConvertValue(), overallAutomationsData.AutomationsPower.ConvertValue());
        }

        public void AutomationsPowerUpdated(int newPower)
        {
            _automationsPresentation.UpdateAutomationsPower(newPower.ConvertValue());
        }

        public void ClickPowerUpdated(int newPower)
        {
            _automationsPresentation.UpdateClickPower(newPower.ConvertValue());
        }

        public void UnlockNewAutomation(int newAutomationId)
        {
            _automationsPresentation.UnlockNewAutomation(newAutomationId);
        }
    }
}
