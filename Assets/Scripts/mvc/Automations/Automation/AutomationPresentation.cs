using System;
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
    /*#region Events
    public event Action Upgrade;
    #endregion*/

    [SerializeField]
    private TextMeshProUGUI _damageText;
    [SerializeField]
    private TextMeshProUGUI _upgradeCostText;
    [SerializeField]
    private Color _notEnoughMoneyColorText;

    public void OnAutomationUpgraded(AutomationViewModel automationParams)
    {
        _damageText.text = automationParams.AutomationDamage;
        _upgradeCostText.text = automationParams.AutomationCost;

        if (!automationParams.IsEnoughMoney)
        {
            //TODO: делать текст красным или что-то тип такого. Чтобы дать понять игроку, что денег не хватает
            _upgradeCostText.color = _notEnoughMoneyColorText;//Сделать так, чтобы дизайнер мог это настраивать
        }
    }

    public void OnAutomationNotUpgraded()
    {
        //предложить докупить валюту за кристалы
        //Посмотреть какое-то количество рекламы или задонатить
    }
}

[Serializable]
public class UsualAutomation : IAutomation
{
    private float _upgradeFactor = 1.07f;

    public void Upgrade(ref CurrentPlayerAutomationData automationData)
    {
        automationData.Level += 1;
        automationData.DamagePerSecond = Mathf.RoundToInt(automationData.StartingDamage * _upgradeFactor * automationData.Level);
    }
}

[Serializable]
public class ClickAutomation : IAutomation
{
    private float _upgradeFactor = 1.07f;

    public void Upgrade(ref CurrentPlayerAutomationData automationData)
    {
        automationData.Level += 1;
        automationData.DamagePerSecond = Mathf.RoundToInt(automationData.StartingDamage * _upgradeFactor * automationData.Level);

        float costFactor = _upgradeFactor;
        for (int i = 0; i < automationData.Level - 1; i++)
            costFactor *= _upgradeFactor;
        automationData.Cost = (int)(automationData.StartingCost * costFactor);
    }
}

public interface IBadgeBusinessInput
{
    void TakeProgress();
}

public interface IBadgeBusinessOutput
{
    void CreateNewBadge();
    void BadgeGotProgressCallback(BadgeData badgeData);
}

public class BadgeBusinessRules : IBadgeBusinessInput
{
    private IPlayerDataProvider _playerDataProvider;
    private IBadgeDatabase _badgeDatabase;
    private IBadgeBusinessOutput _badgeOutput;

    public BadgeBusinessRules(IPlayerDataProvider playerDataProvider,
                              IBadgeDatabase badgeDatabase,
                              IBadgeBusinessOutput badgeOutput)
    {
        _playerDataProvider = playerDataProvider;
        _badgeDatabase = badgeDatabase;
        _badgeOutput = badgeOutput;
    }

    public void TakeProgress()
    {
        Data playerData = _playerDataProvider.GetPlayerData();
        BadgeData badgeData = _badgeDatabase.GetBadgeData();

        badgeData.CurrentHp += (int)(playerData.AutomationsPower * Time.deltaTime);

        if (badgeData.CurrentHp >= badgeData.MaxHp)
            CreateNewBadge();
        else
            BadgeGotProgress(badgeData);

        _badgeDatabase.SaveBadgeData();
    }

    public void CreateNewBadge()
    {
        _badgeOutput.CreateNewBadge();
    }

    public void BadgeGotProgress(BadgeData badgeData)
    {
        _badgeOutput.BadgeGotProgressCallback(badgeData);
    }
}

public interface IBadgeDatabase
{
    BadgeData GetBadgeData();
    void SaveBadgeData();
}

public class BadgeDatabaseAccess : IBadgeDatabase
{
    private static BadgeDatabaseAccess _singleton;
    private BadgeData _badgeData;

    public static BadgeDatabaseAccess GetBadgeDatabase()
    {
        if(_singleton==null)
            return _singleton = new BadgeDatabaseAccess();

        return _singleton;
    }

    public BadgeData GetBadgeData()
    {
        return _badgeData;
    }

    public void SaveBadgeData()
    {
        throw new NotImplementedException();
    }

    private BadgeDatabaseAccess()
    {

    }
}

[SerializeField]
public struct BadgeData
{
    public int CurrentHp;
    public int MaxHp;
}

public struct BadgeModelView
{

}

public class BadgePresentation : MonoBehaviour
{
    [SerializeField]
    private Image _badgeImage;

    public void UpdateBadgeProgress(float alpha)
    {
        Color tempColor = _badgeImage.color;
        tempColor.a = alpha;

        _badgeImage.color = tempColor;
    }

    public void ShowNewBadge()
    {

    }
}

public class BadgePresentator : IBadgeBusinessOutput
{
    private BadgePresentation _badgePresentation;

    public BadgePresentator(BadgePresentation badgePresentation)
    {
        _badgePresentation = badgePresentation;
    }

    public void BadgeGotProgressCallback(BadgeData badgeData)
    {
        float alpha = Mathf.Clamp01(Mathf.InverseLerp(0, badgeData.MaxHp, badgeData.CurrentHp));

        _badgePresentation.UpdateBadgeProgress(alpha);
    }

    public void CreateNewBadge()
    {
        throw new NotImplementedException();
    }
}

public class Badge : MonoBehaviour
{
    public List<Sprite> Badges;

    private IBadgeBusinessInput _badgeBusinessInput;
    private IBadgeBusinessOutput _badgeOutput;

    private void Awake()
    {
        BadgePresentation badgePresentation = GetComponent<BadgePresentation>();
        _badgeOutput = new BadgePresentator(badgePresentation);
        _badgeBusinessInput = new BadgeBusinessRules(PlayerDataAccess.GetPlayerDatabase(),
                                                     BadgeDatabaseAccess.GetBadgeDatabase(),
                                                     _badgeOutput);
    }

    private void Update()
    {
        _badgeBusinessInput.TakeProgress();
    }
}