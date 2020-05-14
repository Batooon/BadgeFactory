using UnityEngine;

namespace Badge.BusinessRules
{
    public class BadgeBusinessRules : IBadgeBusinessInput
    {
        private IPlayerDataProvider _playerDataProvider;
        private IBadgeDatabase _badgeDatabase;
        private IBadgeBusinessOutput _badgeOutput;

        public BadgeBusinessRules(IPlayerDataProvider playerDataProvider,
                                  IBadgeDatabase badgeDatabase,
                                  IBadgeBusinessOutput badgeOutput)
        {
            _playerDataProvider = playerDataProvider;
            _badgeDatabase = badgeDatabase;
            _badgeOutput = badgeOutput;
        }

        public void TakeProgress()
        {
            Data playerData = _playerDataProvider.GetPlayerData();
            BadgeData badgeData = _badgeDatabase.GetBadgeData();

            badgeData.CurrentHp += (int)(playerData.AutomationsPower * Time.deltaTime);

            if (badgeData.CurrentHp >= badgeData.MaxHp)
                _badgeOutput.OnBadgeCreated(badgeData);
            else
                _badgeOutput.BadgeGotProgressCallback(badgeData);

            _badgeDatabase.SaveBadgeData();
        }
    }
}