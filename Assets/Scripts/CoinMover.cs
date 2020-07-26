using UnityEngine;
using DroppableItems;
using System;
using UnityEngine.Events;

public class CoinMover : MonoBehaviour, IItemTweener
{
    [SerializeField] private UnityEvent _finished;
    [SerializeField] private LeanTweenType _easeType;
    [SerializeField] private AnimationCurve _animationCurve;
    [SerializeField] private float _duration;
    [SerializeField] private float _groundYCoordinate;
    [SerializeField] private float _groundXMaxCoordinate;
    [SerializeField] private float _groundXMinCoordinate;

    public void StartMotion()
    {
        if (_easeType == LeanTweenType.animationCurve)
        {
            LeanTween.moveY(gameObject, _groundYCoordinate, _duration).setEase(_animationCurve).setOnComplete(_finished.Invoke);
            LeanTween.moveX(gameObject, UnityEngine.Random.Range(_groundXMinCoordinate, _groundXMaxCoordinate), _duration * 0.5f);
        }
        else
        {
            LeanTween.move(gameObject,
            new Vector2(UnityEngine.Random.Range(_groundXMinCoordinate, _groundXMaxCoordinate),
            _groundYCoordinate),
            _duration)
            .setEase(_easeType)
            .setOnComplete(_finished.Invoke);
        }
    }

    public void Init() { }
}
