using BadgeImplementation.BusinessRules;
using DroppableItems;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.U2D;

namespace BadgeImplementation
{
    [RequireComponent(typeof(BadgePresentation))]
    public class Badge : MonoBehaviour
    {
        [SerializeField] private BadgeProgressPresentation _badgeProgressPresentation;
        [SerializeField] private SpriteAtlas _badges;
        [SerializeField] private SpriteAtlas _bossBadges;
        [SerializeField] private SpriteAtlas _badgeStands;
        [SerializeField] private SpriteAtlas _bossStands;
        [SerializeField] private List<DroppingMothership> _droppingMotherships;
        [SerializeField] private BossCountdown _bossCountdown;
        [SerializeField] private HitDamageSpawner _hitDamageSpawner;
        [SerializeField] private UnityEvent _clicked;

        private Sprite[] _badgeSprites;
        private Sprite[] _bossSprites;
        private Sprite[] _bossStandSprites;
        private Sprite[] _badgeStandSprites;

        private BadgeBusinessRules _badgeBusinessInput;
        private IBadgeBusinessOutput _badgeOutput;
        private BadgePresentation _badgePresentation;

        public void Init(PlayerData playerData, AutomationsData automationsData, BadgeData badgeData)
        {
            _badgeSprites = new Sprite[_badges.spriteCount];
            _bossSprites = new Sprite[_bossBadges.spriteCount];
            _bossStandSprites = new Sprite[_bossStands.spriteCount];
            _badgeStandSprites = new Sprite[_badgeStands.spriteCount];

            _badges.GetSprites(_badgeSprites);
            _bossBadges.GetSprites(_bossSprites);
            _badgeStands.GetSprites(_badgeStandSprites);
            _bossStands.GetSprites(_bossStandSprites);

            _bossCountdown.Init();

            _badgeBusinessInput = new BadgeBusinessRules(playerData,
                                                         badgeData,
                                                         automationsData,
                                                         _bossCountdown);

            _badgePresentation = GetComponent<BadgePresentation>();
            _badgePresentation.Init(badgeData, _badgeBusinessInput);

            foreach (var mothership in _droppingMotherships)
            {
                mothership.Init(badgeData, playerData);
            }


            _badgeBusinessInput.CreateBadgeEvent += CreateBadge;
            _badgeBusinessInput.CreateBossEvent += CreateBoss;
            _badgeBusinessInput.BadgeCreated += OnBadgeCreated;
            _bossCountdown.BossNotDefeated += OnBossNotDefeated;

            _badgeProgressPresentation.Init(badgeData);
            _hitDamageSpawner.Init(automationsData);
            _badgeBusinessInput.CreateNewBadge();
        }

        private void TakeProgress()
        {
            _badgeBusinessInput.TakeProgress();
        }

        private void Update()
        {
            HandlePlayerInput();
            TakeProgress();
        }

        private void OnDisable()
        {
            _bossCountdown.BossNotDefeated -= OnBossNotDefeated;
            _badgeBusinessInput.BadgeCreated -= OnBadgeCreated;
            _badgeBusinessInput.CreateBadgeEvent -= CreateBadge;
            _badgeBusinessInput.CreateBossEvent -= CreateBoss;
        }

        private void OnBossNotDefeated()
        {
            _badgeBusinessInput.OnBossNotDefeated();
        }

        private void HandlePlayerInput()
        {
            Touch[] touches = Input.touches;

            if (touches.Length > 0)
            {
                for (int i = 0; i < touches.Length; i++)
                {
                    if (touches[i].phase == TouchPhase.Began)
                    {
                        if (EventSystem.current.IsPointerOverGameObject(touches[i].fingerId))
                            return;

                        _badgeBusinessInput.ClickProgress();
                        _clicked?.Invoke();
                        _hitDamageSpawner.SpawnText(_badgeBusinessInput.CurrentClickHitValue);
                    }
                }
            }
        }

        private void OnBadgeCreated()
        {
            foreach (var mothership in _droppingMotherships)
            {
                mothership.Spawn(mothership.transform.position);
            }
        }

        private void CreateBoss()
        {
            _badgePresentation.ShowNewBadge(_bossSprites[Random.Range(0, _bossSprites.Length)], _bossStandSprites[Random.Range(0, _bossStandSprites.Length)]);
        }

        private void CreateBadge()
        {
            _badgePresentation.ShowNewBadge(_badgeSprites[Random.Range(0, _badgeSprites.Length)], _badgeStandSprites[Random.Range(0, _badgeStandSprites.Length)]);
        }
    }
}