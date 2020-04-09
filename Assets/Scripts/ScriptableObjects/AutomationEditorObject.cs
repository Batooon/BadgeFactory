using UnityEngine;
using System.Collections.Generic;

public enum AutomationTypes
{
    UsualAutomation,
    ClickPower
}

[System.Serializable]
public class AutomationCreationParams
{
    [System.NonSerialized]
    public Sprite Icon;
    public string Name;
    public int StartingLevel;
    public int StartingCost;
    public int StartingDamagePerSecond;
    public AutomationTypes AutomationType;
}

[CreateAssetMenu]
[System.Serializable]
public class AutomationEditorObject : ScriptableObject
{
    [SerializeField]
    public List<AutomationCreationParams> Automations;
}

[System.Serializable]
public class AutomationsData
{
    [SerializeField]
    public List<AutomationCreationParams> Automations;
}
