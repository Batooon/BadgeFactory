using UnityEngine;
using UnityEditor;

[CustomPropertyDrawer(typeof(FloatReference))]
[CustomPropertyDrawer(typeof(IntReference))]
public class FloatReferenceDrawer : PropertyDrawer
{
    private readonly string[] _popupOptions = { "Use Constant", "Use Variable" };

    private GUIStyle _popupStyle;

    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        if (_popupStyle == null)
        {
            _popupStyle = new GUIStyle(GUI.skin.GetStyle("PaneOptions"));
            _popupStyle.imagePosition = ImagePosition.ImageOnly;
        }

        label = EditorGUI.BeginProperty(position, label, property);
        position = EditorGUI.PrefixLabel(position, label);

        EditorGUI.BeginChangeCheck();

        SerializedProperty useConstant = property.FindPropertyRelative("UseConstant");
        SerializedProperty constantValue = property.FindPropertyRelative("ConstantValue");
        SerializedProperty variable = property.FindPropertyRelative("Variable");

        Rect buttonRect = new Rect(position);
        buttonRect.yMin += _popupStyle.margin.top;
        buttonRect.width = _popupStyle.fixedWidth + _popupStyle.margin.right;
        position.xMin = buttonRect.xMax;

        int indent = EditorGUI.indentLevel;
        EditorGUI.indentLevel = 0;

        int result = EditorGUI.Popup(buttonRect, useConstant.boolValue ? 0 : 1, _popupOptions, _popupStyle);

        useConstant.boolValue = result == 0;

        EditorGUI.PropertyField(position,
            useConstant.boolValue ? constantValue : variable,
            GUIContent.none);

        if (EditorGUI.EndChangeCheck())
            property.serializedObject.ApplyModifiedProperties();

        EditorGUI.indentLevel = indent;
        EditorGUI.EndProperty();
    }
}
