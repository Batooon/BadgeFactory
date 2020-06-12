using Badge;
using UnityEngine;
using System.Collections.Generic;

namespace DroppableItems
{
    public abstract class DroppingMothership : MonoBehaviour, IItemsMothership
    {
        [SerializeField]
        protected AmountOfObjectsToSpawn _amountOfObjectsToSpawn;
        [SerializeField]
        protected int _chanceToSpawn;
        /*[SerializeField]
        protected float _itemLifetime;*/
        /*[SerializeField]
        [RequireInterface(typeof(ICollectable))]*/
        //protected ICollectable _collectableItem;
        [Header("ICollectable component required!")]
        [SerializeField]
        protected GameObject _itemToSpawn;

        public abstract void Spawn(Vector2 position);

        /*private void Awake()
        {
            _collectableItem = _itemToSpawn.GetComponent<ICollectable>();
        }*/
    }
}
