using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(ItemDataEditorObject))]
public class DroppableItemCustomInspector : Editor
{
    public override void OnInspectorGUI()
    {
        if(GUILayout.Button("Open Droppable items Editor"))
        {
            DroppableItemEditor.Open((ItemDataEditorObject)target);
        }
    }
}
