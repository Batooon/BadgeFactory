﻿using UnityEngine;
using UnityEditor;
using System.Linq;


/// <summary>
/// Drawer for the RequireInterface attribute.
/// </summary>
[CustomPropertyDrawer(typeof(RequireInterfaceAttribute))]
public class RequireInterfaceDrawer : PropertyDrawer
{
    ///<summary>
    ///Overrides GUI drawing for the attribute.
    ///</summary>
    ///<param name="position">Position.</param>
    ///<param name="property">Property.</param>
    ///<param name="label">Label.</param>
    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        if (property.propertyType == SerializedPropertyType.ObjectReference)
        {
            //Get attribute parameters
            var requiredAttribute = attribute as RequireInterfaceAttribute;
            //Begin drawing property field
            EditorGUI.BeginProperty(position, label, property);
            // Draw property field.
            property.objectReferenceValue = EditorGUI.ObjectField(position, label, property.objectReferenceValue, requiredAttribute.RequiredType, true);
            // Finish drawing property field.
            EditorGUI.EndProperty();
        }
        else
        {
            // If field is not reference, show error message.
            // Save previous color and change GUI to red.
            var previousColor = GUI.color;
            GUI.color = Color.red;
            // Display label with error message.
            EditorGUI.LabelField(position, label, new GUIContent("Property is not a reference type"));
            // Revert color change.
            GUI.color = previousColor;
        }
    }
}
