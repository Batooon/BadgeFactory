using System;
using System.Numerics;

namespace Automations
{
    public class AutomationLogic : IObserver, IDisposable
    {
        private const int LevelToGetPoint = 2000;
        private const int PointsIncrease = 1;
        private readonly AutomationsData _automationsData;
        private readonly PlayerData _playerData;
        private Automation _automationData;
        private readonly AutomationPresentation _automationPresentation;
        private IAutomation _automation;

        public AutomationLogic(PlayerData playerData, AutomationsData automationsData,
            AutomationPresentation automationPresentation, Automation automationData)
        {
            _playerData = playerData;
            _automationsData = automationsData;
            _automationPresentation = automationPresentation;
            _automationData = automationData;
            
            _automationPresentation.UpgradeButtonPressed += TryUpgradeAutomation;
            _playerData.Attach(this);
            _automationData.Attach(this);
            OnGoldAmountUpdated(_playerData.Gold);
            _automationPresentation.gameObject.SetActive(_automationData.IsUnlocked);
        }
        
        public void Dispose()
        {
            _automationPresentation.UpgradeButtonPressed -= TryUpgradeAutomation;
            _playerData.Detach(this);
            _automationData.Detach(this);
        }

        public void SetAutomation(IAutomation automation)
        {
            _automation = automation;
            RecalculateCost(_automationsData.LevelsToUpgrade);
        }

        public void Fetch(ISubject subject)
        {
            switch (subject)
            {
                case PlayerData _:
                    OnGoldAmountUpdated(_playerData.Gold);
                    break;
                case AutomationsData _:
                    RecalculateCost(_automationsData.LevelsToUpgrade);
                    break;
            }
        }

        private void TryUpgradeAutomation(Action<bool> upgradeResult)
        {
            var result = IsEnoughMoneyToUpgrade();
            upgradeResult?.Invoke(result);
            if (result == false)
                return;

            _playerData.Gold -= _automationData.CurrentCost;
            _automation.Upgrade(_automationData, _automationsData);
            if (_automationData.Level % LevelToGetPoint == 0)
                _playerData.BadgePoints += PointsIncrease;
        }

        private void OnGoldAmountUpdated(BigInteger goldAmount)
        {
            _automationData.CanUpgrade = goldAmount >= _automationData.CurrentCost;
        }

        private void RecalculateCost(int levelsToUpgrade)
        {
            _automation.RecalculateCost(levelsToUpgrade, _automationData);
        }

        private bool IsEnoughMoneyToUpgrade()
        {
            return _playerData.Gold >= _automationData.CurrentCost;
        }
    }
}