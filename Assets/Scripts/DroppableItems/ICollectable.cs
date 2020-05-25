using Badge;
using System.Collections.Generic;

namespace DroppableItems
{
    public interface ICollectable
    {
        int Collect();
    }

    public interface IDroppable
    {
        IEnumerable<ICollectable> GetObjectsToSpawn(BadgeData badgeData);
    }
}