using UnityEngine;

namespace BadgeImplementation.BusinessRules
{
    public interface IBadgeBusinessInput
    {
        void TakeProgress();
        void CreateNewBadge();
        void OnBossNotDefeated();
        void ClickProgress();
    }
}