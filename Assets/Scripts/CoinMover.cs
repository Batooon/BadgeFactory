using UnityEngine;
using DroppableItems;
using UnityEngine.Events;
using DG.Tweening;

public class CoinMover : MonoBehaviour, IItemTweener
{
    [SerializeField] private UnityEvent _finished;
    [SerializeField] private AnimationCurve _easeCurve;
    [SerializeField] private float _duration;
    [SerializeField] private float _groundYCoordinate;
    [SerializeField] private float _groundXMaxCoordinate;
    [SerializeField] private float _groundXMinCoordinate;

    public void StartMotion()
    {
        Sequence coinMove = DOTween.Sequence();
        coinMove.Append(transform
                .DOMoveY(_groundYCoordinate, _duration)
                .SetEase(_easeCurve));

        coinMove.Join(transform
                .DOMoveX(Random.Range(_groundXMinCoordinate, _groundXMaxCoordinate), _duration));

        coinMove.OnComplete(_finished.Invoke);
    }

    public void Init() { }
}
