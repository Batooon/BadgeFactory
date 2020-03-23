using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

//Developer: Antoshka

public class CollectedCoinText : MonoBehaviour
{
    private float _speed;
    private float _lifetime;
    private bool _canMove;

    public void Init(float speed, float lifetime)
    {
        _speed = speed;
        _lifetime = lifetime;
        _canMove = true;
        Destroy(gameObject, lifetime);
    }

    private void Update()
    {
        if (_canMove)
            transform.Translate(Vector2.up * _speed * Time.deltaTime);
    }
}
