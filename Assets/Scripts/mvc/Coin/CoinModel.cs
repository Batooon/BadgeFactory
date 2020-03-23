using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinModel
{
    public int Cost;
    public float Lifetime;
    public float TimeToLive;
    public float CollectedTextSpeed = 2f;
    public float CollectedTextLifetime = .3f;

    public CoinModel(float timeToLive, int coinCost)
    {
        TimeToLive = timeToLive;
        Cost = coinCost;
        Lifetime = 0;
        CollectedTextSpeed = 2f;
        CollectedTextLifetime = .3f;
    }
}
