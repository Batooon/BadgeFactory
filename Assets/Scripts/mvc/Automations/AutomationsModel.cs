using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System;

public interface IJsonSerializer
{
    void Serialize(object item, string path);
    T Deserialize<T>(string path);
}

public class AutomationsModel : IJsonSerializer
{
    public IPlayerDataProvider PlayerData;
    public int UnlockedAutomationsAmount;
    //public AutomationsData AutomationData;
    public List<CurrentPlayerAutomationData> PlayerAutoamtionData;

    public AutomationsModel(IPlayerDataProvider playerData)
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
            //AutomationData = FileOperations.Deserialize<AutomationsData>(Path.Combine(Application.persistentDataPath, "Automations.json"));
        }
        catch (Exception e)
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

    public void Serialize(object item, string path)
    {
        StreamWriter writer = new StreamWriter(path);
        string file = JsonUtility.ToJson(item, true);
        writer.Write(file);
        writer.Close();
    }

    public T Deserialize<T>(string path)
    {
        StreamReader reader = new StreamReader(path);
        string file = reader.ReadToEnd();
        T deserialized = JsonUtility.FromJson<T>(file);
        reader.Close();
        return deserialized;
    }
}
