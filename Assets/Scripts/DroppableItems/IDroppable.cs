using Badge;

namespace DroppableItems
{
    public interface IDroppable
    {
        void Collect(ref Data playerData);
        int AmountToSpawn(BadgeData badgeData);
    }
}