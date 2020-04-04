using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;
using UnityEngine.UI;

//Developer: Antoshka

public class AutomationPresentation : MonoBehaviour
{
    #region Events
    public event Action Upgrade;
    #endregion

    #region UI Fields
    [SerializeField]
    private Button _upgradeButton;
    [Space]
    [SerializeField]
    private Image _automationImage;
    [SerializeField]
    private TextMeshProUGUI _nameText;
    [SerializeField]
    private TextMeshProUGUI _damageText;
    [SerializeField]
    private TextMeshProUGUI _upgradeCostText;
    #endregion

    public void InitAutomation(AutomationCreationParams automationData)
    {
        _upgradeButton.onClick.AddListener(UpgradeButtonPressed);
        _automationImage.sprite = automationData.Icon;
        _nameText.text = automationData.Name;
        _damageText.text = automationData.StartingDamagePerSecond.ConvertValue();
        _upgradeCostText.text = automationData.StartingCost.ConvertValue();
    }

    public void UpdateDamage(int newDamage,bool isUpgradeButtonInteractable)
    {
        _damageText.text = newDamage.ConvertValue();
        _upgradeButton.interactable = isUpgradeButtonInteractable;
    }

    public void UpdateCost(int newCost) => _upgradeCostText.text = newCost.ConvertValue();

    public void UpgradeButtonPressed()
    {
        Upgrade?.Invoke();
    }
}

public class AutomationData
{
    private IAutomation _automation;
    public IAutomation Automation
    {
        get => _automation;
        private set { }
    }

    private AutomationCreationParams _automationData;
    public AutomationCreationParams AutomationParams
    {
        get => _automationData;
        private set { }
    }
    private Data _playerData;
    public Data PlayerStats
    {
        get => _playerData;
        private set { }
    }

    public AutomationData(IAutomation automation,AutomationCreationParams automationData,Data playerData)
    {
        _automation = automation;
        _automationData = automationData;
        _playerData = playerData;
    }

    public void Upgrade()
    {
        _automation.Upgrade();
    }
}

public class UsualAutomation : IAutomation
{
    public void Upgrade()
    {
        throw new NotImplementedException();
    }
}

public class ClickAutomation : IAutomation
{
    public void Upgrade()
    {
        throw new NotImplementedException();
    }
}
