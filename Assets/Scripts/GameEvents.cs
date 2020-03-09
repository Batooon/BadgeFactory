using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

//Developer: Antoshka

public class GameEvents : MonoBehaviour
{
    public static GameEvents current;

    public event Action OnQuit;
    public event Action AddAdditionalGold;

    public event Action<EnemyDataVariable> BadgeCreated;
    public void GainAdditionalGold()
    {
        AddAdditionalGold.Invoke();
    }

    public void BadgeCreatedCallback(EnemyDataVariable badge)
    {
        BadgeCreated.Invoke(badge);
    }

    private void Awake()
    {
        current = this;
    }
}
