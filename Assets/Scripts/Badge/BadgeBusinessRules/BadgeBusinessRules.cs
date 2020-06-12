using Automation;
using UnityEngine;

namespace Badge.BusinessRules
{
    public class BadgeBusinessRules : IBadgeBusinessInput
    {
        private IPlayerDataProvider _playerDataProvider;
        private IBadgeDatabase _badgeDatabase;
        private IBadgeBusinessOutput _badgeOutput;
        private IBossCountdown _bossCountdown;
        private IAutomationDatabase _automationDatabase;

        //TODO: Fluent builder?
        public BadgeBusinessRules(IPlayerDataProvider playerDataProvider,
                                  IBadgeDatabase badgeDatabase,
                                  IAutomationDatabase automationDatabase,
                                  IBadgeBusinessOutput badgeOutput,
                                  IBossCountdown bossCountdown)
        {
            _playerDataProvider = playerDataProvider;
            _badgeDatabase = badgeDatabase;
            _badgeOutput = badgeOutput;
            _bossCountdown = bossCountdown;
            _automationDatabase = automationDatabase;
        }

        public void CreateNewBadge()
        {
            Data playerData = _playerDataProvider.GetPlayerData();
            ResetBadgeHp();
            if (playerData.Level != 0 && playerData.Level % 5 == 0)
            {
                InitBoss();
            }
            else
            {
                InitBadge();
            }
        }

        public void InitBoss()
        {
            _badgeOutput.SpawnBoss();
            _bossCountdown.StartCountdown(_playerDataProvider.GetPlayerData().BossCountdownTime);
        }

        public void InitBadge()
        {
            _badgeOutput.SpawnBadge();
        }

        public void OnBossNotDefeated()
        {
            Data playerData = _playerDataProvider.GetPlayerData();
            playerData.Level -= 1;
            playerData.MaxLevelProgress = 10;
            playerData.LevelProgress = 0;
            CreateNewBadge();
        }

        public void ClickProgress()
        {
            OverallAutomationsData automationsData = _automationDatabase.GetOverallAutomationsData();
            BadgeData badgeData = _badgeDatabase.GetBadgeData();

            badgeData.CurrentHp += automationsData.ClickPower;

            AddBadgeProgress();
        }

        public void TakeProgress()
        {
            OverallAutomationsData automationsData = _automationDatabase.GetOverallAutomationsData();
            BadgeData badgeData = _badgeDatabase.GetBadgeData();

            badgeData.CurrentHp += (int)(automationsData.AutomationsPower * Time.deltaTime);

            AddBadgeProgress();
        }

        private void ResetBadgeHp()
        {
            BadgeData badgeData = _badgeDatabase.GetBadgeData();
            Data playerData = _playerDataProvider.GetPlayerData();
            float exponent = 1f;
            for (int i = 0; i < playerData.Level - 1; i++)
            {
                exponent *= 1.55f;
            }
            int maxBadgeHp = (int)(10 * ((playerData.Level - 1) + exponent));
            if (playerData.Level % 5 == 0)
                maxBadgeHp *= 10;
            badgeData.MaxHp = maxBadgeHp;
            badgeData.CurrentHp = 0;
        }

        private void AddBadgeProgress()
        {
            Data playerData = _playerDataProvider.GetPlayerData();
            BadgeData badgeData = _badgeDatabase.GetBadgeData();

            if (badgeData.CurrentHp >= badgeData.MaxHp)
            {
                if (playerData.Level % 5 == 0)
                    _bossCountdown.StopCountdown();

                IncreaseLevel(playerData);
                
                CreateNewBadge(); 
                _badgeOutput.OnBadgeCreated(badgeData);
            }
            else
                _badgeOutput.BadgeGotProgressCallback(badgeData);
        }

        private void IncreaseLevel(Data playerData)
        {
            if (playerData.NeedToIncreaseLevel)
            {
                if (playerData.LevelProgress == playerData.MaxLevelProgress)
                {
                    playerData.Level += 1;
                    playerData.LevelProgress = 1;
                    if (playerData.Level % 5 == 0)
                        playerData.MaxLevelProgress = 1;//TODO: сделать редактор, чтобы можно было настраивать максимальный прогресс
                    else
                        playerData.MaxLevelProgress = 10;
                }
                else
                {
                    playerData.LevelProgress += 1;
                }
            }
        }
    }
}
