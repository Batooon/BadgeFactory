using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using System.IO;

[Serializable]
public struct RareBadge
{
    public Sprite AchievedBadge;
    public Sprite ClosedBadge;
    public bool isAchieved;
    public GameObject Content;
}

public class BadgeCollection : MonoBehaviour
{
    public Transform ParentToSpawnBadges;
    public GameObject Badge;
    public Sprite ClosedSprite;
    //public List<RareBadge> RareBadges = new List<RareBadge>();
    public RareBadge[] RareBadges;

    private List<GameObject> _instantiatedBadges = new List<GameObject>();

    private void TryUnlockBadge(EnemyDataVariable createdBadge)
    {
        int badgeId = createdBadge.EnemyDataVar.Id;
        bool isRare = createdBadge.EnemyDataVar.isRare;
        if (isRare && !RareBadges[badgeId].isAchieved)
        {
            RareBadges[badgeId].isAchieved = true;
            _instantiatedBadges[badgeId].GetComponent<Image>().sprite = RareBadges[badgeId].AchievedBadge;
        }
    }

    private void InitBadgesCollection()
    {
        Sprite[] rareSprites = Resources.LoadAll<Sprite>("босс");
        RareBadges = new RareBadge[rareSprites.Length];
        string[] isOpened;
        bool firstTimeVisited = true;

        if (PlayerPrefs.HasKey("Badges"))
        {
            firstTimeVisited = false;
            isOpened = PlayerPrefs.GetString("Badges").Split('\n');
        }
        else
            isOpened = null;

        for (int i = 0; i < rareSprites.Length; i++)
        {
            RareBadge badge = new RareBadge();
            badge.AchievedBadge = rareSprites[i];
            badge.ClosedBadge = ClosedSprite;
            badge.Content = Badge;
            if (firstTimeVisited)
            {
                badge.isAchieved = false;
            }
            else
                badge.isAchieved = Convert.ToBoolean(isOpened[i]);
            if (badge.isAchieved)
                badge.Content.GetComponent<Image>().sprite = badge.AchievedBadge;
            else
                badge.Content.GetComponent<Image>().sprite = badge.ClosedBadge;
            RareBadges[i] = badge;
        }

        for (int i = 0; i < RareBadges.Length; i++)
        {
            GameObject GO = Instantiate(RareBadges[i].Content, ParentToSpawnBadges.position, Quaternion.identity);
            GO.transform.SetParent(ParentToSpawnBadges, false);
            if (RareBadges[i].isAchieved)
                GO.GetComponent<Image>().sprite = RareBadges[i].AchievedBadge;
            else
                GO.GetComponent<Image>().sprite = RareBadges[i].ClosedBadge;

            _instantiatedBadges.Add(GO);
        }
    }

    private void Awake()
    {
        InitBadgesCollection();
    }

    private void Start()
    {
        GameEvents.current.BadgeCreated += TryUnlockBadge;
    }

    private void OnApplicationPause(bool pause)
    {
        if (pause)
        {
            OnPauseOrQuit();
        }
    }

    private void OnApplicationQuit()
    {
        OnPauseOrQuit();
    }

    private void OnPauseOrQuit()
    {
        int[] keys = new int[RareBadges.Length];
        string key = "";
        for (int i = 0; i < RareBadges.Length; i++)
        {
            if (i == RareBadges.Length - 1)
            {
                key += RareBadges[i].isAchieved;
                continue;
            }
            key += RareBadges[i].isAchieved + "\n";
        }
        PlayerPrefs.SetString("Badges", key);
        PlayerPrefs.Save();
    }
}
