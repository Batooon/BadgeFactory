using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

//Developer: Antoshka

public class CollectedCoinText : MonoBehaviour
{
    public float Speed;

    [SerializeField]
    private TextMeshProUGUI _collectedAmount;

    private bool _canMove;

    private Vector2 _randomFinishPosition;

    public void StartMotion(float amount)
    {
        _collectedAmount.text = amount.ConvertValue();
        _canMove = true;
        Destroy(gameObject, 0.3f);
    }

    private void Update()
    {
        if (!_canMove)
            return;
        transform.Translate(Vector2.up * Speed * Time.deltaTime);
    }
}
