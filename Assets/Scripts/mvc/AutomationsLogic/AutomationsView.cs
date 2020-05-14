using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutomationsView : MonoBehaviour
{
    public GameObject AutomationPrefab;
    [HideInInspector]
    public List<GameObject> AutomationsObjects = new List<GameObject>();

    public void InitAutomations(List<AutomationEditorParams> automationsData)
    {
        for (int i = 0; i < automationsData.Count; i++)
        {
            GameObject automation = Instantiate(AutomationPrefab, transform);
            AutomationsObjects.Add(automation);
        }
    }
}
