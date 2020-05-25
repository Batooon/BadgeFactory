using System;
using UnityEngine;
using System.Collections.Generic;
using DroppableItems;
using UnityEngine.U2D;

namespace Badge
{
    public class BadgePresentator : IBadgeBusinessOutput
    {
        public event Action<IBadgeDatabase> BadgeCreated;

        private BadgePresentation _badgePresentation;
        private List<DroppableObject> _droppable;
        private List<GameObject> _itemsToDrop;
        private Sprite[] _badges;
        private Sprite[] _bossBadges;

        private IBadgeSpawner _badgeSpawner;
        private IClickEffect _clickEffect;

        private int _coinsToSpawn;
        private int _oneCoinCost;

        public BadgePresentator(BadgePresentation badgePresentation,
                                List<DroppableObject> droppable,
                                Sprite[] badges,
                                Sprite[] bosses,
                                IClickEffect clickEffect)
        {
            _badgePresentation = badgePresentation;
            _droppable = droppable;
            _badges = badges;
            _bossBadges = bosses;
            _badgeSpawner = new UsualBadgeSpawner();
            _clickEffect=clickEffect;
        }

        public void BadgeGotProgressCallback(BadgeData badgeData)
        {
            float alpha = Mathf.Clamp01(Mathf.InverseLerp(0, badgeData.MaxHp, badgeData.CurrentHp));

            _badgePresentation.UpdateBadgeProgress(alpha);
        }

        public void OnBadgeCreated(BadgeData badgeData)
        {

            /*foreach (var item in _droppable)
            {
                int amountToSpawn = item.GetAmountToSpawn(badgeData);
                for (int i = 0; i < amountToSpawn; i++)
                {
                    int chanceToSpawn = UnityEngine.Random.Range(0, 100);

                    if (item.ChanceToSpawn >= chanceToSpawn)
                    {
                        //GameObject itemToDrop =  //TODO: разобраться с этой фигнёй
                        //_badgePresentation.Drop(itemToDrop);
                    }
                }
            }*/
        }

        public void SpawnBoss()
        {
            _badgePresentation.ShowNewBadge(_badgeSpawner.Spawn(_bossBadges));
        }

        public void SpawnBadge()
        {
            _badgePresentation.ShowNewBadge(_badgeSpawner.Spawn(_badges));
        }

        public void PlayerClicked(Vector2 clickPosition)
        {
            _clickEffect.SpawnEffect(clickPosition);
        }
    }

    public class UsualBadgeSpawner : IBadgeSpawner
    {
        public Sprite Spawn(Sprite[] sprites)
        {
            return sprites[UnityEngine.Random.Range(0, sprites.Length - 1)];
        }
    }

    public interface IBadgeSpawner
    {
        Sprite Spawn(Sprite[] sprites);
    }
}