using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

[RequireComponent(typeof(Toggle)), RequireComponent(typeof(IUpgrade))]
public class UpgradeComponent : MonoBehaviour
{
    [SerializeField] private int _levelToUnlock;
    [SerializeField] private long _upgradeCost;
    [SerializeField] private float _percentage;
    [SerializeField] private TextMeshProUGUI _percentageText;
    [SerializeField] private string _percentageTemplate;
    [SerializeField] private TextMeshProUGUI _costText;
    [SerializeField] private string _costTemplate;
    [SerializeField] private UnityEvent _powerUpUnlocked;

    private IUpgrade _upgrade;
    private int _automationIndex;
    private AutomationsData _automationsData;
    private Automation _automation;
    private bool _upgradeBuyed;
    private PlayerData _playerData;
    private Toggle _buyUpgradeToggle;
    private bool _isPowerUpUnlocked;

    public void Init(PlayerData playerData, AutomationsData automationsData, Automation automation, int automationIndex)
    {
        _playerData = playerData;
        _automationsData = automationsData;
        _automationIndex = automationIndex;
        _automation = automation;
        _buyUpgradeToggle = GetComponent<Toggle>();
        _upgrade = GetComponent<IUpgrade>();
        _upgradeBuyed = _buyUpgradeToggle.interactable;

        _automation.LevelChanged += OnPlayerLevelChanged;
        _playerData.GoldChanged += OnGoldAmountChanged;
        _percentageText.text = string.Format(_percentageTemplate, _percentage);
        _costText.text = string.Format(_costTemplate, _upgradeCost.ConvertValue());

        OnPlayerLevelChanged(_automation.Level);
        OnGoldAmountChanged(_playerData.Gold);
    }

    private void OnEnable()
    {
        if (_playerData == null)
            return;

        _buyUpgradeToggle.interactable = _automation.Level >= _levelToUnlock && _upgradeCost <= _playerData.Gold;
        _playerData.LevelChanged += OnPlayerLevelChanged;
        _playerData.GoldChanged += OnGoldAmountChanged;
        OnPlayerLevelChanged(_automation.Level);
        OnGoldAmountChanged(_playerData.Gold);
    }

    private void OnDisable()
    {
        _playerData.LevelChanged -= OnPlayerLevelChanged;
        _playerData.GoldChanged -= OnGoldAmountChanged;
    }

    public void OnBuyComponentButtonPressed(bool buyed)
    {
        if (_automation.Level < _levelToUnlock)
            return;

        _playerData.Gold -= _upgradeCost;
        _upgrade.Upgrade(_automationsData, _percentage, _automationIndex);
        _buyUpgradeToggle.interactable = false;
        _upgradeBuyed = true;
    }

    public void OnPlayerLevelChanged(int newLevel)
    {
        if (_upgradeBuyed)
            return;

        if (_automation.Level >= _levelToUnlock)
        {
            _isPowerUpUnlocked = true;
            _powerUpUnlocked?.Invoke();
        }

        _buyUpgradeToggle.interactable = _isPowerUpUnlocked && _upgradeCost <= _playerData.Gold;
    }

    private void OnGoldAmountChanged(long newGoldAmount)
    {
        if (_upgradeBuyed)
            return;
        _buyUpgradeToggle.interactable = _upgradeCost <= newGoldAmount && _isPowerUpUnlocked;
    }
}

public interface IUpgrade
{
    void Upgrade(AutomationsData automationsData, float percentage, int automationIndex);
}
