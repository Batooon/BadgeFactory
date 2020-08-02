using TMPro;
using UnityEngine;

namespace DroppableItems
{
    public class BoxPromtPanelPresentation : MonoBehaviour
    {
        [SerializeField] private string _template;
        [SerializeField] private TextMeshProUGUI _promtText;
        private PlayerData _playerData;
        private BadgeData _badgeData;

        public void Init(long goldAmount, PlayerData playerData, BadgeData badgeData)
        {
            _playerData = playerData;
            _badgeData = badgeData;
            _promtText.text = string.Format(_template, goldAmount);
        }

        public void AdWatched()
        {
            _playerData.Gold += _badgeData.CoinsReward * 20;
            gameObject.SetActive(false);
        }
    }
}
