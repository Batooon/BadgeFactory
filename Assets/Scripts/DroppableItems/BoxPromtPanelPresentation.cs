using System;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

namespace DroppableItems
{
    [Serializable]
    public class LongUnityEvent : UnityEvent<long> { }

    public class BoxPromtPanelPresentation : MonoBehaviour
    {
        [SerializeField] private LongUnityEvent _rewardPlayer;
        [SerializeField] private string _template;
        [SerializeField] private TextMeshProUGUI _promtText;
        private long _goldReward;

        public void Init(long goldReward)
        {
            _goldReward = goldReward;
            _promtText.text = string.Format(_template, _goldReward);
        }

        public void AdWatched()
        {
            //_playerData.Gold += _goldReward;
            gameObject.SetActive(false);
            _rewardPlayer?.Invoke(_goldReward);
        }
    }
}
