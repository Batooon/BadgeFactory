using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

namespace Automations
{
    public class AutomationsPresentation : MonoBehaviour
    {
        private List<GameObject> _automationObjects;

        public void UnlockNewAutomation(int newAutomationId)
        {
            _automationObjects[newAutomationId].SetActive(true);
        }

        public void AddAutomation(GameObject automation)
        {
            _automationObjects.Add(automation);
        }
    }
}
