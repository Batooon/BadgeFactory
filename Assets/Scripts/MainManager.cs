using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public struct GameData
{

}

public class MainManager : MonoBehaviour
{
    [ContextMenu("Load Badge")]
    private void LoadBadge()
    {
        new BadgeFactory().Load();
    }
}