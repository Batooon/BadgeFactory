﻿using Badge;
using System.Collections.Generic;
using UnityEngine;

namespace DroppableItems
{
    public class Coin : MonoBehaviour, ICollectable
    {
        [SerializeField]
        private float _lifetime;

        private PlayerDataAccess _playerDataProvider;
        private IItemTweener _itemTweener;

        private int _costReward;

        public void Collect()
        {
            Data playerData = _playerDataProvider.GetPlayerData();
            playerData.GoldAmount += _costReward;
            _playerDataProvider.PlayerData = playerData;
            //_playerDataProvider.SavePlayerData(in playerData);
        }

        public void SetReward(in int reward)
        {
            _costReward = reward;
        }

        private void Awake()
        {
            _playerDataProvider = PlayerDataAccess.GetPlayerDatabase();
            _itemTweener = GetComponent<IItemTweener>();
            _itemTweener.StartMotion();
            Destroy(gameObject, _lifetime);
        }

        private void OnMouseEnter()
        {
            Data playerData = _playerDataProvider.GetPlayerData();
            Collect();
            Destroy(gameObject);
        }
    }

    [System.Serializable]
    public struct AmountOfObjectsToSpawn
    {
        public int MinValue;
        public int MaxValue;
    }
}