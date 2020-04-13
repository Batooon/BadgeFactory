using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class AutomationPresentation : MonoBehaviour //Automation View
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

    public void InitAutomation(AutomationEditorParams automationData)
    {
        _upgradeButton.onClick.AddListener(UpgradeButtonPressed);
        _automationImage.sprite = automationData.Icon;
        /*_nameText.text = automationData.Name;
        _damageText.text = automationData.StartingDamagePerSecond.ConvertValue();
        _upgradeCostText.text = automationData.StartingCost.ConvertValue();*/
    }

    public void FetchDamage(int newDamage,bool isUpgradeButtonInteractable)
    {
        _damageText.text = newDamage.ConvertValue();
        _upgradeButton.interactable = isUpgradeButtonInteractable;
    }

    public void FetchCost(int newCost) => _upgradeCostText.text = newCost.ConvertValue();

    public void UpgradeButtonPressed()
    {
        Upgrade?.Invoke();
    }
}

public interface IAutomationLogic
{
    void SetAutomationType(IAutomation automation);
}

public class AutomationModel //Automation Model
{
    private IAutomation _automation;
    public IAutomation Automation
    {
        get => _automation;
        private set { }
    }

    private AutomationEditorParams _automationData;
    public AutomationEditorParams AutomationParams
    {
        get => _automationData;
        private set { }
    }
    private IPlayerData _playerData;
    public Data PlayerStats
    {
        get => _playerData.GetPlayerData();
        private set { }
    }

    public AutomationModel(IAutomation automation, AutomationEditorParams automationData, IPlayerData playerData)
    {
        _automation = automation;
        _automationData = automationData;
        _playerData = playerData;
    }
}

public struct AutomationUpgradeParams
{
    public int startingDpsValue;
    public int Startingcost;
}

[Serializable]
public class UsualAutomation : IAutomation
{
    public void Upgrade(ref int currentLevel, ref int currentDpsValue, ref int currentCost, AutomationUpgradeParams automationUpgradeParams)
    {
        currentLevel += 1;
        currentDpsValue = Mathf.RoundToInt(automationUpgradeParams.startingDpsValue * 1.07f * currentLevel);
    }
}

[Serializable]
public class ClickAutomation : IAutomation
{
    public void Upgrade(ref int currentLevel, ref int currentDpsValue, ref int currentCos, AutomationUpgradeParams automationUpgradeParams)
    {
        currentLevel += 1;
        currentDpsValue = Mathf.RoundToInt(automationUpgradeParams.startingDpsValue * 1.07f * currentLevel);
    }
}
