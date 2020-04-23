using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

/// <summary>
/// Contains All Data
/// Save/Load
/// Controller Modifies Data
/// Controllers listen for change
/// </summary>

public class BadgeModel
{
    #region Events
    public event Action HpChanged;
    public event Action BadgesLoaded;
    #endregion
    #region Data Variables
    public float CurrentHp;
    public float MaxHp;
    public float BossCountdown;
    public int CostReward;
    public bool IsBoss;
    public Badge CurrentBadge;

    public IPlayerDataProvider PlayerData;
    #endregion
    #region Graphic Data
    public Sprite[] BadgeImages;
    public Sprite[] BossBadgeImages;

    public List<Badge> Badges = new List<Badge>();
    public List<Badge> BossBadges = new List<Badge>();
    #endregion

    public BadgeModel(IPlayerDataProvider playerData)
    {
        PlayerData = playerData;
        DeleteAll();
        Load();
    }

    public void IncreaseHp()
    {
        CurrentHp += PlayerData.GetPlayerData().ClickPower;
        HpChanged?.Invoke();
    }

    public void Save()
    {
        PlayerPrefs.SetFloat("BossCountdown", BossCountdown);
    }

    public void Load()
    {
        //TODO: Сделать сериализацию через JSON
        BossCountdown = PlayerPrefs.GetFloat("BossCountdown", 30f);

        BadgeImages = Resources.LoadAll<Sprite>("Badges");
        LoadBadges(ref Badges, BadgeImages);

        BossBadgeImages = Resources.LoadAll<Sprite>("Bossess");
        LoadBadges(ref BossBadges, BossBadgeImages, true);
    }

    public void DeleteAll()
    {
    }

    public void InitNewBossData(int hp, int cost)
    {
        CostReward = cost;
        MaxHp = hp;
        CurrentHp = 0;
        IsBoss = true;
        CurrentBadge = BossBadges[UnityEngine.Random.Range(0, BossBadges.Count - 1)];
    }

    public void InitNewBadgeData(int hp, int cost)
    {
        CostReward = cost;
        MaxHp = hp;
        CurrentHp = 0;
        CurrentBadge = Badges[UnityEngine.Random.Range(0, Badges.Count - 1)];
    }

    private void LoadBadges(ref List<Badge> badgeList, Sprite[] images, bool isRare = false)
    {
        for (int i = 0; i < images.Length; i++)
        {
            Badge badge = new Badge();
            badge.BadgeSprite = images[i];
            badge.IsRare = isRare;
            badgeList.Add(badge);
        }
    }
}
