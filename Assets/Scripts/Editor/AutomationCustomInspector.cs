using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEditor.Callbacks;

public class AssetHandler
{
    [OnOpenAsset()]
    public static bool OpenEditor(int instanceId,int line)
    {
        Automation automation = EditorUtility.InstanceIDToObject(instanceId) as Automation;
        if (automation != null)
        {
            AutomationEditor.Open(automation);
            return true;
        }
        return false;
    }
}

[CustomEditor(typeof(Automation))]
public class AutomationCustomInspector : Editor
{
    public override void OnInspectorGUI()
    {
        if(GUILayout.Button("Open Automation Editor"))
        {
            AutomationEditor.Open((Automation)target);
        }
    }
}
