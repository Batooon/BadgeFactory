using DroppableItems;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class ItemEditorParams
{
    [SerializeReference]
    [SelectImplementation(typeof(ICollectable))]
    public ICollectable DroppableItem;
    [Range(1,100)]
    public int ChanceToSpawn;
    [Range(0.1f, 20f)]
    public float Lifetime;
    public Sprite ItemSprite;
}

[CreateAssetMenu]
public class ItemDataEditorObject : ScriptableObject
{
    [SerializeField]
    public ItemEditorParams ItemEditor;
}
