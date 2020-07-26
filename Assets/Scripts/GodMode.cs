using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[ExecuteInEditMode]
public class GodMode : MonoBehaviour
{
    public void AddSomeGold()
    {
        Data playerData = PlayerDataAccess.GetPlayerDatabase().GetPlayerData();
        playerData.GoldAmount += 100000;
    }
}

[CustomEditor(typeof(GodMode))]
public class GodModeEditor : Editor
{
    public override void OnInspectorGUI()
    {
        if(GUILayout.Button("Add some gold"))
        {
            GodMode godMode = (GodMode)target;
            godMode.AddSomeGold();
        }
    }
}
