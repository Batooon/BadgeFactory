using UnityEngine;
using UnityEngine.Events;
using System;
using UnityEngine.UI;
using UnityEngine.Advertisements;
using System.Collections.Generic;

//Developer: Antoshka

public class ClickButton : MonoBehaviour
{
    private int _clicks;

    private void Start()
    {
        _clicks = 0;
    }

    public void ButtonClicked()
    {
        _clicks++;
    }
}
