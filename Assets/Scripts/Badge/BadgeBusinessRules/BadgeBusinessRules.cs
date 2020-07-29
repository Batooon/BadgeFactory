using AutomationImplementation;
using System;
using UnityEngine;

namespace BadgeImplementation.BusinessRules
{
    public class BadgeBusinessRules : IBadgeBusinessInput
    {
        public event Action BadgeCreated;
        public event Action CreateBadgeEvent;
        public event Action CreateBossEvent;

        private PlayerData _playerData;
        private BadgeData _badgeData;
        private AutomationsData _automationsData;
        private IBadgeBusinessOutput _badgeOutput;
        private IBossCountdown _bossCountdown;

        //TODO: Fluent builder?
        public BadgeBusinessRules(PlayerData playerData,
                                  BadgeData badgeData,
                                  AutomationsData automationsData,
                                  IBossCountdown bossCountdown)
        {
            _playerData = playerData;
            _badgeData = badgeData;
            _automationsData = automationsData;
            _bossCountdown = bossCountdown;
        }

        public void CreateNewBadge()
        {
            ResetBadgeHp();
            ResetBadgeCoinsReward();
            if (_playerData.Level % 5 == 0)
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
            //_badgeOutput.SpawnBoss();
            CreateBossEvent?.Invoke();
            _bossCountdown.StartCountdown(_playerData.BossCountdownTime);
        }

        public void InitBadge()
        {
            //_badgeOutput.SpawnBadge();
            CreateBadgeEvent?.Invoke();
        }

        public void OnBossNotDefeated()
        {
            _playerData.Level -= 1;
            _playerData.MaxLevelProgress = 10;
            _playerData.LevelProgress = 0;
            CreateNewBadge();
        }

        public void ClickProgress()
        {
            _badgeData.CurrentHp += _automationsData.ClickPower;

            AddBadgeProgress();
        }

        public void TakeProgress()
        {
            _badgeData.CurrentHp += _automationsData.AutomationsPower + _automationsData.AutomationsPower * _playerData.DamageBonus / 100;

            AddBadgeProgress();
        }

        private void ResetBadgeHp()
        {
            float exponent = 1f;
            for (int i = 0; i < _playerData.Level - 1; i++)
            {
                exponent *= 1.55f;
            }
            int maxBadgeHp = (int)(10 * ((_playerData.Level - 1) + exponent));
            if (_playerData.Level % 5 == 0)
                maxBadgeHp *= 10;
            _badgeData.MaxHp = maxBadgeHp;
            _badgeData.CurrentHp = 0;
        }

        private void ResetBadgeCoinsReward()
        {
            float value = _badgeData.MaxHp / 15f * ((_playerData.Level > 75) ? Mathf.Min(3, Mathf.Pow(1.025f, _playerData.Level - 75)) : 1);
            _badgeData.CoinsReward = Mathf.CeilToInt(value);
        }

        private void AddBadgeProgress()
        {
            if (_badgeData.CurrentHp >= _badgeData.MaxHp)
            {
                if (_playerData.Level % 5 == 0)
                    _bossCountdown.StopCountdown();
                if (_playerData.Level >= 105 && _playerData.Level % 5 == 0)
                    _playerData.BadgePoints += 1;
                if (_playerData.Level == 60)
                    _playerData.BadgePoints += 1;

                IncreaseLevel();
                
                CreateNewBadge();
                BadgeCreated?.Invoke();
            }
        }

        private void IncreaseLevel()
        {
            if (_playerData.NeedToIncreaseLevel)
            {
                if (_playerData.LevelProgress == _playerData.MaxLevelProgress)
                {
                    _playerData.Level += 1;
                    _playerData.LevelProgress = 1;
                    if (_playerData.Level % 5 == 0)
                        _playerData.MaxLevelProgress = 1;
                    else
                        _playerData.MaxLevelProgress = 10;
                }
                else
                {
                    _playerData.LevelProgress += 1;
                }
            }
        }
    }
}
