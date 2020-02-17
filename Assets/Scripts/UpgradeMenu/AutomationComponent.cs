using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

//Developer: Antoshka

public class AutomationComponent : MonoBehaviour
{
    [SerializeField]
    private Automation _component;

    public void Upgrade()
    {
        _component.UpdateDps();
    }
}
