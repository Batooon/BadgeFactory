﻿using UnityEngine;
using System.Collections.Generic;
using System;

[Serializable]
public class AutomationData
{
    public string Name;

    public int StartingLevel;
    public int StartingCost;
    public int StartingDps;
}

[Serializable]
public class AutomationEditorParams
{
    public Sprite Icon;
    public AutomationData automationData;
    [SerializeReference]
    [SelectImplementation(typeof(IAutomation))]
    public IAutomation Automation;
}

[CreateAssetMenu]
public class AutomationEditorObject : ScriptableObject
{
    [SerializeField]
    public List<AutomationEditorParams> Automations;
}

public class SelectImplementationAttribute : PropertyAttribute
{
    public Type FieldType;
    public SelectImplementationAttribute(Type fieldType)
    {
        FieldType = fieldType;
    }
}