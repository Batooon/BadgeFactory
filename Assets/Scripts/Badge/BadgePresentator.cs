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
        private List<DroppingMothership> _droppable;
        private List<GameObject> _itemsToDrop;
        private Sprite[] _badges;
        private Sprite[] _bossBadges;
        private Sprite[] _badgeStands;
        private Sprite[] _bossStands;

        private IBadgeSpawner _badgeSpawner;

        private int _coinsToSpawn;
        private int _oneCoinCost;

        public BadgePresentator(BadgePresentation badgePresentation,
                                List<DroppingMothership> droppable,
                                Sprite[] badges,
                                Sprite[] bosses,
                                Sprite[] badgeStands,
                                Sprite[] bossStands)
        {
            _badgePresentation = badgePresentation;
            _droppable = droppable;
            _badges = badges;
            _bossBadges = bosses;
            _badgeStands = badgeStands;
            _bossStands = bossStands;
            _badgeSpawner = new UsualBadgeSpawner();
        }

        public void OnBadgeCreated(BadgeData badgeData)
        {
            Debug.Log("Badge Created?????");
            _badgePresentation.OnBadgeCreated();
            foreach (var mothership in _droppable)
            {
                mothership.Spawn(mothership.transform.position);
            }
        }

        public void SpawnBoss()
        {
            _badgePresentation.ShowNewBadge(_badgeSpawner.Spawn(_bossBadges), _badgeSpawner.Spawn(_bossStands));
        }

        public void SpawnBadge()
        {
            _badgePresentation.ShowNewBadge(_badgeSpawner.Spawn(_badges), _badgeSpawner.Spawn(_badgeStands));
        }

        public void BadgeGotProgressCallback(BadgeData badgeData)
        {

        }
    }

    public class UsualBadgeSpawner : IBadgeSpawner
    {
        public Sprite Spawn(Sprite[] sprites)
        {
            return sprites[UnityEngine.Random.Range(0, sprites.Length)];
        }
    }

    public interface IBadgeSpawner
    {
        Sprite Spawn(Sprite[] sprites);
    }
}