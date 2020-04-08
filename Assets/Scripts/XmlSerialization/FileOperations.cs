using System.IO;
using System.Xml.Serialization;
using UnityEngine;
using System.Collections.Generic;
using System;

public static class FileOperations
{
    public static void Serialize(object item, string path)
    {
        StreamWriter writer = new StreamWriter(path);
        string file = JsonUtility.ToJson(item, true);
        writer.Write(file);
        writer.Close();
    }

    public static T Deserialize<T>(string path)
    {
        StreamReader reader = new StreamReader(path);
        string file = reader.ReadToEnd();
        T deserialized = JsonUtility.FromJson<T>(file);
        reader.Close();
        return deserialized;
    }

    public static T LoadAssetBundle<T>(string assetBundleName) where T : UnityEngine.Object
    {
        var assetBundle = AssetBundle.LoadFromFile(Path.Combine(Application.streamingAssetsPath, assetBundleName));
        if (assetBundle == null)
        {
            Debug.LogError("Failed to load AssetBundle!");
            throw new DirectoryNotFoundException();
        }
        T asset = assetBundle.LoadAsset<T>("Object");
        return asset;
    }
}
