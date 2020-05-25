using UnityEngine;

namespace Badge.BusinessRules
{
    public interface IBadgeBusinessInput
    {
        void TakeProgress();
        void CreateNewBadge();
        void OnBossNotDefeated();
        void ClickProgress(Vector2 clickPosition);
    }
}