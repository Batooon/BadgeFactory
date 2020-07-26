using UnityEngine;

namespace BadgeImplementation
{
    public interface IBadgeBusinessOutput
    {
        void OnBadgeCreated(BadgeData badgeData);
        void BadgeGotProgressCallback(BadgeData badgeData);
        void SpawnBadge();
        void SpawnBoss();
    }
}