using UnityEditor;
using UnityEngine;

/*[CustomPropertyDrawer(typeof(PlayerData))]
public class PlayerDataCustomProperty : ExtendedPropertyDrawer
{   
    private string _fileName = "";
    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        EditorGUI.BeginProperty(position, label, property);

        Rect pos = EditorGUI.PrefixLabel(position, GUIUtility.GetControlID(FocusType.Passive), label);
        EditorGUIUtility.labelWidth = 128;

        int indent = EditorGUI.indentLevel;
        EditorGUI.indentLevel = 0;

        DrawProperties(property, false, pos);

        EditorGUI.indentLevel = indent;

        EditorGUI.EndProperty();
    }

    public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
    {
        int lineCount = 2;
        return EditorGUIUtility.singleLineHeight * lineCount + EditorGUIUtility.standardVerticalSpacing * (lineCount - 1);
    }

    private void SaveToJson(SerializedProperty property)
    {
        if (string.IsNullOrEmpty(_fileName))
        {
            Debug.LogError("Provide the file name before saving!");
            return;
        }
        FileOperations.Serialize(property, _fileName);
    }
}
*/