using UnityEngine;
using UnityEditor;

//Developer: Antoshka

public class CustomTools : EditorWindow
{
    [MenuItem("Window/Custom Tools")]
    public static void ResetPlayerPrefs()
    {
        GetWindow<CustomTools>("PlayerPrefs Resetter");
    }

    private void OnGUI()
    {
        if(GUILayout.Button("Reset PlayerPrefs"))
        {
            PlayerPrefs.DeleteAll();
            Debug.Log("PlayerPrefs have been resetted");
        }
    }
}
