﻿using Automation;
using Automations;
using UnityEditor;
using UnityEngine;

public class AutomationEditor : ExtendedEditorWindow
{
    private int _currentArraylength = 1;

    private GameObject AutomationPrefab;
    private Transform AutomationParent;

    //private AutomationEditorObject automationEditor;

    public static void Open(AutomationEditorObject automation)
    {
        AutomationEditor window = GetWindow<AutomationEditor>("Automations Editor");
        window.serializedObject = new SerializedObject(automation);
        //window.automationEditor = automation;
    }

    private void OnInspectorUpdate()
    {
        Repaint();
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
            Apply();
        }

        DrawSidebar(currentProperty);

        AutomationPrefab = EditorGUILayout.ObjectField("Select Automation object",
            AutomationPrefab,
            typeof(GameObject),
            true) as GameObject;
        AutomationParent = EditorGUILayout.ObjectField("Select Automation parent object",
            AutomationParent,
            typeof(Transform),
            true) as Transform;

        if (AutomationParent != null && AutomationPrefab != null && GUILayout.Button("Instantiate Automations"))
        {
            var automationEditor = serializedObject.targetObject as AutomationEditorObject;
            IAutomationDatabase automationDatabase = AutomationDatabse.GetAutomationDatabase();

            for (int i = 0; i < _currentArraylength; i++)
            {
                AutomationsPresentation automationsPresentation=AutomationParent.GetComponent<AutomationsPresentation>();
                AutomationEditorParams automationParams = automationEditor.Automations[i];

                GameObject automation = Instantiate(AutomationPrefab, AutomationParent);
                AutomationInitializer automationInitializer = automation.GetComponent<AutomationInitializer>();

                automationInitializer.InitializeAutomation(automationParams.Automation,
                    automationParams.Name,
                    automationParams.Icon);

                automationsPresentation.AddAutomation(automation);

                CurrentPlayerAutomationData automationData = new CurrentPlayerAutomationData();
                automationData.StartingCost = automationEditor.Automations[i].StartingCost;
                automationData.StartingDamage = automationEditor.Automations[i].StartingDps;
                automationData.Cost = automationData.StartingCost;
                automationData.DamagePerSecond = automationData.StartingDamage;

                automationDatabase.SaveAutomationData(automationData, i);
            }
            automationDatabase.Serialize();
        }

        EditorGUILayout.EndVertical();

        EditorGUILayout.BeginVertical("box", GUILayout.ExpandHeight(true));

        if (_selectedProperty != null)
        {
            DrawProperties(_selectedProperty, true);
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
