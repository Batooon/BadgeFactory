using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.Networking;

public static class FileOperations
{
    public static void Serialize(object item, string path)
    {
        var bytes = System.Text.Encoding.UTF8.GetBytes(JsonUtility.ToJson(item));
        File.WriteAllBytes(Path.Combine(Application.persistentDataPath, path), bytes);

        //string file = JsonUtility.ToJson(item, true);
        /*StreamWriter writer = new StreamWriter(path);
        string file = JsonUtility.ToJson(item, true);
        writer.Write(file);
        writer.Close();*/
    }

    /*public static IEnumerator<object> Deserialize(string path)
    {
        if (File.Exists(Path.Combine(Application.persistentDataPath, path)))
        {
            StreamReader reader = new StreamReader(Path.Combine(Application.persistentDataPath, path));
            string localFile = reader.ReadToEnd();
            object localDeserialized = JsonUtility.FromJson<object>(localFile);
            reader.Close();
            yield return localDeserialized;
            yield break;
        }
        using (UnityWebRequest unityWebRequest = UnityWebRequest.Get(Path.Combine(Application.streamingAssetsPath, path)))
        {
            yield return unityWebRequest.SendWebRequest();

            string file = unityWebRequest.downloadHandler.text;
            object deserialized = JsonUtility.FromJson<object>(file);
            unityWebRequest.Dispose();
            yield return deserialized;
            yield break;
        }
    }*/

    public static T Deserialize<T>(string path)
    {
        if (File.Exists(Path.Combine(Application.persistentDataPath, path)))
        {
            StreamReader reader = new StreamReader(Path.Combine(Application.persistentDataPath, path));
            string localFile = reader.ReadToEnd();
            T localDeserialized = JsonUtility.FromJson<T>(localFile);
            reader.Close();
            return localDeserialized;
        }

        UnityWebRequest unityWebRequest = UnityWebRequest.Get(Path.Combine(Application.streamingAssetsPath, path));
        unityWebRequest.SendWebRequest();

        while (!unityWebRequest.isDone) { }

        string file = unityWebRequest.downloadHandler.text;
        T deserialized = JsonUtility.FromJson<T>(file);
        unityWebRequest.Dispose();
        return deserialized;

        /*FileStream stream = new FileStream(path, FileMode.OpenOrCreate);
        StreamReader reader = new StreamReader(stream);
        string file = reader.ReadToEnd();
        T deserialized = JsonUtility.FromJson<T>(file);
        reader.Close();
        return deserialized;*/
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
