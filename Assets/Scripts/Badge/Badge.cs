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
        [SerializeField] private List<DroppingMothership> _droppingMotherships;
        [SerializeField] private BossCountdown _bossCountdown;
        [SerializeField] private ObjectPooler _coinPooler;
        [SerializeField] private UnityEvent _clicked;

        private Sprite[] badges;
        private Sprite[] bosses;
        private BadgeBusinessRules _badgeBusinessInput;
        private IBadgeBusinessOutput _badgeOutput;
        private BadgePresentation _badgePresentation;

        public void Init(PlayerData playerData, AutomationsData automationsData, BadgeData badgeData)
        {
            badges = new Sprite[_badges.spriteCount];
            bosses = new Sprite[_bossBadges.spriteCount];

            _badges.GetSprites(badges);
            _bossBadges.GetSprites(bosses);

            _bossCountdown.Init();

            _badgeBusinessInput = new BadgeBusinessRules(playerData,
                                                         badgeData,
                                                         automationsData,
                                                         _bossCountdown);

            _badgePresentation = GetComponent<BadgePresentation>();
            _badgePresentation.Init(badgeData, _badgeBusinessInput);

            _badgeBusinessInput.BadgeCreated += OnBadgeCreated;
            _badgeBusinessInput.CreateBadgeEvent += CreateBadge;
            _badgeBusinessInput.CreateBossEvent += CreateBoss;

            foreach (var mothership in _droppingMotherships)
            {
                mothership.Init(badgeData, playerData, _coinPooler);
            }

            _badgeBusinessInput.CreateNewBadge();
            _badgeProgressPresentation.Init(badgeData);
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

        private void OnEnable()
        {
            _bossCountdown.BossNotDefeated += OnBossNotDefeated;
            if (_badgeBusinessInput == null)
                return;

            _badgeBusinessInput.BadgeCreated += OnBadgeCreated;
            _badgeBusinessInput.CreateBadgeEvent += CreateBadge;
            _badgeBusinessInput.CreateBossEvent += CreateBoss;
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
            _badgePresentation.ShowNewBadge(bosses[Random.Range(0, bosses.Length)]);
        }

        private void CreateBadge()
        {
            _badgePresentation.ShowNewBadge(badges[Random.Range(0, badges.Length)]);
        }
    }
}