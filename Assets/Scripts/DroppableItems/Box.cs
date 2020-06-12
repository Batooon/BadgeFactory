using System;
using UnityEngine;

namespace DroppableItems
{
    public class Box : MonoBehaviour
    {
        public event Action<Vector2> BoxOpened;

        [SerializeField]
        private int _hp;
        [SerializeField]
        private int _oneHitDamage;
        [SerializeField]
        private float _lifetime;
        [SerializeField]
        private GameObject _boxOpenedEffect;

        private IItemTweener _itemTweener;

        private void Awake()
        {
            _itemTweener = GetComponent<IItemTweener>();
        }

        private void Start()
        {
            _itemTweener.StartMotion();
            Destroy(gameObject, _lifetime);
        }

        private void OnMouseEnter()
        {
            _hp -= _oneHitDamage;
            if (_hp <= 0)
            {
                GameObject effect = Instantiate(_boxOpenedEffect, transform.position, Quaternion.identity);
                BoxOpened?.Invoke(transform.position);
                Destroy(gameObject);
            }
        }

        private void OnDestroy()
        {
            BoxOpened = null;
        }
    }
}
