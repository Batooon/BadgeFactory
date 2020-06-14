using UnityEngine;

namespace DroppableItems
{
    public interface IItemTweener
    {
        void StartMotion();
        void SetDestination(Vector2 destination);
    }
}