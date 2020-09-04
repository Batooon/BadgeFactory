using DroppableItems;
using UnityEngine;
using System;

public class UsualItemMover : MonoBehaviour, IItemTweener
{
    [SerializeField]
    private float _moveTime;
    private Vector2 _destination;

    public Action Finished { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

    public void Init()
    {
        throw new NotImplementedException();
    }

    public void SetDestination(Vector2 destination)
    {
        _destination = destination;
    }

    public void StartMotion()
    {
        LeanTween.move(gameObject,
            new Vector2(UnityEngine.Random.Range(-1.5f, 1.5f),
            UnityEngine.Random.Range(-1.5f, 1.5f)),
            _moveTime);
    }
}
