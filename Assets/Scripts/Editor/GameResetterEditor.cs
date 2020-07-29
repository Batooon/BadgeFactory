using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(GameResetter))]
public class GameResetterEditor : Editor
{
    public override void OnInspectorGUI()
    {
        if(GUILayout.Button("Reset Game"))
        {
            GameResetter resetter = (GameResetter)target;
            resetter.ResetGame();
        }

        base.OnInspectorGUI();
    }
}
