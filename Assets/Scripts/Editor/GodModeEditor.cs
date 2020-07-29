using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(GodMode))]
public class GodModeEditor : Editor
{
    public override void OnInspectorGUI()
    {
        if(GUILayout.Button("Add some gold"))
        {
            GodMode godMode = (GodMode)target;
            godMode.AddSomeGold();
        }
    }
}
