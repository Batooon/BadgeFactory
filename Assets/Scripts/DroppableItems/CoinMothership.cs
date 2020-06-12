using Badge;
using System.Collections.Generic;
using UnityEngine;

namespace DroppableItems
{
    public class CoinMothership : DroppingMothership
    {
        private IBadgeDatabase _badgeDatabase;

        private void Awake()
        {
            _badgeDatabase = BadgeDatabaseAccess.GetBadgeDatabase();
        }

        public override void Spawn(Vector2 position)
        {
            BadgeData badgeData = _badgeDatabase.GetBadgeData();

            if (Random.Range(0, 100) <= _chanceToSpawn)
            {
                int coinsToSpawn = Random.Range(_amountOfObjectsToSpawn.MinValue, _amountOfObjectsToSpawn.MaxValue);
                int oneCoinCost;
                if (badgeData.CoinsReward >= coinsToSpawn)
                    oneCoinCost = Mathf.CeilToInt(badgeData.CoinsReward / coinsToSpawn);
                else
                {
                    coinsToSpawn = 1;
                    oneCoinCost = badgeData.CoinsReward;
                }

                for (int i = 0; i < coinsToSpawn; i++)
                {
                    GameObject spawnedCoin = Instantiate(_itemToSpawn, position, Quaternion.identity) as GameObject;
                    ICollectable collectableComponent = spawnedCoin.GetComponent<ICollectable>();
                    SetCoinReward(in oneCoinCost, collectableComponent);
                }
            }
        }

        private void SetCoinReward(in int coinReward, ICollectable collectable)
        {
            collectable.SetReward(in coinReward);
        }
    }
}
