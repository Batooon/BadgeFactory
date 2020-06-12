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

        private int _clickPower;
        private int _automationsPower;

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
    }

    [Serializable]
    public class AutomationsData : ISerializationCallbackReceiver
    {
        public int ClickPower;
        public int AutomationsPower;
        //TODO: количество уровней для улучшения

        public OverallAutomationsData OverallData = new OverallAutomationsData();
        public List<CurrentPlayerAutomationData> AutomationData;

        public void OnAfterDeserialize()
        {
            OverallData.ClickPower = ClickPower;
            OverallData.AutomationsPower = AutomationsPower;
        }

        public void OnBeforeSerialize()
        {
            ClickPower = OverallData.ClickPower;
            AutomationsPower = OverallData.AutomationsPower;
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
            return _automationsData.AutomationData[automationId];
        }

        public void SaveAutomationData(CurrentPlayerAutomationData automationData, int automationID)
        {
            if (_automationsData.AutomationData.Count - 1 < automationID)
            {
                _automationsData.AutomationData.Add(automationData);
            }
            else
            _automationsData.AutomationData[automationID] = automationData;
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
                if(_automationsData.AutomationData[i].IsUnlocked)
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