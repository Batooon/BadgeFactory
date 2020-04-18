using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class AutomationInitializer : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI Name;
    [SerializeField]
    private Image AutomationImage;

    public void InitializeAutomation(IAutomation automationType, string Name, Sprite Icon)
    {
        this.Name.text = Name;
        AutomationImage.sprite = Icon;
        AutomationLogic automationLogic = gameObject.GetComponent<AutomationLogic>();
        automationLogic.SetAutomationType(automationType);
    }
}