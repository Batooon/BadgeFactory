using System.Collections.Generic;
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
    [SerializeField] private TextMeshProUGUI _levelText;
    [SerializeField] private string _levelTextTemplate;
    [SerializeField] private string _damageTextTemplate;
    [SerializeField] private Color _notEnoughMoneyColorText;
    [SerializeField] private Color _defaultMoneyColorText;
    [SerializeField] private Button _upgradeButton;
    [SerializeField] private List<int> _starsLevels;
    [SerializeField] private List<Image> _starPlaceholders;

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

    private void Start()
    {
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
        _levelText.text = string.Format(_levelTextTemplate, newLevel);
        for (int i = _starsLevels.Count - 1; i >= 0; i--)
        {
            if (i >= _starPlaceholders.Count)
                return;

            Color tempColor = _starPlaceholders[i].color;
            tempColor.a = newLevel >= _starsLevels[i] ? 255 : 0;
            _starPlaceholders[i].color = tempColor;
        }
    }

    private void OnDamageChanged(long newDamage)
    {
        _damageText.text = string.Format(_damageTextTemplate, newDamage.ConvertValue());
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
        //FetchUI(automationParams);
    }

    public void OnAutomationUpgraded(AutomationViewModel automationParams)
    {
        //FetchUI(automationParams);
    }

    public void OnAutomationNotUpgraded()
    {
        //проиграть анимацию кнопки улучшения, тип что нельзя улучшить
        //предложить докупить валюту за кристалы
        //Посмотреть какое-то количество рекламы или задонатить хехехехе ✪ ω ✪
    }
    
    public void FetchUpgradeButton(bool isInteractable)
    {
        //_upgradeButton.interactable = isInteractable;
        //_upgradeCostText.color = isInteractable ? _defaultMoneyColorText : _notEnoughMoneyColorText;
    }
    
    public void FetchCost(long cost)
    {
        //_upgradeCostText.text = cost.ConvertValue();
    }
    /*
    private void FetchUI(AutomationViewModel automationParams)
    {
        _damageText.text = automationParams.AutomationDamage;
        _upgradeCostText.text = automationParams.AutomationCost;

        FetchUpgradeButton(automationParams.IsEnoughMoney);
    }*/
}
