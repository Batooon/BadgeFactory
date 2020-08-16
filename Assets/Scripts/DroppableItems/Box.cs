using UnityEngine;
using UnityEngine.Events;

namespace DroppableItems
{
    public class Box : MonoBehaviour
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
            Destroy(gameObject, _lifetime);
        }

        private void OnMouseEnter()
        {
            _boxOpenEvent?.Invoke();
            Destroy(gameObject);
        }
    }
}
