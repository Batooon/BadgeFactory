using System;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace Automations
{
    public class AutomationPresentation : MonoBehaviour,IObserver
    {
        public event Action<Action<bool>> UpgradeButtonPressed;
        [SerializeField] private TextMeshProUGUI _damageText;
        [SerializeField] private string _damageTextTemplate;
        [SerializeField] private TextMeshProUGUI _upgradeCostText;
        [SerializeField] private TextMeshProUGUI _levelText;
        [SerializeField] private string _levelTextTemplate;
        [SerializeField] private Color _notEnoughMoneyColorText;
        [SerializeField] private Color _defaultMoneyColorText;
        [SerializeField] private Button _upgradeButton;
        [SerializeField] private List<int> _starsLevels;
        [SerializeField] private List<Image> _starPlaceholders;
        [SerializeField] private UnityEvent _automationUnlocked;
        [SerializeField] private Transform _skillsParent;
        [SerializeField] private UnityEvent _automationUpgraded;
        [SerializeField] private UnityEvent _automationNotUpgraded;
        private List<AutomationUpgradeComponentPresenter> _skillsPrefabs;
        
        private Automation _automation;

        public Transform SkillIconsParent => _skillsParent;

        public void Init(Automation automation)
        {
            _automation = automation;
            /*_automation.Attach(this);
            TickEvents();*/
        }

        public void Fetch(ISubject subject)
        {
            if (subject is Automation == false)
                return;
            
            TickEvents();
        }

        public void AddSkillPrefab(AutomationUpgradeComponentPresenter skillPrefab)
        {
            if (_skillsPrefabs == null)
            {
                SetSkillPrefabs(new[] {skillPrefab});
                return;
            }
            _skillsPrefabs.Add(skillPrefab);
        }

        private void OnEnable()
        {
            _automation?.Attach(this);

            TickEvents();
        }

        private void OnDisable()
        {
            _automation.Detach(this);
        }

        public void PressUpgradeButton()
        {
            UpgradeButtonPressed?.Invoke(UpgradeResult);
        }

        private void TickEvents()
        {
            if (_automation == null)
                return;
            OnCostChanged(_automation.CurrentCost);
            OnDamageChanged(_automation.CurrentDamage);
            OnLevelChanged(_automation.Level);
            OnUpgradeAvailabilityChanged(_automation.CanUpgrade);
            OnUnlockedChanged(_automation.IsUnlocked);
        }
        
        private void SetSkillPrefabs(IEnumerable<AutomationUpgradeComponentPresenter> skillPrefabs)
        {
            _skillsPrefabs = new List<AutomationUpgradeComponentPresenter>(skillPrefabs.ToArray());
        }

        private void OnCostChanged(long newCost)
        {
            _upgradeCostText.text = newCost.ConvertValue();
        }

        private void OnLevelChanged(int newLevel)
        {
            if (newLevel >= 1)
                _automationUnlocked?.Invoke();

            _levelText.text = string.Format(_levelTextTemplate, newLevel);
            for (int i = _starsLevels.Count - 1; i >= 0; i--)
            {
                if (i >= _starPlaceholders.Count)
                    return;

                Color tempColor = _starPlaceholders[i].color;
                tempColor.a = newLevel >= _starsLevels[i] ? 255 : 0;
                _starPlaceholders[i].color = tempColor;
            }
        }

        private void OnDamageChanged(long newDamage)
        {
            _damageText.text = string.Format(_damageTextTemplate, newDamage.ConvertValue());
        }

        private void OnUpgradeAvailabilityChanged(bool canUpgrade)
        {
            _upgradeButton.interactable = canUpgrade;
            _upgradeCostText.color = canUpgrade ? _defaultMoneyColorText : _notEnoughMoneyColorText;
        }

        private void OnUnlockedChanged(bool unlocked)
        {
            gameObject.SetActive(unlocked);
        }

        private void UpgradeResult(bool result)
        {
            if (result == false)
            {
                _automationNotUpgraded?.Invoke();
                return;
            }

            _automationUpgraded?.Invoke();
        }
    }
}
