using UnityEngine.Events;

namespace Automations
{
    public interface IAutomationBusinessInput
    {
        void TryUpgradeAutomation(int automationId, IAutomation automation, UnityEvent automationUpgraded);
        void CheckIfUpgradeAvailable(int automationId, long goldValue);
    }
}