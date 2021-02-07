using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

namespace Automations
{
    [Serializable]
    public class UpgradeComponentData : ISubject
    {
        [SerializeField] private SkillType _skillType;
        [SerializeField] private string _iconSkillPath;
        [SerializeField] private string _skillPrefabPath;
        [SerializeField] private int _levelToUnlock;
        [SerializeField] private long _upgradeCost;
        [SerializeField] private float _percentage;
        [SerializeField, HideInInspector] private bool _isComponentUnlocked;
        [SerializeField, HideInInspector] private bool _isUpgradeComponentPurchased;

        public SkillType Skill => _skillType;
        public string IconSkillPath => _iconSkillPath;
        public string SkillPrefabPath => _skillPrefabPath;
        public int LevelToUnlock { get => _levelToUnlock; set => _levelToUnlock = value; }
        public long UpgradeCost { get => _upgradeCost; set => _upgradeCost = value; }
        public float Percentage { get => _percentage; set => _percentage = value; }
        public bool IsComponentUnlocked
        {
            get => _isComponentUnlocked;
            set
            {
                _isComponentUnlocked = value;
                Notify();
            }
        }

        public bool IsUpgradeComponentPurchased
        {
            get => _isUpgradeComponentPurchased;
            set
            {
                _isUpgradeComponentPurchased = value;
                Notify();
            }
        }

        private List<IObserver> _observers;

        public void ResetData()
        {
            IsComponentUnlocked = false;
            IsUpgradeComponentPurchased = false;
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
                observer.Fetch(this);
        }
    }
}