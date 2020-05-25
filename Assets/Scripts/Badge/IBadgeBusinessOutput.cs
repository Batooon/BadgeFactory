using UnityEngine;

namespace Badge
{
    public interface IBadgeBusinessOutput
    {
        void OnBadgeCreated(BadgeData badgeData);
        void BadgeGotProgressCallback(BadgeData badgeData);
        void SpawnBadge();
        void SpawnBoss();
        void PlayerClicked(Vector2 clickPosition);
    }
}