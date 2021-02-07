using System;

namespace Automations
{
    public class UpgradeComponent : IObserver, IDisposable
    {
        #region Params
        public AutomationsData AutomationsData { get; }
        public Automation AutomationData { get; }
        
        private readonly IAutomationCommand _skillCommand;
        private readonly AutomationUpgradeComponentPresenter _skillPresenter;
        private readonly UpgradeComponentData _upgradeComponentData;
        private readonly PlayerData _playerData;
        #endregion

        public UpgradeComponent(PlayerData playerData, AutomationsData automationsData, Automation automationData,
            UpgradeComponentData upgradeComponentData, IAutomationCommand automationCommand,
            AutomationUpgradeComponentPresenter skillPresenter)
        {
            _upgradeComponentData = upgradeComponentData;
            _playerData = playerData;
            AutomationsData = automationsData;
            AutomationData = automationData;
            _skillCommand = automationCommand;
            _skillPresenter = skillPresenter;
            
            Subscribe();
        }
        
        public void Dispose()
        {
            _skillPresenter.UpgradeButtonPressed -= OnBuyComponentButtonPressed;
            AutomationData.Detach(this);
        }
        
        public void Fetch(ISubject subject)
        {
            OnLevelChanged(AutomationData.Level);
        }

        private void Subscribe()
        {
            _skillPresenter.UpgradeButtonPressed += OnBuyComponentButtonPressed;
            AutomationData.Attach(this);

            OnLevelChanged(AutomationData.Level);
        }

        private void OnBuyComponentButtonPressed()
        {
            if (_upgradeComponentData.IsUpgradeComponentPurchased ||
                _upgradeComponentData.IsComponentUnlocked == false)
                return;

            _playerData.Gold -= _upgradeComponentData.UpgradeCost;
            BuySkill();
            _upgradeComponentData.IsUpgradeComponentPurchased = true;
        }

        private void OnLevelChanged(int newLevel)
        {
            if (_upgradeComponentData.IsUpgradeComponentPurchased)
                return;

            _upgradeComponentData.IsComponentUnlocked = newLevel >= _upgradeComponentData.LevelToUnlock;
        }

        private void BuySkill()
        {
            _skillCommand.Execute(this, _upgradeComponentData.Percentage);
        }
    }
}