using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.IO;
using System;

public class AutomationEditor : ExtendedEditorWindow
{
    private int _currentArraylength = 1;
    private AutomationsData automations;

    public static void Open(AutomationEditorObject automation)
    {
        AutomationEditor window = GetWindow<AutomationEditor>("Automations Editor");
        window.serializedObject = new SerializedObject(automation);
        window.automations = new AutomationsData();
        window.automations.Automations = automation.Automations;
    }

    private void OnGUI()
    {
        currentProperty = serializedObject.FindProperty("Automations");
        EditorGUILayout.BeginHorizontal();

        EditorGUILayout.BeginVertical("box", GUILayout.MaxWidth(150), GUILayout.ExpandHeight(true));

        _currentArraylength = EditorGUILayout.IntField("Array length", _currentArraylength);

        if (GUILayout.Button("Apply"))
        {
            currentProperty.arraySize = _currentArraylength;
            automations.Automations.Capacity = _currentArraylength;
            Apply();
        }

        DrawSidebar(currentProperty);

        if (GUILayout.Button("Save to JSON"))
        {
            try
            {
                FileOperations.Serialize(automations, Path.Combine(Application.persistentDataPath, "Automations.json"));
            }
            catch(Exception e)
            {
                Debug.LogError(e);
                return;
            }
            Debug.Log("<b><color=green>Automations serialized successfully</color></b>");
        }

        EditorGUILayout.EndVertical();

        EditorGUILayout.BeginVertical("box", GUILayout.ExpandHeight(true));
        if (_selectedProperty != null)
        {
            EditorGUILayout.PropertyField(_selectedProperty, true);
            DrawSelectedPropertiesPanel();
        }
        else
        {
            EditorGUILayout.LabelField("Select an item from the list");
        }
        EditorGUILayout.EndVertical();
        EditorGUILayout.EndHorizontal();

        Apply();
    }

    private void DrawSelectedPropertiesPanel()
    {
        currentProperty = _selectedProperty;
    }
}
