using DroppableItems;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class ItemEditorParams
{
    [SerializeReference]
    [SelectImplementation(typeof(IDroppable))]
    public IDroppable DroppableItem;
    [Range(1,100)]
    public int ChanceToSpawn;
    public Sprite ItemSprite;
}

[CreateAssetMenu]
public class ItemDataEditorObject : ScriptableObject
{
    [SerializeField]
    public ItemEditorParams ItemEditor;
}
