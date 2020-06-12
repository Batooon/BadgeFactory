using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class AutomationsStatsPresentation : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI _clickPowerText;
    [SerializeField]
    private TextMeshProUGUI _automationsPowerText;

    private void UpdateClickPowerText(string clickPower)
    {
        _clickPowerText.text = clickPower;
    }

    private void UpdateAutomationPowerText(string automationsPower)
    {
        _automationsPowerText.text = automationsPower;
    }
}
