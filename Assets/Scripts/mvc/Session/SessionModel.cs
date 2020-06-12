using UnityEngine;
using System;

[Serializable]
public class SerializedPlayerData : ISerializationCallbackReceiver
{
    public int Level;
    public int LevelProgress;
    public int GoldAmount;
    public Data PlayerData;

    public void OnAfterDeserialize()
    {
        PlayerData.Level = Level;
        PlayerData.LevelProgress = LevelProgress;
        PlayerData.GoldAmount = GoldAmount;
    }

    public void OnBeforeSerialize()
    {
        Level = PlayerData.Level;
        LevelProgress = PlayerData.LevelProgress;
        GoldAmount = PlayerData.GoldAmount;
    }
}

[Serializable]
public class Data
{
    private int _level;
    private int _levelProgress;
    public int MaxLevelProgress;
    private int _goldAmount;
    public bool IsReturningPlayer;
    public int AutomationsAmountUnlocked;
    public int BossCountdownTime;
    public bool NeedToIncreaseLevel;

    public event Action<int> GoldAmountChanged;
    public event Action<int> PlayerLevelChanged;
    public event Action<int> PlayerLevelProgressChanged;

    public int Level
    {
        get => _level;
        set
        {
            if (_level != value)
            {
                PlayerLevelChanged?.Invoke(value);
                _level = value;
            }
        }
    }

    public int LevelProgress
    {
        get => _levelProgress; 
        set
        {
            if (_levelProgress != value)
            {
                PlayerLevelProgressChanged?.Invoke(value);
                _levelProgress = value;
            }
        }
    }

    public int GoldAmount
    {
        get => _goldAmount;
        set
        {
            if (_goldAmount != value)
            {
                GoldAmountChanged?.Invoke(value);
                _goldAmount = value;
            }
        }
    }
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
        //PlayerData.ClickPower = PlayerPrefs.GetInt("ClickPower", 1);
        PlayerData.AutomationsAmountUnlocked = PlayerPrefs.GetInt("UnlockedAutomations", 12);
    }

    public void Save()
    {
        PlayerPrefs.SetInt("IsReturningPlayer", Convert.ToInt32(PlayerData.IsReturningPlayer));
        PlayerPrefs.SetInt("Level", PlayerData.Level);
        //PlayerPrefs.SetInt("ClickPower", PlayerData.ClickPower);
        PlayerPrefs.Save();
    }

    private void DeleteAll()
    {
    }
}
