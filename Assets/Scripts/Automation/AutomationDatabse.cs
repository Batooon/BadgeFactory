using System;
using System.Collections.Generic;
using UnityEngine;

namespace Automation
{
    public class AutomationDatabse : IAutomationDatabase
    {
        private static AutomationDatabse _singleton;

        private List<CurrentPlayerAutomationData> AutomationData = new List<CurrentPlayerAutomationData>();

        public static AutomationDatabse GetAutomaitonDatabase()
        {
            if (_singleton == null)
                return _singleton = new AutomationDatabse();

            return _singleton;
        }

        private AutomationDatabse()
        {
            //Deserialize Automation Data
        }

        public CurrentPlayerAutomationData GetAutomationData(int automationId)
        {
            return new CurrentPlayerAutomationData();
            //return AutomationData[automationId];
        }

        public void SaveAutomationData(CurrentPlayerAutomationData automationData, int automationID)
        {
            Debug.Log("Saving Automation Data(Actually no ✪ ω ✪)");
        }

        public void Serialize()
        {
            Debug.Log("Automations Serialized(no)");
        }

        public int GetAutomationsLength()
        {
            return AutomationData.Count;
        }

        public int GetLastUnlockedAutomationId()
        {
            for (int i = AutomationData.Count; i > 0; i--)
            {
                if(AutomationData[i].IsUnlocked)
                    return i;
            }
            throw new Exception("Нет открытых автомаций. Должна быть как минимум одна!");
        }

        ~AutomationDatabse()
        {
            Serialize();
        }
    }
}