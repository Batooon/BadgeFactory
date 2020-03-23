using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BadgeFactory
{
    public BadgeController Controller
    {
        get;
        private set;
    }

    public BadgeModel Model
    {
        get;
        private set;
    }

    public BadgeView View
    {
        get;
        private set;
    }

    public void Load()
    {
        GameObject prefab = Resources.Load<GameObject>("Badge");
        GameObject instance = GameObject.Instantiate<GameObject>(prefab);
    }
}
