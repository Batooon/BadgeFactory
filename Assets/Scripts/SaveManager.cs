using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveParameters
{

}

public class SaveManager : MonoBehaviour
{
    public SaveParameters SaveData;

    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
        Load();
    }

    public void Save()
    {
        PlayerPrefs.SetString("save", Serializer.Serialize<SaveParameters>(SaveData));
    }

    public void Load()
    {
        if (PlayerPrefs.HasKey("save"))
        {
            SaveData = Serializer.Deserialize<SaveParameters>(PlayerPrefs.GetString("save"));
        }
        else
        {
            SaveData = new SaveParameters();
            Save();
            Debug.Log("No file found, creating a new one)");
        }
    }
}
