using UnityEngine;

namespace DroppableItems
{
    public interface ICollectable
    {
        void Init(in long reward, PlayerData playerData, AudioService audioService);
        void OnMovingEnded();
    }

    public interface IItemsMothership
    {
        void Spawn(Vector3 position);
    }
}