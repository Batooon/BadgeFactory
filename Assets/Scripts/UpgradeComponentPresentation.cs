using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class UpgradeComponentPresentation : MonoBehaviour
{
    [SerializeField] private Toggle _buyUpgradeToggle;
    [SerializeField] private TextMeshProUGUI _costText;
    [SerializeField] private TextMeshProUGUI _percentageText;
    [SerializeField] private UnityEvent _componentBecomesVisible;
    [SerializeField] private UnityEvent _componentBecomesInvisible;
    [SerializeField] private string _costTemplate;
    [SerializeField] private string _percentageTemplate;

    private PlayerData _playerData;
    private Automation _automationData;
    private UpgradeComponentData _upgradeComponentData;

    public void Init(PlayerData playerData, UpgradeComponentData upgradeComponentData, Automation automation)
    {
        _playerData = playerData;
        _upgradeComponentData = upgradeComponentData;
        _automationData = automation;

        _upgradeComponentData.ComponentVisibilityChanged += OnComponentVisibilityChanged;
        _upgradeComponentData.UpgradeComponentStatechanged += OnComponentPurchasedOrNot;

        OnComponentVisibilityChanged(_upgradeComponentData.IsComponentUnlocked);
        OnComponentPurchasedOrNot(_upgradeComponentData.IsUpgradeComponentPurchased);
        OnGoldAmountChanged(_playerData.Gold);

        _costText.text = string.Format(_costTemplate, _upgradeComponentData.UpgradeCost);
        _percentageText.text = string.Format(_percentageTemplate, _upgradeComponentData.Percentage);
    }

    private void OnEnable()
    {
        if (_upgradeComponentData != null)
        {
            _upgradeComponentData.ComponentVisibilityChanged += OnComponentVisibilityChanged;
            _upgradeComponentData.UpgradeComponentStatechanged += OnComponentPurchasedOrNot;

            OnComponentVisibilityChanged(_upgradeComponentData.IsComponentUnlocked);
            OnComponentPurchasedOrNot(_upgradeComponentData.IsUpgradeComponentPurchased);
        }
        if (_playerData != null)
        {
            _playerData.GoldChanged += OnGoldAmountChanged;
            OnGoldAmountChanged(_playerData.Gold);
        }
    }

    private void OnDisable()
    {
        _upgradeComponentData.ComponentVisibilityChanged -= OnComponentVisibilityChanged;
        _playerData.GoldChanged -= OnGoldAmountChanged;
    }

    private void OnComponentPurchasedOrNot(bool isPurchased)
    {
        if (isPurchased)
            _buyUpgradeToggle.interactable = false;
    }

    private void OnGoldAmountChanged(long newValue)
    {
        _buyUpgradeToggle.interactable = _upgradeComponentData.UpgradeCost <= newValue && _upgradeComponentData.IsComponentUnlocked;
    }

    private void OnComponentVisibilityChanged(bool isVisible)
    {
        if (isVisible)
            _componentBecomesVisible?.Invoke();
        else
            _componentBecomesInvisible?.Invoke();
        OnGoldAmountChanged(_playerData.Gold);
    }
}
