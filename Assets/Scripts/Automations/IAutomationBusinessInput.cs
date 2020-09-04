using UnityEngine.Events;

namespace Automations
{
    public interface IAutomationBusinessInput
    {
        void TryUpgradeAutomation(int automationId, IAutomation automation, UnityEvent automationUnlocked, UnityEvent automationUpgraded);
        void CheckIfUpgradeAvailable(int automationId, long goldValue);
    }
}