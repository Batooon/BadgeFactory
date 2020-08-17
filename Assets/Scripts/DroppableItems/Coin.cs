﻿using UnityEngine;
using UnityEngine.Events;

namespace DroppableItems
{
    public class Coin : MonoBehaviour, ICollectable
    {
        [SerializeField] private ParticleSystem _collectEffect;
        [SerializeField] private UnityEvent _collect;

        private PlayerData _playerData;
        private IItemTweener _itemTweener;
        private Transform _transform;

        private long _costReward;

        public void Init(in long reward, PlayerData playerData)
        {
            _costReward = reward;
            _playerData = playerData;
            _itemTweener = GetComponent<IItemTweener>();
            _transform = GetComponent<Transform>();
            _itemTweener.StartMotion();
        }

        public void OnMovingEnded()
        {
            _collect?.Invoke();
            GameObject effect = Instantiate(_collectEffect.gameObject, _transform.position, _transform.rotation);
            Destroy(effect, _collectEffect.main.duration);
            Collect();
            gameObject.SetActive(false);
        }

        private void Collect()
        {
            _playerData.Gold += _costReward;
        }
    }

    [System.Serializable]
    public struct AmountOfObjectsToSpawn
    {
        public int MinValue;
        public int MaxValue;
    }
}