using AutomationImplementation;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace AutomationsImplementation
{
    public class AutomationsPresentation : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _clickPowerText;
        [SerializeField] private TextMeshProUGUI _automationsPowerText;

        private AutomationsData _automationsData;
        private List<GameObject> _automationObjects = new List<GameObject>();

        public void Init(AutomationsData automationsData)
        {
            _automationsData = automationsData;

            _automationsData.AutomationsPowerChanged += OnAutomationsPowerChanged;
            _automationsData.ClickPowerChanged += OnClickPowerChanged;

            OnAutomationsPowerChanged(_automationsData.AutomationsPower);
            OnClickPowerChanged(_automationsData.ClickPower);
        }

        private void OnEnable()
        {
            if (_automationsData == null)
                return;

            _automationsData.AutomationsPowerChanged += OnAutomationsPowerChanged;
            _automationsData.ClickPowerChanged += OnClickPowerChanged;

            OnAutomationsPowerChanged(_automationsData.AutomationsPower);
            OnClickPowerChanged(_automationsData.ClickPower);
        }

        private void OnDisable()
        {
            _automationsData.AutomationsPowerChanged -= OnAutomationsPowerChanged;
            _automationsData.ClickPowerChanged -= OnClickPowerChanged;
        }

        private void OnAutomationsPowerChanged(long newPower)
        {
            _automationsPowerText.text = newPower.ConvertValue();
        }

        private void OnClickPowerChanged(long newClickPower)
        {
            _clickPowerText.text = newClickPower.ConvertValue();
        }

        //

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
        /*
        public void UpdateClickPower(string clickPower)
        {
            _clickPowerText.text = clickPower;
        }

        public void UpdateAutomationsPower(string automationsPower)
        {
            _automationsPowerText.text = automationsPower;
        }*/
        /*
        public void HideAutomations()
        {
            for (int i = 1; i < _automationObjects.Count; i++)
            {
                _automationObjects[i].SetActive(false);
            }
        }*/

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
