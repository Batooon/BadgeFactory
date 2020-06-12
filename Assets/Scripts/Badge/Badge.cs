using Badge.BusinessRules;
using DroppableItems;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.U2D;
using UnityEngine.Events;
using Automation;

namespace Badge
{
    [System.Serializable]
    public class PlayerClicked : UnityEvent<Vector2> { }

    [RequireComponent(typeof(BadgePresentation))] 
    public class Badge : MonoBehaviour
    {
        [SerializeField]
        private SpriteAtlas _badges;
        [SerializeField]
        private SpriteAtlas _bossBadges;
        [SerializeField]
        private List<DroppingMothership> _droppingMotherships;
        [SerializeField]
        private BossCountdown _bossCountdown;

        private Sprite[] badges;
        private Sprite[] bosses;

        private IBadgeBusinessInput _badgeBusinessInput;
        private IBadgeBusinessOutput _badgeOutput;

        [SerializeField]
        public PlayerClicked Clicked;

        private void Awake()
        {
            badges = new Sprite[_badges.spriteCount];
            bosses = new Sprite[_bossBadges.spriteCount];

            _badges.GetSprites(badges);
            _bossBadges.GetSprites(bosses);

            BadgePresentation badgePresentation = GetComponent<BadgePresentation>();

            _badgeOutput = new BadgePresentator(badgePresentation, _droppingMotherships, badges, bosses);
            _badgeBusinessInput = new BadgeBusinessRules(PlayerDataAccess.GetPlayerDatabase(),
                                                         BadgeDatabaseAccess.GetBadgeDatabase(),
                                                         AutomationDatabse.GetAutomationDatabase(),
                                                         _badgeOutput,
                                                         _bossCountdown);
        }

        private void Start()
        {
            _badgeBusinessInput.CreateNewBadge();
        }

        private void Update()
        {
            //Спавнитться сразу же очень мног монеток так как хп у значка 0,
            //поэтому пока что закомментировал
            _badgeBusinessInput.TakeProgress();
            HandlePlayerInput();
        }

        private void OnEnable()
        {
            _bossCountdown.BossNotDefeated+=OnBossNotDefeated;
        }

        private void OnDisable()
        {
            _bossCountdown.BossNotDefeated -= OnBossNotDefeated;
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

                        Vector2 screenPosition = Camera.main.ScreenToWorldPoint(touches[i].position);
                        _badgeBusinessInput.ClickProgress();
                        Clicked?.Invoke(screenPosition);
                    }
                }
            }
        }

        private void OnApplicationQuit()
        {
            BadgeDatabaseAccess.GetBadgeDatabase().Serialize();
            PlayerDataAccess.GetPlayerDatabase().SerializePlayerData();
        }

        private void OnApplicationPause(bool pause)
        {
            if (pause)
            {
                BadgeDatabaseAccess.GetBadgeDatabase().Serialize();
                PlayerDataAccess.GetPlayerDatabase().SerializePlayerData();
            }
        }
    }
}