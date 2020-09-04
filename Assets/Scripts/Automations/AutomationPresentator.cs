using System;

namespace Automations
{
    public class AutomationPresentator : IAutomationBusinessOutput
    {
        private AutomationPresentation _automationPresentation;

        public AutomationPresentator(AutomationPresentation automationPresentation, Automation automationData)
        {
            _automationPresentation = automationPresentation;
            SetUpAutomation(automationData);
        }

        public void AutomationNotUpgraded()
        {
            _automationPresentation.OnAutomationNotUpgraded();
        }

        public void AutomationUpgraded(Automation automationData, bool isEnoughMoneyForNextUpgrade)
        {
            _automationPresentation.OnAutomationUpgraded(GetAutomationParams(automationData, isEnoughMoneyForNextUpgrade));
        }

        public void FetchUpgradeButton(bool isInteractable)
        {
            _automationPresentation.FetchUpgradeButton(isInteractable);
        }

        public void UnlockNewAutomation()
        {
            throw new NotImplementedException();
        }

        private void SetUpAutomation(Automation automationData)
        {
            _automationPresentation.SetUpAutomation(GetAutomationParams(automationData, automationData.CanUpgrade));
        }

        private AutomationViewModel GetAutomationParams(Automation automationData, bool isEnoughMoneyForNextUpgrade)
        {
            AutomationViewModel automationParams;
            automationParams.AutomationCost = automationData.CurrentCost.ConvertValue();
            automationParams.AutomationDamage = automationData.CurrentDamage.ConvertValue();
            automationParams.IsEnoughMoney = isEnoughMoneyForNextUpgrade;
            return automationParams;
        }
    }
}