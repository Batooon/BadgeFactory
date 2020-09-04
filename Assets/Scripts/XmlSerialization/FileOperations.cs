using System.IO;
using System.Text;
using UnityEngine;

public static class FileOperations
{
    public static void Serialize(object item, string fileName)
    {
        File.WriteAllText(GetFilePath(fileName), JsonUtility.ToJson(item));
    }

    public static T Deserialize<T>(string fileName)
    {
        return JsonUtility.FromJson<T>(File.ReadAllText(GetFilePath(fileName)));
    }

    public static bool IsFileExist(string fileName)
    {
        return File.Exists(GetFilePath(fileName));
    }

    public static void DeleteFile(string fileName)
    {
        if (IsFileExist(fileName))
            File.Delete(GetFilePath(fileName));
    }

    public static byte[] GetBytes(object item)
    {
        string json = JsonUtility.ToJson(item);
        byte[] byteArray = Encoding.UTF8.GetBytes(json);
        return byteArray;
    }

    public static T GetDataFromBytes<T>(byte[] data)
    {
        return JsonUtility.FromJson<T>(Encoding.UTF8.GetString(data));
    }

    private static string GetFilePath(string fileName)
    {
        return Path.Combine(Application.persistentDataPath, fileName);
    }
}
