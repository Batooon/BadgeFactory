using System;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Toggle)), RequireComponent(typeof(IUpgrade)), RequireComponent(typeof(UpgradeComponentPresentation))]
public class UpgradeComponent : MonoBehaviour
{
    [SerializeField] private int _levelToUnlock;
    [SerializeField] private long _upgradeCost;
    [SerializeField] private float _percentage;

    private UpgradeComponentPresentation _upgradeComponentPresentation;
    private UpgradeComponentData _upgradeComponentData;
    private IUpgrade _upgrade;
    private int _automationIndex;
    private AutomationsData _automationsData;
    private Automation _automationData;
    private PlayerData _playerData;

    public void Init(PlayerData playerData, AutomationsData automationsData, Automation automation, UpgradeComponentData upgradeComponentData, int automationIndex)
    {
        _upgradeComponentData = upgradeComponentData;
        _playerData = playerData;
        _automationsData = automationsData;
        _automationIndex = automationIndex;
        _automationData = automation;
        _upgradeComponentPresentation = GetComponent<UpgradeComponentPresentation>();
        _upgradeComponentPresentation.Init(_playerData, _upgradeComponentData, _automationData);
        _upgrade = GetComponent<IUpgrade>();
        Debug.Log("Init Called", gameObject);
    }

    private void OnEnable()
    {
        Debug.Log("OnEnable called", gameObject);
        _automationData.LevelChanged += OnLevelChanged;
        OnLevelChanged(_automationData.Level);
    }

    private void OnDisable()
    {
        _automationData.LevelChanged -= OnLevelChanged;
    }

    public void OnBuyComponentButtonPressed(bool buyed)
    {
        if (_upgradeComponentData.IsUpgradeComponentPurchased)
            return;
        _playerData.Gold -= _upgradeComponentData.UpgradeCost;
        _upgrade.Upgrade(_automationsData, _upgradeComponentData.Percentage, _automationIndex);
        _upgradeComponentData.IsUpgradeComponentPurchased = true;
    }

    public void OnLevelChanged(int newLevel)
    {
        if (_upgradeComponentData.IsUpgradeComponentPurchased)
            return;

        _upgradeComponentData.IsComponentUnlocked = _automationData.Level >= _upgradeComponentData.LevelToUnlock;
    }
}

[Serializable]
public class UpgradeComponentData
{
    [SerializeField] private int _levelToUnlock;
    [SerializeField] private long _upgradeCost;
    [SerializeField] private float _percentage;
    [SerializeField] private bool _isComponentUnlocked;
    [SerializeField] private bool _isupgradeComponentPurchased;

    public event Action<bool> ComponentVisibilityChanged;
    public event Action<bool> UpgradeComponentStatechanged;

    public int LevelToUnlock { get => _levelToUnlock; set => _levelToUnlock = value; }
    public long UpgradeCost { get => _upgradeCost; set => _upgradeCost = value; }
    public float Percentage { get => _percentage; set => _percentage = value; }
    public bool IsComponentUnlocked { get => _isComponentUnlocked; set
        {
            _isComponentUnlocked = value;
            ComponentVisibilityChanged?.Invoke(value);
        }
    }
    public bool IsUpgradeComponentPurchased { get => _isupgradeComponentPurchased; set { _isupgradeComponentPurchased = value; UpgradeComponentStatechanged?.Invoke(value); } }

    private bool _startingIsComponentUnlocked;
    private bool _startingIsUpgradeComponentPurchased;

    public void Init()
    {
        _startingIsComponentUnlocked = _isComponentUnlocked;
        _startingIsUpgradeComponentPurchased = _isupgradeComponentPurchased;
    }

    public void ResetData()
    {
        IsComponentUnlocked = _startingIsComponentUnlocked;
        IsUpgradeComponentPurchased = _startingIsUpgradeComponentPurchased;
    }
}

public interface IUpgrade
{
    void Upgrade(AutomationsData automationsData, float percentage, int automationIndex);
}
