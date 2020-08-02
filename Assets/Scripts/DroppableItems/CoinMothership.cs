using UnityEngine;

namespace DroppableItems
{
    public class CoinMothership : DroppingMothership
    {
        public override void Spawn(Vector3 position)
        {
            if (Random.value <= _chanceToSpawn)
            {
                int coinsToSpawn = Random.Range(_amountOfObjectsToSpawn.MinValue, _amountOfObjectsToSpawn.MaxValue);
                long oneCoinCost;
                if (_badgeData.CoinsReward >= coinsToSpawn)
                    oneCoinCost = Mathf.CeilToInt(_badgeData.CoinsReward / coinsToSpawn);
                else
                {
                    coinsToSpawn = 1;
                    oneCoinCost = _badgeData.CoinsReward;
                }

                for (int i = 0; i < coinsToSpawn; i++)
                {
                    GameObject spawnedCoin = Instantiate(_itemToSpawn, position, _itemToSpawn.transform.rotation) as GameObject;
                    ICollectable collectableComponent = spawnedCoin.GetComponent<ICollectable>();
                    SetCoinReward(in oneCoinCost, collectableComponent);
                }
            }
        }

        private void SetCoinReward(in long coinReward, ICollectable collectable)
        {
            collectable.Init(in coinReward, _playerData);
        }
    }
}
