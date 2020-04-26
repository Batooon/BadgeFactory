using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public interface IAutomationUpgrader
{
    void UpgradeAutomation(CurrentPlayerAutomationData automationData);
}

public interface IAutomationBusinessOutput
{
    void AutomationUpgraded(CurrentPlayerAutomationData autoamtionData);
}

public interface IAutomationBusinessInput
{
    void TryUpgradeAutomation(AutomationUpgradeParams automationUpgradeParams);
}

public struct AutomationUpgradeParams
{
    public CurrentPlayerAutomationData AutomationData;
    public IAutomation Automation;
}

public class AutomationBusinessRules : IAutomationBusinessInput
{
    private IPlayerDataProvider _playerData;
    private IAutomationBusinessOutput _automationOutput;

    public AutomationBusinessRules(IAutomationBusinessOutput automationBusinessOutput)
    {
        _automationOutput = automationBusinessOutput;
    }

    public void TryUpgradeAutomation(AutomationUpgradeParams automationUpgradeParams)
    {
        //Data playerData = _playerData.GetPlayerData();
        Data playerData = new Data();
        if (playerData.GoldAmount >= automationUpgradeParams.AutomationData.Cost)
        {
            //TODO: сделать улучшение сразу же на несколько уровней
            automationUpgradeParams.Automation.Upgrade(ref automationUpgradeParams.AutomationData);
            _automationOutput.AutomationUpgraded(automationUpgradeParams.AutomationData);
        }
        else
        {
            //предложить докупить валюту за кристалы
            //Посмотреть какое-то количество рекламы или задонатить
        }
    }
}

public class AutomationPresentator : IAutomationBusinessOutput
{
    private AutomationPresentation _automationPresentation;

    public AutomationPresentator(AutomationPresentation automationPresentation)
    {
        _automationPresentation = automationPresentation;
    }

    public void AutomationUpgraded(CurrentPlayerAutomationData automationData)
    {
        AutomationViewModel automationParams;
        automationParams.AutomationCost = automationData.Cost.ConvertValue();
        automationParams.AutomationDamage = automationData.DamagePerSecond.ConvertValue();
        _automationPresentation.OnAutomationUpgraded(automationParams);
    }
}
