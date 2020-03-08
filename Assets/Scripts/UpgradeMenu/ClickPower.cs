using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

//Developer: Antoshka

public class ClickPower : AutomationBase
{
    public override void Init()
    {
        Dps = PlayerPrefs.GetFloat($"{Name}_Dps", 1);
        Cost = PlayerPrefs.GetFloat($"{Name}_Cost", _startingCost);
        _level = PlayerPrefs.GetInt($"{Name}_Level", 1);
        if (_level == 0)
        {
            _dpsText.text = "0";
            _priceText.text = $"HIRE {_startingCost}";
        }
        else
        {
            _dpsText.text = Dps.ConvertValue();
            _priceText.text = Cost.ConvertValue();
        }
        AfterInit();
    }
}
