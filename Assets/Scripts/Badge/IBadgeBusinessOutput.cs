namespace Badge
{
    public interface IBadgeBusinessOutput
    {
        void OnBadgeCreated(BadgeData badgeData);
        void BadgeGotProgressCallback(BadgeData badgeData);
    }
}