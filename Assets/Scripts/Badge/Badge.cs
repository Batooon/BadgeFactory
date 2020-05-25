using Badge.BusinessRules;
using DroppableItems;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.U2D;
using OdinSerializer;

namespace Badge
{
    [RequireComponent(typeof(BadgePresentation))] 
    public class Badge : MonoBehaviour
    {
        [SerializeField]
        private SpriteAtlas _badges;
        [SerializeField]
        private SpriteAtlas _bossBadges;
        [SerializeField]
        private List<DroppableObject> _droppableItems;
        [SerializeField]
        private BossCountdown _bossCountdown;
        [SerializeField]
        private ClickPresentation _clickEffect;

        private Sprite[] badges;
        private Sprite[] bosses;

        private IBadgeBusinessInput _badgeBusinessInput;
        private IBadgeBusinessOutput _badgeOutput;

        private void Awake()
        {
            badges = new Sprite[_badges.spriteCount];
            bosses = new Sprite[_bossBadges.spriteCount];

            _badges.GetSprites(badges);
            _bossBadges.GetSprites(bosses);

            BadgePresentation badgePresentation = GetComponent<BadgePresentation>();
            List<DroppableObject> droppableObjects = new List<DroppableObject>();

            for (int i = 0; i < _droppableItems.Count; i++)
            {
                DroppableObject droppableObject;
                _droppableItems[i].TryGetComponent(out droppableObject);

                if(droppableObject==null)
                    Debug.LogError("Droppable items error! in Badge.cs list of droppable items must contain DroppableObject.cs component!!");
                droppableObjects.Add(droppableObject);
            }

            _badgeOutput = new BadgePresentator(badgePresentation, droppableObjects, badges, bosses, _clickEffect);
            _badgeBusinessInput = new BadgeBusinessRules(PlayerDataAccess.GetPlayerDatabase(),
                                                         BadgeDatabaseAccess.GetBadgeDatabase(),
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
            //_badgeBusinessInput.TakeProgress();
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
                        _badgeBusinessInput.ClickProgress(screenPosition);
                    }
                }
            }
        }
    }

    /*public class DroppableItems : MonoBehaviour
    {
        [SerializeField]
        private List<GameObject> _droppableItems;
        [OdinSerialize]
        private List<IDroppable> _droppableImplementations;

        public void DropItems()
        {

        }
    }*/
}