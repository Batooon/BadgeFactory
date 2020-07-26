using UnityEngine;
using DroppableItems;
using UnityEngine.Events;
using System;

[RequireComponent(typeof(Rigidbody))]
public class ExplosionCoinPuncher : MonoBehaviour, IItemTweener
{
    [SerializeField] private UnityEvent _movingEnded;
    [SerializeField] private float _upwardsModifier = 5f;
    [SerializeField] private float _explosionForce = 3;
    [SerializeField] private float _explosionRadius = 2;

    private Rigidbody _rigidbody;
    private Vector3 _explosionPosition;
    private Transform _transform;

    public Action Finished { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

    public void Init()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _transform = GetComponent<Transform>();
    }

    private void Update()
    {
        if (_rigidbody.IsSleeping())
        {
            _movingEnded.Invoke();
        }
    }

    public void StartMotion()
    {
        _explosionPosition = _transform.localPosition + new Vector3(UnityEngine.Random.Range(-2, 3), UnityEngine.Random.Range(-1, 3));
        _rigidbody.AddExplosionForce(_explosionForce, _explosionPosition, _explosionRadius, _upwardsModifier, ForceMode.Impulse);
    }
}
