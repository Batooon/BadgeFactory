namespace Badge
{
    public interface IBadgeDatabase
    {
        BadgeData GetBadgeData();
        void SaveBadgeData();
    }
}
