using Badge;
using System.Collections.Generic;
using UnityEngine;

namespace DroppableItems
{
    public class Coin : ICollectable
    {
        private int _costReward;

        public static Coin GetCoin(int costReward)
        {
            return new Coin(costReward);
        }

        private Coin(int costReward)
        {
            _costReward = costReward;
        }

        public int Collect()
        {
            return _costReward;
        }
    }

    public struct AmountOfObjectsToSpawn
    {
        public int MinValue;
        public int MaxValue;
    }

    public class CoinSpawner : IDroppable
    {
        private AmountOfObjectsToSpawn _amountOfObjectsToSpawn;
        private int _costReward;

        public static CoinSpawner GetCoinSpawner(AmountOfObjectsToSpawn amountOfObjectsToSpawn)
        {
            return new CoinSpawner(amountOfObjectsToSpawn);
        }

        private CoinSpawner(AmountOfObjectsToSpawn amountOfObjectsToSpawn)
        {
            _amountOfObjectsToSpawn = amountOfObjectsToSpawn;
        }

        private int AmountToSpawn(BadgeData badgeData)
        {
            int _coinsToSpawn = Random.Range(_amountOfObjectsToSpawn.MinValue, _amountOfObjectsToSpawn.MaxValue);
            if (badgeData.CoinsReward >= _coinsToSpawn)
            {
                _costReward = Mathf.CeilToInt(badgeData.CoinsReward / _coinsToSpawn);
                return _coinsToSpawn;
            }
            else
            {
                _costReward = badgeData.CoinsReward;
                return _coinsToSpawn = 1;
            }
        }

        public IEnumerable<ICollectable> GetObjectsToSpawn(BadgeData badgeData)
        {
            List<ICollectable> coinsToSpawn = new List<ICollectable>();
            for (int i = 0; i < AmountToSpawn(badgeData); i++)
            {
                ICollectable coin = Coin.GetCoin(_costReward);
                coinsToSpawn.Add(coin);
            }

            return coinsToSpawn;
        }
    }
}