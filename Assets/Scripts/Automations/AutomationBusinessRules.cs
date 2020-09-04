using UnityEngine;
using UnityEngine.Events;

namespace Automations
{
    public class AutomationBusinessRules : IAutomationBusinessInput
    {
        private Automation _automation;
        private PlayerData _playerData;
        private AutomationsData _automationsData;
        private IAutomationBusinessOutput _automationOutput;

        public AutomationBusinessRules(IAutomationBusinessOutput automationBusinessOutput,
            PlayerData playerData,
            Automation automation,
            AutomationsData automationsData)
        {
            _automation = automation;
            _playerData = playerData;
            _automationOutput = automationBusinessOutput;
            _automationsData = automationsData;
        }

        public void CheckIfUpgradeAvailable(int automationId, long goldValue)
        {
            _automation.CanUpgrade = goldValue >= _automation.CurrentCost;

            _automationOutput.FetchUpgradeButton(_automation.CanUpgrade);
        }

        public void TryUpgradeAutomation(int automationId, IAutomation automation, UnityEvent automationUnlocked, UnityEvent automationUpgraded)
        {
            if (_playerData.Gold >= _automation.CurrentCost)
            {
                _playerData.Gold -= _automation.CurrentCost;
                automation.Upgrade(_automation, _automationsData);
                if (_automation.Level % 2000 == 0)
                    _playerData.BadgePoints += 1;
                automationUpgraded?.Invoke();
                _automationOutput.AutomationUpgraded(_automation, _playerData.Gold >= _automation.CurrentCost);
            }
            else
            {
                _automationOutput.AutomationNotUpgraded();
            }
        }

        public void RecalculateAutomationPower(int automationId, float addPercentageAmount)
        {
            float percentage = addPercentageAmount / 100;
            long damage = _automationsData.Automations[automationId].CurrentDamage;
            _automationsData.Automations[automationId].CurrentDamage += Mathf.RoundToInt(damage * percentage);
        }
    }
}