using Automation;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace Automations
{
    public class AutomationsPresentation : MonoBehaviour
    {
        [SerializeField]
        private TextMeshProUGUI _clickPowerText;
        [SerializeField]
        private TextMeshProUGUI _automationsPowerText;
        /*[SerializeField]
        private TextMeshProUGUI _levelsToUpgradeText;*/

        private List<GameObject> _automationObjects = new List<GameObject>();

        public void SetUIValues(string clickPower, string automationsPower)
        {
            _clickPowerText.text = clickPower;
            _automationsPowerText.text = automationsPower;
        }

        public void UnlockNewAutomation(int newAutomationId)
        {
            _automationObjects[newAutomationId].SetActive(true);
        }

        public void AddAutomation(GameObject automation)
        {
            _automationObjects.Add(automation);
        }

        public void UpdateClickPower(string clickPower)
        {
            _clickPowerText.text = clickPower;
        }

        public void UpdateAutomationsPower(string automationsPower)
        {
            _automationsPowerText.text = automationsPower;
        }

        private void Awake()
        {
            foreach (Transform child in transform)
            {
                if (child.gameObject.GetComponent<AutomationLogic>()!=null)
                    _automationObjects.Add(child.gameObject);
            }
        }
    }
}
