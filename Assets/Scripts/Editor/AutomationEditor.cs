using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.IO;
using System;

public class AutomationEditor : ExtendedEditorWindow
{
    private int _currentArraylength = 1;
    private Automation automations;

    public static void Open(AutomationVariables automation)
    {
        AutomationEditor window = GetWindow<AutomationEditor>("Automations Editor");
        window.serializedObject = new SerializedObject(automation);
        window.automations = new Automation();
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
                XmlOperation.Serialize(automations, Path.Combine(Application.persistentDataPath, "Automations.json"));
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
        bool clickPower = false;
        bool automation = false;

        currentProperty = _selectedProperty;

        EditorGUILayout.BeginHorizontal("box");

        if(GUILayout.Button("Click Power", EditorStyles.toolbarButton))
        {
            clickPower = true;
            automation = false;
        }
        if (GUILayout.Button("Automation", EditorStyles.toolbarButton))
        {
            clickPower = false;
            automation = true;
        }

        EditorGUILayout.EndHorizontal();

        if (clickPower)
        {
            EditorGUILayout.BeginVertical("box");

            EditorGUILayout.EndVertical();
        }
    }
}
