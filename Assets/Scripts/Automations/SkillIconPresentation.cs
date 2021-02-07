using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Automations
{
    public class SkillIconPresentation : MonoBehaviour,IObserver
    {
        private Image _componentImage;
        private UpgradeComponentData _upgradeComponentData;

        public void Init(UpgradeComponentData upgradeComponentData)
        {
            _upgradeComponentData = upgradeComponentData;
        }

        private void OnEnable()
        {
            _upgradeComponentData.Attach(this);
            Fetch(_upgradeComponentData);
        }

        private void OnDisable()
        {
            _upgradeComponentData.Detach(this);
        }

        public void Fetch(ISubject subject)
        {
            if (subject is UpgradeComponentData)
            {
                OnComponentUnlocked(_upgradeComponentData.IsComponentUnlocked);
            }
        }

        private void OnComponentUnlocked(bool isUnlocked)
        {
            /*_componentImage.sprite=isUnlocked?открытая иконка:замочек;*/
        }

        private void OnComponentPurchased(bool isPurchased)
        {
            if (_upgradeComponentData.IsComponentUnlocked)
                return;
            /*_componentImage.sprite=isPurchased?открытая иконка:замочек;*/
        }
        
        /*
        [SerializeField] private Toggle _buyUpgradeToggle;
        [SerializeField] private TextMeshProUGUI _costText;
        [SerializeField] private TextMeshProUGUI _percentageText;
        [SerializeField] private UnityEvent _componentBecomesVisible;
        [SerializeField] private UnityEvent _componentBecomesInvisible;
        [SerializeField] private string _costTemplate;
        [SerializeField] private string _percentageTemplate;

        private PlayerData _playerData;
        private Automation _automationData;
        private UpgradeComponentData _upgradeComponentData;

        public void Init(PlayerData playerData, UpgradeComponentData upgradeComponentData, Automation automation)
        {
            _playerData = playerData;
            _upgradeComponentData = upgradeComponentData;
            _automationData = automation;

            _costText.text = string.Format(_costTemplate, _upgradeComponentData.UpgradeCost);
            _percentageText.text = string.Format(_percentageTemplate, _upgradeComponentData.Percentage);
        }
        
        public void Fetch(ISubject subject)
        {
            PlayerData playerData = subject as PlayerData;
            OnGoldAmountChanged(playerData.Gold);
        }

        private void OnEnable()
        {
            _playerData.Attach(this);
            _upgradeComponentData.ComponentVisibilityChanged += OnComponentVisibilityChanged;
            _upgradeComponentData.UpgradeComponentStatechanged += OnComponentPurchasedOrNot;
            OnComponentVisibilityChanged(_upgradeComponentData.IsComponentUnlocked);
            OnComponentPurchasedOrNot(_upgradeComponentData.IsUpgradeComponentPurchased);

            OnGoldAmountChanged(_playerData.Gold);
        }

        private void OnDisable()
        {
            _playerData.Detach(this);
            _upgradeComponentData.ComponentVisibilityChanged -= OnComponentVisibilityChanged;
        }

        private void OnComponentPurchasedOrNot(bool isPurchased)
        {
            if (isPurchased)
                _buyUpgradeToggle.interactable = false;
        }

        private void OnGoldAmountChanged(BigInteger newValue)
        {
            _buyUpgradeToggle.interactable = _upgradeComponentData.UpgradeCost <= newValue && _upgradeComponentData.IsComponentUnlocked;
        }

        private void OnComponentVisibilityChanged(bool isVisible)
        {
            if (isVisible)
                _componentBecomesVisible?.Invoke();
            else
                _componentBecomesInvisible?.Invoke();
            OnGoldAmountChanged(_playerData.Gold);
        }*/
    }

    public class AutomationUpgradeComponentPresenter : MonoBehaviour
    {
        //private IAutomationCommand _upgradeCommand;
        public event Action UpgradeButtonPressed;
        private UpgradeComponentData _upgradeComponentData;

        public void PressUpgradeButton()
        {
            UpgradeButtonPressed?.Invoke();
            //_upgradeCommand.Execute();
        }

        /*public void SetAutomationUpgradeCommand(IAutomationCommand command)
        {
            _upgradeCommand = command;
        }*/

        public void SetUpgradeComponentData(UpgradeComponentData skillData)
        {
            _upgradeComponentData = skillData;
        }
    }

    public interface IAutomationBuilder
    {
        void InstantiateAutomation(Transform parent, Automation automationData);
        void AddSkill(IAutomationCommand command, UpgradeComponentData skillData);
        void SetAutomationType(IAutomation automation);
    }
}