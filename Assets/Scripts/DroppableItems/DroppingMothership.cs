using UnityEngine;

namespace DroppableItems
{
    public abstract class DroppingMothership : MonoBehaviour, IItemsMothership
    {
        [SerializeField] protected AmountOfObjectsToSpawn _amountOfObjectsToSpawn;
        [SerializeField, Range(0f, 1f)] protected float _chanceToSpawn;
        [Header("ICollectable component required!")]
        [SerializeField] protected GameObject _itemToSpawn;

        protected BadgeData _badgeData;
        protected PlayerData _playerData;
        public void Init(BadgeData badgeData, PlayerData playerData)
        {
            _badgeData = badgeData;
            _playerData = playerData;
        }

        public abstract void Spawn(Vector3 position);
    }
}
