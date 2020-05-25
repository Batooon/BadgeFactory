using UnityEditor;
using UnityEngine;
using UnityEngine.UI;
using DroppableItems;

public class DroppableItemEditor : ExtendedEditorWindow
{
    private GameObject _baseItem;
    private string assetPath;

    public static void Open(DroppableObject itemData)
    {
        DroppableItemEditor window = GetWindow<DroppableItemEditor>("Droppable Item editor");
        window.serializedObject = new SerializedObject(itemData);
        /*window.currentProperty=window.serializedObject.GetIterator();
        window.currentProperty.Next(true);*/
    }

    private void OnGUI()
    {
        
        //currentProperty = serializedObject.FindProperty("ItemEditor");

        _baseItem = EditorGUILayout.ObjectField("Select base prefab",
                                                _baseItem,
                                                typeof(GameObject),
                                                true) as GameObject;

        assetPath = EditorGUILayout.TextField("Asset path", assetPath);

        if (_baseItem != null)
        {
            //currentProperty.Next(true);

            DrawProperties(currentProperty, true);


            /*if (GUILayout.Button("Create prefab variant"))
            {
                GameObject instance = (GameObject)PrefabUtility.InstantiatePrefab(_baseItem);
                DroppableItemInitializer initializer = instance.GetComponent<DroppableItemInitializer>();
                var itemData = serializedObject.targetObject as ItemDataEditorObject;
                initializer.Initialize(itemData.ItemEditor);
                GameObject variant = PrefabUtility.SaveAsPrefabAsset(instance, assetPath);
                GameObject.DestroyImmediate(instance);
            }*/
        }

        //Apply();

        //currentProperty = serializedObject.FindProperty();

        /*EditorGUILayout.BeginHorizontal();

        EditorGUILayout.BeginVertical("box", GUILayout.MaxWidth(150), GUILayout.ExpandHeight(true));*/

        //_currentArraylength = EditorGUILayout.IntField("Array length", _currentArraylength);

        /*if (GUILayout.Button("Apply"))
        {
            currentProperty.arraySize = _currentArraylength;
            Apply();
        }*/

        //DrawSidebar(currentProperty);

        /*AutomationPrefab = EditorGUILayout.ObjectField("Select Automation object",
            AutomationPrefab,
            typeof(GameObject),
            true) as GameObject;
        AutomationParent = EditorGUILayout.ObjectField("Select Automation parent object",
            AutomationParent,
            typeof(Transform),
            true) as Transform;*/

        /*if (AutomationParent != null && AutomationPrefab != null && GUILayout.Button("Instantiate Automations"))
        {
            var automationEditor = serializedObject.targetObject as AutomationEditorObject;
            IAutomationDatabase automationDatabase = AutomationDatabse.GetAutomaitonDatabase();

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

                automationDatabase.SaveAutomationData(automationData, i);
            }
            automationDatabase.Serialize();
        }*/

        /*EditorGUILayout.EndVertical();

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

        Apply();*/
    }

    private void DrawSelectedPropertiesPanel()
    {
        currentProperty = _selectedProperty;
    }

    private void OnInspectorUpdate()
    {
        Repaint();
    }
}
