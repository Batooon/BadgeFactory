using UnityEngine;

namespace DroppableItems
{
    public interface ICollectable
    {
        void Init(in int reward, PlayerData playerData);
        void OnMovingEnded();
    }

    public interface IItemsMothership
    {
        void Spawn(Vector3 position);
    }
}