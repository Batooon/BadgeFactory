using Badge;
using UnityEngine;

namespace DroppableItems
{
    public class Coin : MonoBehaviour, IDroppable
    {
        private int _coinsToSpawn;
        private int _costReward;

        public int AmountToSpawn(BadgeData badgeData)
        {
            _coinsToSpawn = Random.Range(3, 5);
            if (badgeData.CoinsReward >= _coinsToSpawn)
            {
                _costReward = Mathf.CeilToInt(badgeData.CoinsReward / _coinsToSpawn);
                return _coinsToSpawn;
            }
            else
            {
                _costReward=badgeData.CoinsReward;
                return _coinsToSpawn = 1;
            }
        }

        public void Collect(ref Data playerData)
        {
            playerData.GoldAmount += _costReward;
        }
    }
}