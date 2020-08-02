using UnityEditor;
using UnityEngine;

public class ExtendedPropertyDrawer : PropertyDrawer
{
    protected void DrawProperties(SerializedProperty prop, bool drawChildren, Rect position)
    {
        string lastPropPath = string.Empty;
        foreach (SerializedProperty p in prop)
        {
            if (p.isArray && p.propertyType == SerializedPropertyType.Generic)
            {
                p.isExpanded = EditorGUI.Foldout(new Rect(position.x,position.y,50,EditorGUIUtility.singleLineHeight), p.isExpanded, p.displayName);

                if (p.isExpanded)
                {
                    DrawProperties(p, drawChildren, new Rect(position.x,position.y+EditorGUIUtility.singleLineHeight,position.size.x/2,EditorGUIUtility.singleLineHeight));
                }
            }
            else
            {
                if (string.IsNullOrEmpty(lastPropPath) == false && p.propertyPath.Contains(lastPropPath))
                    continue;
                lastPropPath = p.propertyPath;
                EditorGUI.PropertyField(position, p);
            }
        }
    }
}
