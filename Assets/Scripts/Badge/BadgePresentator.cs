using System;
using UnityEngine;
using System.Collections.Generic;
using DroppableItems;

namespace Badge
{
    public class BadgePresentator : IBadgeBusinessOutput
    {
        public event Action<IBadgeDatabase> BadgeCreated;

        private BadgePresentation _badgePresentation;
        private List<DroppableObject> _droppableObjects;

        private int _coinsToSpawn;
        private int _oneCoinCost;

        public BadgePresentator(BadgePresentation badgePresentation, List<DroppableObject> droppableObjects)
        {
            _badgePresentation = badgePresentation;
            _droppableObjects = droppableObjects;
        }

        public void BadgeGotProgressCallback(BadgeData badgeData)
        {
            float alpha = Mathf.Clamp01(Mathf.InverseLerp(0, badgeData.MaxHp, badgeData.CurrentHp));

            _badgePresentation.UpdateBadgeProgress(alpha);
        }

        public void OnBadgeCreated(BadgeData badgeData)
        {
            foreach (var item in _droppableObjects)
            {
                int amountToSpawn=item.GetAmountToSpawn(badgeData);
                for (int i = 0; i < amountToSpawn; i++)
                {
                    int chanceToSpawn = UnityEngine.Random.Range(0, 100);

                    if (item.ChanceToSpawn >= chanceToSpawn)
                    {
                        GameObject itemToDrop = item.gameObject;
                        _badgePresentation.Drop(itemToDrop);
                    }
                }
            }
        }
    }
}