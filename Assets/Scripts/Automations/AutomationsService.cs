using System.Numerics;
using UnityEngine;

namespace Automations
{
    [RequireComponent(typeof(AutomationsPresentation))]
    public class AutomationsService : IObserver
    {
        /*private readonly AutomationsUpgradeAvailableChecker _upgradeAvailableChecker;
        private readonly UpgradeLevelsAmount _upgradeLevelsAmount;*/

        private readonly PlayerData _playerData;
        private readonly AutomationsData _automationsData;
        private AutomationsPresentation _automationPresentation;

        public AutomationsService(PlayerData playerData, AutomationsData automationsData)
        {
            _playerData = playerData;
            _automationsData = automationsData;
            
            _automationsData.Attach(this);
            _playerData.Attach(this);
            for (int i = 0; i < _automationsData.Automations.Count; i++)
                _automationsData.Automations[i].Attach(this);

            /*foreach (Transform automation in transform)
            {
                AutomationLogic automationLogic = automation.GetComponent<AutomationLogic>();
                automationLogic.Init(_playerData, _automationsData, _automationsData.Automations[automationLogic.AutomationId]);
            }*/

            //_upgradeLevelsAmount.Init(_automationsData);

            //_upgradeAvailableChecker.Init(_automationsData);
            
            TryUnlockNewAutomation(_playerData.Gold);
            CheckIfCanUpgradeSomething();
        }

        ~AutomationsService()
        {
            _automationsData.Detach(this);
            _playerData.Detach(this);
            foreach (var automation in _automationsData.Automations)
                automation.Detach(this);
        }

        public void Fetch(ISubject subject)
        {
            switch (subject)
            {
                case PlayerData playerData:
                    TryUnlockNewAutomation(playerData.Gold);
                    break;
                case AutomationsData automationsData:
                    //RecalculateClickPower(automationsData.AutomationsPowerPercentageIncrease);
                    //RecalculateAutomationsPower(automationsData.CriticalPowerIncreasePercentage);
                    break;
                case Automation automation:
                    CheckIfCanUpgradeSomething();
                    break;
            }
        }

        private void CheckIfCanUpgradeSomething()
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

        private void TryUnlockNewAutomation(BigInteger newGoldAmount)
        {
            int lastUnlockedAutomationId = _automationsData.GetLastUnlockedAutomationId();
            int newAutomationId = lastUnlockedAutomationId + 1;

            if (newAutomationId >= _automationsData.Automations.Count)
                return;

            Automation newAutomationData = _automationsData.Automations[newAutomationId];
            Automation currentAutomation = _automationsData.Automations[lastUnlockedAutomationId];

            if (newGoldAmount >= currentAutomation.CurrentCost)
                newAutomationData.IsUnlocked = true;
        }

        /*private void RecalculateAutomationsPower(float percentage)
        {
            _automationsData.AutomationsPower += Mathf.RoundToInt(_automationsData.AutomationsPower * percentage);
        }*/

        /*private void RecalculateClickPower(float percentage)
        {
            _automationsData.ClickPower += Mathf.RoundToInt(_automationsData.AutomationsPower * percentage);
        }*/
    }
}
