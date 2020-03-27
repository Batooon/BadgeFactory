using UnityEngine;
using System.Collections.Generic;

[System.Serializable]
public class AutomationData
{
    public Sprite Icon;
    public string Name;
    public int StartingLevel;
    public int StartingCost;
    public int StartingDamagePerSecond;
}

[CreateAssetMenu]
[System.Serializable]
public class AutomationVariables : ScriptableObject
{
    [SerializeField]
    public List<AutomationData> Automations;
}

[System.Serializable]
public class Automation
{
    [SerializeField]
    public List<AutomationData> Automations;
}
