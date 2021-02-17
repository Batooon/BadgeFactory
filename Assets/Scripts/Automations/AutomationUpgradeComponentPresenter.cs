using System;
using TMPro;
using UnityEngine;

namespace Automations
{
    public class AutomationUpgradeComponentPresenter : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _skillStats;
        public event Action UpgradeButtonPressed;
        private UpgradeComponentData _upgradeComponentData;

        public void Init(string skillStatsTemplate, object args)
        {
            _skillStats.text = string.Format(skillStatsTemplate, args);
        }
        
        public void PressUpgradeButton()
        {
            UpgradeButtonPressed?.Invoke();
        }

        public void SetUpgradeComponentData(UpgradeComponentData skillData)
        {
            _upgradeComponentData = skillData;
        }
    }
}