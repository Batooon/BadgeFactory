using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Xml.Serialization;
using System;

/*public struct RuntimeAutomationData
{
    public int Level;
    public int DamagePerSecond;
    public int Cost;
}

public struct AutomationDataContainer
{
    [XmlArray]
    public List<RuntimeAutomationData> AutomationsData;
}*/

public class AutomationsModel
{
    public Data PlayerData;
    public Automation AutomationData;
    public int UnlockedAutomationsAmount;

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
            AutomationData = XmlOperation.Deserialize<Automation>(Path.Combine(Application.persistentDataPath, "Automations.json"));
            //Console.Clear();
            foreach (var item in AutomationData.Automations)
            {
                Debug.Log(item.Name);
            }
        }
        catch(Exception e)
        {
            Debug.LogError(e);
        }

        /*try
        {
            AutomationContainer = XmlOperation.Deserialize<AutomationDataContainer>(Path.Combine(Application.persistentDataPath, "AutomationData.xml"));
            foreach (var item in AutomationContainer.AutomationsData)
            {
                Debug.Log(item.Level);
            }
        }
        catch(Exception e)
        {
            Debug.Log("AutomationsModel.cs не смог получить данные доступных автомаций.<color=red>ЭТО ПЛОХО! ◑﹏◐</color>");
            AutomationContainer.AutomationsData = new List<RuntimeAutomationData>();
            AutomationContainer.AutomationsData.Add(new RuntimeAutomationData());
            XmlOperation.Serialize(AutomationContainer, Path.Combine(Application.persistentDataPath, "AutomationData.xml"));
            //Debug.LogException(e);
        }*/
    }

    public void Save()
    {
        //XmlOperation.Serialize(AutomationContainer, Path.Combine(Application.persistentDataPath, "AutomationData.xml"));
        Debug.Log("Сохраняем данные автомаций (☞ﾟヮﾟ)☞");
    }

    private void Reset()
    {

    }
}
