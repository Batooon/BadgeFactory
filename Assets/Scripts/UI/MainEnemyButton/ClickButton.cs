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
    [SerializeField]
    private UnityEvent Init;

    [SerializeField]
    private FloatReference _clickPower;

    [SerializeField]
    private FloatReference _score;

    //private int _clicks;

    private void Start()
    {
        //_clicks = 0;
        //_clickPower.Variable.SetValue(1f);
        Init.Invoke();
    }

    public void ButtonClicked()
    {
        _score.Variable.Value += _clickPower.Variable.Value;
    }
}
