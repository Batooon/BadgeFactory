namespace Automations
{
    public interface IAutomationBusinessOutput
    {
        void AutomationUpgraded(Automation autoamtionData, bool isEnougshMoney);
        void AutomationNotUpgraded();
        void UnlockNewAutomation();
        void FetchUpgradeButton(bool isInteractable);
    }
}