using System;
using System.Collections.Generic;
using UnityEngine;

namespace Automations
{
    [Serializable]
    public class Automation : ISubject
    {
        [SerializeField, HideInInspector] private bool _canUpgrade;
        [SerializeField] private long _startingDamage;
        [SerializeField] private long _startingCost;
        [SerializeField] private int _level;
        [SerializeField] private long _currentCost;
        [SerializeField] private long _currentDamage;
        [SerializeField] private bool _isUnlocked;
        [SerializeField] private float _powerUpPercentage;
        [SerializeField] private UpgradeComponentData[] _upgradeComponents;

        private List<IObserver> _observers = new List<IObserver>();

        public bool CanUpgrade
        {
            get => _canUpgrade;
            set
            {
                _canUpgrade = value;
                Notify();
            }
        }

        public long StartingDamage
        {
            get => _startingDamage;
            set => _startingDamage = value;
        }

        public long StartingCost
        {
            get => _startingCost;
            set => _startingCost = value;
        }

        public int Level
        {
            get => _level;
            set
            {
                _level = value;
                Notify();
            }
        }

        public long CurrentCost
        {
            get => _currentCost;
            set
            {
                _currentCost = value;
                Notify();
            }
        }

        public long CurrentDamage
        {
            get => _currentDamage;
            set
            {
                _currentDamage = value;
                Notify();
            }
        }

        public bool IsUnlocked
        {
            get => _isUnlocked;
            set
            {
                _isUnlocked = value;
                Notify();
            }
        }

        public float PowerUpPercentage
        {
            get => _powerUpPercentage;
            set
            {
                float addedPercentage = value - _powerUpPercentage;
                _powerUpPercentage = value;
                Notify();
            }
        }

        public UpgradeComponentData[] UpgradeComponents => _upgradeComponents;

        private bool _startingCanUpgrade;
        private int _startingLevel;
        private bool _startingIsUnlocked;
        private float _startingPowerUpPercentage;

        public void Init()
        {
            _startingCanUpgrade = _canUpgrade;
            _startingLevel = _level;
            _startingIsUnlocked = _isUnlocked;
            _startingPowerUpPercentage = _powerUpPercentage;
        }

        public void ResetData()
        {
            CanUpgrade = _startingCanUpgrade;
            Level = _startingLevel;
            IsUnlocked = _startingIsUnlocked;
            PowerUpPercentage = _startingPowerUpPercentage;
            CurrentCost = _startingCost;
            CurrentDamage = _startingDamage;
            foreach (var item in UpgradeComponents)
                item.ResetData();
        }

        public void Reset(Automation defaultAutomation)
        {
            CanUpgrade = false;
            Level = 0;
            CurrentCost = _startingCost;
            CurrentDamage = _startingDamage;
            PowerUpPercentage = 0;
            IsUnlocked = defaultAutomation.IsUnlocked;
        }

        public void Attach(IObserver observer)
        {
            _observers.Add(observer);
        }

        public void Detach(IObserver observer)
        {
            _observers.Remove(observer);
        }

        public void Notify()
        {
            foreach (var observer in _observers)
            {
                observer.Fetch(this);
            }
        }
    }
}