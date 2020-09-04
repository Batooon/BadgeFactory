using UnityEngine;

namespace Badge
{
    public interface IBadgeBusinessInput
    {
        void TakeProgress();
        void CreateNewBadge();
        void OnBossNotDefeated();
        void ClickProgress();
    }
}