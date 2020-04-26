using System;
using TMPro;
using UnityEngine;

public struct AutomationViewModel
{
    public string AutomationCost;
    public string AutomationDamage;
    public bool IsEnoughMoney;
}

public class AutomationPresentation : MonoBehaviour
{
    /*#region Events
    public event Action Upgrade;
    #endregion*/

    [SerializeField]
    private TextMeshProUGUI _damageText;
    [SerializeField]
    private TextMeshProUGUI _upgradeCostText;

    public void OnAutomationUpgraded(AutomationViewModel automationParams)
    {
        _damageText.text = automationParams.AutomationDamage;
        _upgradeCostText.text = automationParams.AutomationCost;

        if (!automationParams.IsEnoughMoney)
        {
            //TODO: делать текст красным или что-то тип такого. Чтобы дать понять игроку, что денег не хватает
            _upgradeCostText.faceColor = Color.red;//Сделать так, чтобы дизайнер мог это настраивать
        }
    }

    public void OnAutomationNotUpgraded()
    {
        //предложить докупить валюту за кристалы
        //Посмотреть какое-то количество рекламы или задонатить
    }
}

[Serializable]
public class UsualAutomation : IAutomation
{
    private float _upgradeFactor = 1.07f;

    public void Upgrade(ref CurrentPlayerAutomationData automationData)
    {
        automationData.Level += 1;
        automationData.DamagePerSecond = Mathf.RoundToInt(automationData.StartingDamage * _upgradeFactor * automationData.Level);
    }
}

[Serializable]
public class ClickAutomation : IAutomation
{
    private float _upgradeFactor = 1.07f;

    public void Upgrade(ref CurrentPlayerAutomationData automationData)
    {
        automationData.Level += 1;
        automationData.DamagePerSecond = Mathf.RoundToInt(automationData.StartingDamage * _upgradeFactor * automationData.Level);

        float costFactor = _upgradeFactor;
        for (int i = 0; i < automationData.Level - 1; i++)
            costFactor *= _upgradeFactor;
        automationData.Cost = (int)(automationData.StartingCost * costFactor);
    }
}
