using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System;

public class AutomationsModel
{
    public Data PlayerData;
    public int UnlockedAutomationsAmount;
    public AutomationsData AutomationData;

    public AutomationsModel(Data playerData)
    {
        PlayerData = playerData;
        Reset();
        Load();
    }

    private void Load()
    {
        try
        {
            AutomationData = XmlOperation.Deserialize<AutomationsData>(Path.Combine(Application.persistentDataPath, "Automations.json"));
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
