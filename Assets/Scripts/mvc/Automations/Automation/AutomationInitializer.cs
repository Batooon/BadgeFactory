using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class AutomationInitializer : MonoBehaviour, IAutomationInitializer
{
    [SerializeField]
    private TextMeshProUGUI _nameText;
    [SerializeField]
    private TextMeshProUGUI _damageText;
    [SerializeField]
    private TextMeshProUGUI _upgradeCostText;
    [SerializeField]
    private Image _automationImage;

    public void Initialize(AutomationData automationData, Sprite automationIcon, IAutomation automation)
    {
        _nameText.text = automationData.Name;
        _automationImage.sprite = automationIcon;
        _damageText.text = automationData.StartingDps.ConvertValue();
        _upgradeCostText.text = automationData.StartingCost.ConvertValue();
        IAutomationLogic automationLogic = GetComponent<IAutomationLogic>();
        automationLogic.SetAutomationType(automation);
    }
}
