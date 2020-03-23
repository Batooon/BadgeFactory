using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public struct DataVariables
{
    #region BadgeData
    public float BadgeHp;
    public float GetCurrentHp() => BadgeHp;
    public void SetHp(float hp) => BadgeHp = hp;

    public float MaxBadgeHp;
    public float GetMaxHp() => MaxBadgeHp;
    #endregion
    #region UpgradesData
    public int ClickPower;
    public int AutomationsPower;
    #endregion
}
