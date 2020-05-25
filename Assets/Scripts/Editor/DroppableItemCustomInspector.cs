using UnityEditor;
using UnityEngine;
using DroppableItems;

[CustomEditor(typeof(DroppableObject))]
public class DroppableItemCustomInspector : Editor
{
    public override void OnInspectorGUI()
    {
        if(GUILayout.Button("Open Droppable items Editor"))
        {
            DroppableItemEditor.Open((DroppableObject)target);
        }
    }
}
