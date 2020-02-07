using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

//Developer: Antoshka
[RequireComponent(typeof(TextMeshProUGUI))]
public class ClickPowerTracker : MonoBehaviour
{
    [SerializeField]
    private FloatReference _clickPower;
    [SerializeField]
    private float _startingClickPower;
    [SerializeField]
    private IntReference _level;
    private TextMeshProUGUI _powerText;

    public void InitText()
    {
        _powerText = GetComponent<TextMeshProUGUI>();
        _powerText.text = "Click Power: " + _clickPower.Value;
    }

    public void UpdatePower()
    {
        _clickPower.Variable.SetValue(_startingClickPower * _level);
        _powerText.text = "Click Power: " + _clickPower.Value;
    }
}
