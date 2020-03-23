using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Automations : MonoBehaviour
{
    public List<GameObject> AutomationsObjects = new List<GameObject>();

    private int _visibleAutomationsCount;

    public void UnlockNextAutomation()
    {
        _visibleAutomationsCount += 1;
        AutomationsObjects[_visibleAutomationsCount - 1].SetActive(true);
    }

    private void Awake()
    {
        _visibleAutomationsCount = PlayerPrefs.GetInt("VisibleAutomations", 2);

        for (int i = 0; i < _visibleAutomationsCount - 1; i++) 
        {
            AutomationsObjects[i].SetActive(true);
        }
        GameEvents.current.OpenNewAutomation += UnlockNextAutomation;
    }

    private void OnApplicationPause(bool pause)
    {
        if (pause)
        {
            PlayerPrefs.SetInt("VisibleAutomations", _visibleAutomationsCount);
            GameEvents.current.OpenNewAutomation -= UnlockNextAutomation;
        }
    }

    private void OnApplicationQuit()
    {
        PlayerPrefs.SetInt("VisibleAutomations", _visibleAutomationsCount);
        GameEvents.current.OpenNewAutomation -= UnlockNextAutomation;
    }
}
