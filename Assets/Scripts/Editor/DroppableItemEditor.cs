using System.Collections;
using System.Collections.Generic;
using UnityEditor;

public class DroppableItemEditor : ExtendedEditorWindow
{
    public static void Open(ItemDataEditorObject itemData)
    {
        DroppableItemEditor window=GetWindow<DroppableItemEditor>("Droppable Item editor");
        window.serializedObject=new SerializedObject(itemData);
    }
}
