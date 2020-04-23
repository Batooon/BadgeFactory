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
    void OutputData<T>(T value);
}

public interface IAutomationBusinessInput
{
    void TryUpgradeAutomation(CurrentPlayerAutomationData automationData);
}

public class AutomationBusinessRules : IAutomationBusinessInput
{
    public event Action AutomationUpgraded;

    public void TryUpgradeAutomation(CurrentPlayerAutomationData automationData)
    {
    }
}

public class PlayerNusinessRules : IAutomationBusinessInput
{
    private Data _playerData;
    private IAutomationUpgrader _automationUpgrader;

    public void TryUpgradeAutomation(CurrentPlayerAutomationData automationData)
    {
        if (_playerData.GoldAmount >= automationData.Cost)
        {
            _automationUpgrader.UpgradeAutomation(automationData);
            //TODO: остановился на этом месте
        }
    }
}
