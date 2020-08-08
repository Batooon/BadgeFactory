using TMPro;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class UpgradeComponent : MonoBehaviour
{
    [SerializeField] private int _levelToUnlock;
    [SerializeField] private long _upgradeCost;
    [SerializeField] private int _percentage;
    [SerializeField] private TextMeshProUGUI _percentageText;
    [SerializeField] private string _percentageTemplate;
    [SerializeField] private TextMeshProUGUI _costText;
    [SerializeField] private string _costTemplate;
    [SerializeField, RequireInterface(typeof(IUpgrade))] private Object _upgrade;

    private int _automationIndex;
    private AutomationsData _automationsData;
    private Automation _automation;

    private IUpgrade Upgrade => _upgrade as IUpgrade;

    private PlayerData _playerData;
    private Button _buyUpgradeButton;
    private bool _isPowerUpUnlocked;

    public void Init(PlayerData playerData, AutomationsData automationsData, Automation automation, int automationIndex)
    {
        _playerData = playerData;
        _automationsData = automationsData;
        _automationIndex = automationIndex;
        _automation = automation;
        _buyUpgradeButton = GetComponent<Button>();

        _buyUpgradeButton.interactable = _playerData.Level >= _levelToUnlock;
        _automation.LevelChanged += OnPlayerLevelChanged;
        _percentageText.text = string.Format(_percentageTemplate, _percentage);
        _costText.text = string.Format(_costTemplate, _upgradeCost.ConvertValue());
    }

    private void OnEnable()
    {
        if (_playerData == null)
            return;

        _buyUpgradeButton.interactable = _automation.Level >= _levelToUnlock && _upgradeCost <= _playerData.Gold;
        _playerData.LevelChanged += OnPlayerLevelChanged;
        _buyUpgradeButton.onClick.AddListener(OnBuyComponentButtonPressed);
    }

    private void OnDisable()
    {
        _playerData.LevelChanged -= OnPlayerLevelChanged;
        _buyUpgradeButton.onClick.RemoveListener(OnBuyComponentButtonPressed);
    }

    private void OnBuyComponentButtonPressed()
    {
        if (_automation.Level < _levelToUnlock)
            return;

        _playerData.Gold -= _upgradeCost;
        Upgrade.Upgrade(_automationsData, _percentage, _automationIndex);
    }

    public void OnPlayerLevelChanged(int newLevel)
    {
        _isPowerUpUnlocked = _automation.Level >= _levelToUnlock;
        _buyUpgradeButton.interactable = _isPowerUpUnlocked && _upgradeCost <= _playerData.Gold;
    }

    private void OnGoldAmountChanged(long newGoldAmount)
    {
        _buyUpgradeButton.interactable = _upgradeCost <= newGoldAmount && _isPowerUpUnlocked;
    }
}

public interface IUpgrade
{
    void Upgrade(AutomationsData automationsData, int percentage, int automationIndex);
}
