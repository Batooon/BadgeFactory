using System;
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

            for (int i = 0; i < _currentArraylength; i++)
            {
                GameObject automation = Instantiate(AutomationPrefab, AutomationParent);
                IAutomationInitializer automationsInitializer = automation.GetComponent<IAutomationInitializer>();
                automationsInitializer.Initialize(automationEditor.Automations[i].automationData,
                    automationEditor.Automations[i].Icon,
                    automationEditor.Automations[i].Automation);
            }
        }

        /*if (GUILayout.Button("Save to JSON and save sprites to Asset Bundle"))
        {
            //List<DefaultAutomationData> automationsData = new List<DefaultAutomationData>();
            SerializedDefaultAutomationData automationsData = new SerializedDefaultAutomationData();
            foreach (var item in automations.Automations)
            {
                DefaultAutomationData automationData = new DefaultAutomationData();
                automationData.Name = item.Name;
                automationData.StartingCost = item.StartingCost;
                automationData.StartingDps = item.StartingDamagePerSecond;
                automationData.StartingLevel = item.StartingLevel;
                //automationData.AutomationType = item.Automation;

                if (item.Automation is ClickAutomation)
                {
                    automationData.AutomationType = AutomationTypes.ClickPower;
                }
                else if(item.Automation is UsualAutomation)
                {
                    automationData.AutomationType = AutomationTypes.UsualAutomation;
                }
                else
                {
                    Debug.LogError("Такой тип автомации отсутствует. Добавь его сюда, или поправь тип автомации");
                }

                automationsData.AutomationsData.Add(automationData);
                //TODO: сделать сохранение картинок в Asset Bundles
                //TODO: показывать работающие автомации в игре
                AssetDatabase.MoveAsset($"{_spritesFolderPath}/{item.Icon.name}", _assetBundleFolder);
            }

            CreateAssetBundles.BuildAllAssetBundles();

            try
            {
                FileOperations.Serialize(automationsData, Path.Combine(Application.persistentDataPath, "AutomationsDefaultData.json"));
            }
            catch (Exception e)
            {
                Debug.LogError(e);
                return;
            }
            Debug.Log("<b><color=green>Automations serialized successfully</color></b>");
        }*/

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
