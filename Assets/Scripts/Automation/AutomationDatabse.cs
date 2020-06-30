using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

namespace Automation
{
    public class OverallAutomationsData
    {
        public event Action<int> ClickPowerChanged;
        public event Action<int> AutomationsPowerChanged;
        public event Action<bool> UpgradeAvailable;

        private int _clickPower;
        private int _automationsPower;
        private bool _canUpgradeSomething;

        public int ClickPower
        {
            get
            {
                return _clickPower;
            }
            set
            {
                if (_clickPower != value)
                {
                    _clickPower = value;
                    ClickPowerChanged?.Invoke(value);
                }
            }
        }

        public int AutomationsPower
        {
            get
            {
                return _automationsPower;
            }
            set
            {
                if (_automationsPower != value)
                {
                    _automationsPower = value;
                    AutomationsPowerChanged?.Invoke(value);
                }
            }
        }

        public bool CanUpgradeSomething
        {
            get
            {
                return _canUpgradeSomething;
            }
            set
            {
                if (_canUpgradeSomething != value)
                {
                    _canUpgradeSomething = value;
                    UpgradeAvailable?.Invoke(value);
                }
            }
        }
    }

    [Serializable]
    public class AutomationsData : ISerializationCallbackReceiver
    {
        public int ClickPower;
        public int AutomationsPower;
        public bool CanUpgradeSomething;
        //TODO: количество уровней для улучшения

        public OverallAutomationsData OverallData = new OverallAutomationsData();
        public List<SerializedCurrentPLayerAutomationData> AutomationData = new List<SerializedCurrentPLayerAutomationData>();

        public void OnAfterDeserialize()
        {
            OverallData.ClickPower = ClickPower;
            OverallData.AutomationsPower = AutomationsPower;
            OverallData.CanUpgradeSomething = CanUpgradeSomething;
        }

        public void OnBeforeSerialize()
        {
            ClickPower = OverallData.ClickPower;
            AutomationsPower = OverallData.AutomationsPower;
            CanUpgradeSomething = OverallData.CanUpgradeSomething;
        }
    }

    public class AutomationDatabse : IAutomationDatabase
    {
        private static AutomationDatabse _singleton;

        private const string _automationDataPath = "Automations.json";

        private AutomationsData _automationsData;

        public static AutomationDatabse GetAutomationDatabase()
        {
            if (_singleton == null)
                return _singleton = new AutomationDatabse();

            return _singleton;
        }

        private AutomationDatabse()
        {
            Deserialize();
        }

        public OverallAutomationsData GetOverallAutomationsData()
        {
            return _automationsData.OverallData;
        }

        public CurrentPlayerAutomationData GetAutomationData(int automationId)
        {
            return _automationsData.AutomationData[automationId].playerAutomationData;
        }

        public void SaveAutomationData(CurrentPlayerAutomationData automationData, int automationID)
        {
            SerializedCurrentPLayerAutomationData automation = new SerializedCurrentPLayerAutomationData();
            automation.playerAutomationData = automationData;
            automation.CanUpgrade = automationData.CanUpgrade;
            if (_automationsData.AutomationData.Count - 1 < automationID)
            {
                _automationsData.AutomationData.Add(automation);
            }
            else
            _automationsData.AutomationData[automationID] = automation;
        }

        public bool CanUpgradeSomething()
        {
            foreach (var automation in _automationsData.AutomationData)
            {
                if (automation.CanUpgrade)
                    return true;
            }
            return false;
        }

        public void Serialize()
        {
            FileOperations.Serialize(_automationsData,_automationDataPath);
        }

        public int GetAutomationsLength()
        {
            return _automationsData.AutomationData.Count;
        }

        public int GetLastUnlockedAutomationId()
        {
            for (int i = _automationsData.AutomationData.Count - 1; i >= 0; i--)
            {
                if(_automationsData.AutomationData[i].playerAutomationData.IsUnlocked)
                    return i;
            }
            throw new Exception("Нет открытых автомаций. Должна быть как минимум одна!");
        }

        private void Deserialize()
        {
            _automationsData = FileOperations.Deserialize<AutomationsData>(_automationDataPath);
        }
    }
}