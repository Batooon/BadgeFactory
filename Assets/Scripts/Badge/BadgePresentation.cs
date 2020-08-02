using BadgeImplementation.BusinessRules;
using UnityEngine;
using UnityEngine.Events;

namespace BadgeImplementation
{
    public class BadgePresentation : MonoBehaviour
    {
        [SerializeField] private SpriteRenderer _badgeSprite;
        [SerializeField] private UnityEvent _badgeCreated;

        private BadgeData _badgeData;
        private BadgeBusinessRules _badgeBusinessRules;

        public void Init(BadgeData badgeData,BadgeBusinessRules businessRules)
        {
            _badgeData = badgeData;
            _badgeBusinessRules = businessRules;
            _badgeData.HpChanged += UpdateBadgeProgress;
            _badgeBusinessRules.BadgeCreated += OnBadgeCreated;
        }

        private void OnEnable()
        {
            if (_badgeData == null)
                return;

            _badgeData.HpChanged += UpdateBadgeProgress;
        }

        private void OnDisable()
        {
            _badgeData.HpChanged -= UpdateBadgeProgress;
        }

        private void UpdateBadgeProgress(long newHp)
        {
            float alpha = Mathf.Clamp01(Mathf.InverseLerp(0, _badgeData.MaxHp, _badgeData.CurrentHp));

            Color tempColor = _badgeSprite.color;
            tempColor.a = alpha;

            _badgeSprite.color = tempColor;
        }

        public void ShowNewBadge(Sprite sprite)
        {
            _badgeSprite.sprite = sprite;

            Color tempColor = _badgeSprite.color;
            tempColor.a = 0f;

            _badgeSprite.color = tempColor;
        }

        public void OnBadgeCreated()
        {
            _badgeCreated?.Invoke();
        }
    }

    public interface IClickEffect
    {
        void SpawnEffect(Vector2 position);
    }
}