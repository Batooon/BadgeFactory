using UnityEngine;
using UnityEngine.Events;
using System;
using UnityEngine.UI;
using UnityEngine.Advertisements;
using System.Collections.Generic;
using TMPro;

//Developer: Antoshka

public class ClickButton : MonoBehaviour
{
    public UnityEvent Init;

    private void Start()
    {
        Init.Invoke();
    }
}
