using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System;

public class AutomationsModel
{
    public IPlayerData PlayerData;
    public int UnlockedAutomationsAmount;
    public AutomationsData AutomationData;

    public AutomationsModel(IPlayerData playerData)
    {
        PlayerData = playerData;
        Reset();
        Load();
    }

    private void Load()
    {
        //TODO: Load sprites from Asset Bundles
        try
        {
            AutomationData = FileOperations.Deserialize<AutomationsData>(Path.Combine(Application.persistentDataPath, "Automations.json"));
        }
        catch(Exception e)
        {
            Debug.LogError(e);
        }
    }

    public void Save()
    {
        Debug.Log("Сохраняем данные автомаций (☞ﾟヮﾟ)☞");
    }

    private void Reset()
    {

    }
}
