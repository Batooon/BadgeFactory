using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

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
    private TextMeshProUGUI _damageText;
    [SerializeField]
    private TextMeshProUGUI _upgradeCostText;
    #endregion

    [SerializeField]
    private AutomationLogic _automationLogic;

    private void OnEnable()
    {
        _automationLogic.AutomationUpgraded += UpdateUI;
    }

    private void OnDisable()
    {
        _automationLogic.AutomationUpgraded -= UpdateUI;
    }

    private void UpdateUI(CurrentPlayerAutomationData automationData)
    {

    }

    /*public void InitAutomation(CurrentPlayerAutomationData automationData)
    {
        _upgradeButton.onClick.AddListener(UpgradeButtonPressed);
    }

    public void FetchDamage(int newDamage,bool isUpgradeButtonInteractable)
    {
        _damageText.text = newDamage.ConvertValue();
        _upgradeButton.interactable = isUpgradeButtonInteractable;
    }*/

    public void FetchCost(int newCost) => _upgradeCostText.text = newCost.ConvertValue();

    public void UpgradeButtonPressed()
    {
        _automationLogic.OnUpgradeButtonPressed();
        //Upgrade?.Invoke();
    }
}

public interface IAutomationLogic
{
    void SetAutomationType(IAutomation automation);
}

public struct AutomationUpgradeParams
{
    public int startingDpsValue;
    public int Startingcost;
}

[Serializable]
public class UsualAutomation : IAutomation
{
    /*public void Upgrade(ref int currentLevel, ref int currentDpsValue, ref int currentCost, AutomationUpgradeParams automationUpgradeParams)
    {
        currentLevel += 1;
        currentDpsValue = Mathf.RoundToInt(automationUpgradeParams.startingDpsValue * 1.07f * currentLevel);
    }*/

    public void Upgrade(ref CurrentPlayerAutomationData automationData)
    {
        throw new NotImplementedException();
    }
}

[Serializable]
public class ClickAutomation : IAutomation
{
    private float upgradeFactor = 1.07f;

    public void Upgrade(ref CurrentPlayerAutomationData automationData)
    {
        automationData.Level += 1;
        automationData.DamagePerSecond = Mathf.RoundToInt(automationData.StartingDamage * upgradeFactor * automationData.Level);

        float costFactor = upgradeFactor;
        for (int i = 0; i < automationData.Level - 1; i++)
            costFactor *= upgradeFactor;
        automationData.Cost = (int)(automationData.StartingCost * costFactor);

        throw new NotImplementedException();
    }
}
