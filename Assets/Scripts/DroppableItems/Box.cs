﻿using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

namespace DroppableItems
{
    public class Box : MonoBehaviour, ICollectable
    {
        [SerializeField] private float _lifetime;

        private IItemTweener _itemTweener;
        private BadgeData _badgeData;
        private UnityEvent _boxOpenEvent;
        private int _boxReward;

        public int BoxReward => _boxReward;

        public void Init(UnityEvent boxOpenEvent)
        {
            _boxOpenEvent = boxOpenEvent;
            _itemTweener = GetComponent<IItemTweener>();
            _itemTweener.StartMotion();
            StartCoroutine(HideAfterLifetime());
        }

        private void OnMouseDown()
        {
            if (EventSystem.current.IsPointerOverGameObject())
                return;

            _boxOpenEvent?.Invoke();
            gameObject.SetActive(false);
        }

        private IEnumerator HideAfterLifetime()
        {
            float lifeTime = _lifetime;
            while (lifeTime > 0)
            {
                lifeTime -= 1f;
                yield return new WaitForSeconds(1f);
            }
            gameObject.SetActive(false);
        }

        public void Init(in long reward, PlayerData playerData, AudioService audioService)
        {
        }

        public void OnMovingEnded()
        {
        }
    }
}
