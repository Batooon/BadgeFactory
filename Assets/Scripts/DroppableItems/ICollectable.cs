using UnityEngine;

namespace DroppableItems
{
    public interface ICollectable
    {
        void SetReward(in int reward);
        void Collect();
    }

    public interface IItemsMothership
    {
        void Spawn(Vector2 position);
    }
}