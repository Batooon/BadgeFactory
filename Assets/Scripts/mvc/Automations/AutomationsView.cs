using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutomationsView : MonoBehaviour
{
    public GameObject AutomationPrefab;
    public List<GameObject> AutomationsObjects = new List<GameObject>();
    [SerializeField]
    private List<Automation> Automations = new List<Automation>();

    public void InitAutomations(AutomationData[] automationsData)
    {
        for (int i = 0; i < automationsData.Length; i++)
        {
            GameObject automation = Instantiate(AutomationPrefab, transform);
        }
    }
}
