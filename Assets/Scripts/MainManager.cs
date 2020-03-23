using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;
using System;

[Serializable]
public struct GameData
{

}

public class MainManager : MonoBehaviour
{
    [ContextMenu("Load Badge")]
    [Inject]
    private void LoadBadge()
    {
        new BadgeFactory().Load();
    }
}