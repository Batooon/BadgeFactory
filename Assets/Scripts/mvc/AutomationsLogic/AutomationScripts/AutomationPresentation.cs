using Automation;
using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public struct AutomationViewModel
{
    public string AutomationCost;
    public string AutomationDamage;
    public bool IsEnoughMoney;
}

public class AutomationPresentation : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI _damageText;
    [SerializeField]
    private TextMeshProUGUI _upgradeCostText;
    [SerializeField]
    private Color _notEnoughMoneyColorText;
    [SerializeField]
    private Color _defaultMoneyColorText;
    [SerializeField]
    private Button _upgradeButton;

    public void SetUpAutomation(AutomationViewModel automationParams)
    {
        FetchUI(automationParams);
    }

    public void OnAutomationUpgraded(AutomationViewModel automationParams)
    {
        //сделать ещё какой-то эффект улучшения, что-ли
        FetchUI(automationParams);
    }

    public void OnAutomationNotUpgraded()
    {
        //проиграть анимацию кнопки улучшения, тип что нельзя улучшить
        //предложить докупить валюту за кристалы
        //Посмотреть какое-то количество рекламы или задонатить хехехехе✪ ω ✪
    }

    public void FetchUpgradeButton(bool isInteractable)
    {
        _upgradeButton.interactable = isInteractable;
        _upgradeCostText.color = isInteractable ? _defaultMoneyColorText : _notEnoughMoneyColorText;
    }

    private void FetchUI(AutomationViewModel automationParams)
    {
        _damageText.text = automationParams.AutomationDamage;
        _upgradeCostText.text = automationParams.AutomationCost;

        FetchUpgradeButton(automationParams.IsEnoughMoney);
    }
}

[Serializable]
public class UsualAutomation : IAutomation
{
    private const float _upgradeFactor = 1.07f;

    public void Upgrade(CurrentPlayerAutomationData automationData, OverallAutomationsData overallAutomationsData)
    {
        if (automationData.Level != 0)
            overallAutomationsData.AutomationsPower -= automationData.DamagePerSecond;
        automationData.Level += 1;
        automationData.DamagePerSecond = Mathf.RoundToInt(automationData.StartingDamage * _upgradeFactor * automationData.Level);
        overallAutomationsData.AutomationsPower += automationData.DamagePerSecond;

        float costFactor = 1;
        for (int i = 0; i < automationData.Level - 1; i++)
            costFactor *= _upgradeFactor;
        automationData.Cost = (int)(automationData.StartingCost * costFactor);
    }
}

[Serializable]
public class ClickAutomation : IAutomation
{
    private const float _upgradeFactor = 1.07f;

    public void Upgrade(CurrentPlayerAutomationData automationData, OverallAutomationsData overallAutomationsData)
    {
        if (automationData.Level != 0)
            overallAutomationsData.ClickPower -= automationData.DamagePerSecond;
        automationData.Level += 1;
        automationData.DamagePerSecond = Mathf.RoundToInt(automationData.StartingDamage * _upgradeFactor * automationData.Level);
        overallAutomationsData.ClickPower += automationData.DamagePerSecond;

        float costFactor = 1;
        for (int i = 0; i < automationData.Level - 1; i++)
            costFactor *= _upgradeFactor;
        automationData.Cost = (int)(automationData.StartingCost * costFactor);
    }
}
