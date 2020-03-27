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
        AutomationVariables automation = EditorUtility.InstanceIDToObject(instanceId) as AutomationVariables;
        if (automation != null)
        {
            AutomationEditor.Open(automation);
            return true;
        }
        return false;
    }
}

[CustomEditor(typeof(AutomationVariables))]
public class AutomationCustomInspector : Editor
{
    public override void OnInspectorGUI()
    {
        if(GUILayout.Button("Open Automation Editor"))
        {
            AutomationEditor.Open((AutomationVariables)target);
        }
    }
}
