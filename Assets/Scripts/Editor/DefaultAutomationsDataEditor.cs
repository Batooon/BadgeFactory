using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(DefaultAutomationsData))]
public class DefaultAutomationsDataEditor : Editor
{
    private DefaultAutomationsData _automationsData;

    private void Awake()
    {
        _automationsData = (DefaultAutomationsData)target;
    }

    public override void OnInspectorGUI()
    {
        GUILayout.BeginHorizontal();

        if (GUILayout.Button("Remove all"))
            _automationsData.ClearAutomations();
        if (GUILayout.Button("Remove this"))
            _automationsData.RemoveAutomation();
        if (GUILayout.Button("Add"))
            _automationsData.AddAutomation();
        if (GUILayout.Button("<<"))
            _automationsData.GetPrevious();
        if (GUILayout.Button(">>"))
            _automationsData.GetNext();
        GUILayout.TextField(_automationsData.AutomationIndex.ToString());

        GUILayout.EndHorizontal();

        base.OnInspectorGUI();
    }
}
