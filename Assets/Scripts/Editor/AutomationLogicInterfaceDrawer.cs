using System;
using System.Linq;
using UnityEditor;
using AutomationImplementation;
using UnityEngine;

/*[CustomEditor(typeof(AutomationLogic))]
public class AutomationLogicInterfaceDrawer : Editor
{
    private Type[] _implementations;
    private int _implementationTypeIndex;

    public override void OnInspectorGUI()
    {
        AutomationLogic testBehaviour = target as AutomationLogic;
        //specify type
        if (testBehaviour == null)
        {
            return;
        }

        if (_implementations == null || GUILayout.Button("Refresh implementations"))
        {
            //this is probably the most imporant part:
            //find all implementations of INode using System.Reflection.Module
            _implementations = GetImplementations<IAutomation>().Where(impl => !impl.IsSubclassOf(typeof(UnityEngine.Object))).ToArray();
        }

        EditorGUILayout.LabelField($"Found {_implementations.Count()} implementations");

        //select implementation from editor pop
        _implementationTypeIndex = EditorGUILayout.Popup(new GUIContent("Implementation"),
            _implementationTypeIndex, _implementations.Select(impl => impl.FullName).ToArray());

        if (GUILayout.Button("Create instance"))
        {
            //set new value
            testBehaviour.Automation = (IAutomation)Activator.CreateInstance(_implementations[_implementationTypeIndex]);
            Debug.Log(testBehaviour.Automation);
        }

        base.OnInspectorGUI();
    }

    private static Type[] GetImplementations<T>()
    {
        var types = AppDomain.CurrentDomain.GetAssemblies().SelectMany(assembly => assembly.GetTypes());

        var interfaceType = typeof(T);
        return types.Where(p => interfaceType.IsAssignableFrom(p) && !p.IsAbstract).ToArray();
    }
}*/
