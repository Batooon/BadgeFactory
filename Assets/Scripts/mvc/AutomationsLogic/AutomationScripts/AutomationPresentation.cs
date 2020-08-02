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
    [SerializeField] private TextMeshProUGUI _damageText;
    [SerializeField] private TextMeshProUGUI _upgradeCostText;
    [SerializeField] private Color _notEnoughMoneyColorText;
    [SerializeField] private Color _defaultMoneyColorText;
    [SerializeField] private Button _upgradeButton;

    private Automation _automation;

    public void Init(Automation automation)
    {
        _automation = automation;

        _automation.CanUpgradeChanged += OnUpgradeAvailabilityChanged;
        _automation.CostChanged += OnCostChanged;
        _automation.DamageChanged += OnDamageChanged;
        _automation.LevelChanged += OnLevelChanged;
        _automation.UnlockChanged += OnUnlockedChanged;

        OnUnlockedChanged(_automation.IsUnlocked);
        OnCostChanged(_automation.CurrentCost);
        OnDamageChanged(_automation.CurrentDamage);
        OnLevelChanged(_automation.Level);
        OnUpgradeAvailabilityChanged(_automation.CanUpgrade);
    }

    private void OnEnable()
    {
        if (_automation == null)
            return;

        _automation.CanUpgradeChanged += OnUpgradeAvailabilityChanged;
        _automation.CostChanged += OnCostChanged;
        _automation.DamageChanged += OnDamageChanged;
        _automation.LevelChanged += OnLevelChanged;
        _automation.UnlockChanged += OnUnlockedChanged;

        OnUnlockedChanged(_automation.IsUnlocked);
        OnCostChanged(_automation.CurrentCost);
        OnDamageChanged(_automation.CurrentDamage);
        OnLevelChanged(_automation.Level);
        OnUpgradeAvailabilityChanged(_automation.CanUpgrade);
    }

    private void OnDisable()
    {
        if (_automation == null)
            return;

        _automation.CanUpgradeChanged -= OnUpgradeAvailabilityChanged;
        _automation.CostChanged -= OnCostChanged;
        _automation.DamageChanged -= OnDamageChanged;
        _automation.LevelChanged -= OnLevelChanged;
        _automation.UnlockChanged -= OnUnlockedChanged;
    }

    private void OnCostChanged(long newCost)
    {
        _upgradeCostText.text = newCost.ConvertValue();
    }
    
    private void OnLevelChanged(int newLevel)
    {

    }

    private void OnDamageChanged(long newDamage)
    {
        _damageText.text = newDamage.ConvertValue();
    }

    private void OnUpgradeAvailabilityChanged(bool canUpgrade)
    {
        _upgradeCostText.color = canUpgrade ? _defaultMoneyColorText : _notEnoughMoneyColorText;
    }

    private void OnUnlockedChanged(bool unlocked)
    {
        gameObject.SetActive(unlocked);
    }

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
        //Посмотреть какое-то количество рекламы или задонатить хехехехе ✪ ω ✪
    }

    public void FetchUpgradeButton(bool isInteractable)
    {
        _upgradeButton.interactable = isInteractable;
        _upgradeCostText.color = isInteractable ? _defaultMoneyColorText : _notEnoughMoneyColorText;
    }

    public void FetchCost(long cost)
    {
        _upgradeCostText.text = cost.ConvertValue();
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

    public void RecalculateCost(int levelsToUpgrade,Automation automationData)
    {
        int level = automationData.Level;
        long newCost = 0;

        if (automationData.Level == 0 && levelsToUpgrade == 1)
        {
            automationData.CurrentCost = automationData.StartingCost;
            return;
        }

        for (int i = 0; i < levelsToUpgrade; i++)
        {
            newCost += Mathf.FloorToInt(automationData.StartingCost * Mathf.Pow(_upgradeFactor, level - 1));
            level += 1;
        }

        automationData.CurrentCost = newCost;
    }

    public void Upgrade(Automation automationData, AutomationsData automationsData)
    {
        long newDamage = 0;

        if (automationData.Level != 0)
            automationsData.AutomationsPower -= automationData.CurrentDamage;

        for (int i = 0; i < automationsData.LevelsToUpgrade; i++)
        {
            automationData.Level += 1;
            newDamage += Mathf.RoundToInt(automationData.StartingDamage * _upgradeFactor * automationData.Level);
            if (automationData.Level >= 200 && automationData.Level % 25 == 0 && automationData.Level < 4000)
                newDamage *= 4;
            if (automationData.Level >= 1000 && automationData.Level % 1000 == 0 && automationData.Level < 4000)
                newDamage *= 10;
        }

        automationData.CurrentDamage = newDamage;
        automationsData.AutomationsPower += automationData.CurrentDamage;
        RecalculateCost(automationsData.LevelsToUpgrade, automationData);
    }
}

[Serializable]
public class ClickAutomation : IAutomation
{
    private const float _upgradeFactor = 1.07f;

    public void RecalculateCost(int levelsToUpgrade, Automation automationData)
    {
        int level = automationData.Level;
        long newCost = 0;

        if (automationData.Level == 0 && levelsToUpgrade == 1)
        {
            automationData.CurrentCost = automationData.StartingCost;
            return;
        }

        for (int i = 0; i < levelsToUpgrade; i++)
        {
            if (level < 15)
            {
                newCost += Mathf.FloorToInt((5 + level) * Mathf.Pow(_upgradeFactor, level - 1));
                level += 1;
            }
            else
            {
                newCost += Mathf.FloorToInt(20 * Mathf.Pow(_upgradeFactor, level - 1));
                level += 1;
            }
        }
        automationData.CurrentCost = newCost;
    }

    public void Upgrade(Automation automationData, AutomationsData automationsData)
    {
        long newDamage = 0;

        if (automationData.Level != 1)
            automationsData.ClickPower -= automationData.CurrentDamage;

        for (int i = 0; i < automationsData.LevelsToUpgrade; i++)
        {
            automationData.Level += 1;
            newDamage += Mathf.RoundToInt(automationData.StartingDamage * _upgradeFactor * automationData.Level);
        }

        automationData.CurrentDamage = newDamage;
        automationsData.ClickPower += automationData.CurrentDamage;
        RecalculateCost(automationsData.LevelsToUpgrade, automationData);
    }
}
