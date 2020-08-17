using BadgeImplementation.BusinessRules;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace BadgeImplementation
{
    public class BadgePresentation : MonoBehaviour
    {
        [SerializeField] private SpriteRenderer _badgeSprite;
        [SerializeField] private SpriteRenderer _badgeStand;
        [SerializeField] private UnityEvent _badgeCreated;
        [SerializeField] private List<SpriteRenderer> _badgeSpriteComponents;

        private BadgeData _badgeData;
        private BadgeBusinessRules _badgeBusinessRules;

        public void Init(BadgeData badgeData, BadgeBusinessRules businessRules)
        {
            if (_badgeSpriteComponents.Contains(_badgeSprite) == false)
                Debug.LogError("BadgeSticker is not assigned!");

            _badgeData = badgeData;
            _badgeBusinessRules = businessRules;
            _badgeBusinessRules.BadgeCreated += OnBadgeCreated;
            _badgeData.HpChanged += UpdateBadgeProgress;
            _badgeSpriteComponents[_badgeSpriteComponents.IndexOf(_badgeSprite)] = _badgeSprite;
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

        private void UpdateBadgeProgress(float newHp)
        {
            int i = 0;
            float currentComponentProgress = _badgeData.MaxHp / _badgeSpriteComponents.Count;
            if (_badgeData.CurrentHp > currentComponentProgress * (i + 1)) 
                i += 1;

            float alpha = Mathf.Clamp01(Mathf.InverseLerp(0, currentComponentProgress * (i + 1), _badgeData.CurrentHp));

            Color tempColor = _badgeSpriteComponents[i].color;
            tempColor.a = alpha;

            _badgeSpriteComponents[i].color = tempColor;
        }

        public void ShowNewBadge(Sprite sprite, Sprite badgeStand)
        {
            int badgeSrickerIndex = _badgeSpriteComponents.IndexOf(_badgeSprite);
            _badgeSprite.sprite = sprite;
            _badgeSpriteComponents[badgeSrickerIndex] = _badgeSprite;

            int badgeStandIndex = _badgeSpriteComponents.IndexOf(_badgeStand);
            _badgeStand.sprite = badgeStand;
            _badgeSpriteComponents[badgeStandIndex] = _badgeStand;

            for (int i = 0; i < _badgeSpriteComponents.Count; i++)
            {
                Color tempColor = _badgeSpriteComponents[i].color;
                tempColor.a = 0f;

                _badgeSpriteComponents[i].color = tempColor;
            }
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