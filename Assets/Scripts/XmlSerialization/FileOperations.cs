using System.IO;
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
        File.Delete(GetFilePath(fileName));
    }

    private static string GetFilePath(string fileName)
    {
        return Path.Combine(Application.persistentDataPath, fileName);
    }
}
