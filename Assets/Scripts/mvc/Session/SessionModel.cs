using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public struct Data
{
    public int Level;
    public int LevelProgress;
    public int MaxLevelProgress;
    public int GoldAmount;
    public bool IsReturningPlayer;
    public int ClickPower;
    public int AutomationsPower;
    public int AutomationsAmountUnlocked;
    public int BossCountdownTime;
    public bool NeedToIncreaseLevel;
    //В будущем добавить тут кристаллы
    //public int Gems;
}

public class SessionModel
{
    #region data Variables
    public Data PlayerData;
    #endregion

    public SessionModel()
    {
        DeleteAll();
        Load();
    }

    private void Load()
    {
        PlayerData.IsReturningPlayer = Convert.ToBoolean(PlayerPrefs.GetInt("IsReturningPlayer", 0));
        PlayerData.Level = PlayerPrefs.GetInt("Level", 1);
        PlayerData.GoldAmount = PlayerPrefs.GetInt("Gold", 0);
        PlayerData.ClickPower = PlayerPrefs.GetInt("ClickPower", 1);
        PlayerData.AutomationsAmountUnlocked = PlayerPrefs.GetInt("UnlockedAutomations", 12);
    }

    public void Save()
    {
        PlayerPrefs.SetInt("IsReturningPlayer", Convert.ToInt32(PlayerData.IsReturningPlayer));
        PlayerPrefs.SetInt("Level", PlayerData.Level);
        PlayerPrefs.SetInt("ClickPower", PlayerData.ClickPower);
        PlayerPrefs.Save();
    }

    private void DeleteAll()
    {
    }
}
