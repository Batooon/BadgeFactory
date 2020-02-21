using System;
using UnityEngine.EventSystems;
using UnityEngine;
using UnityEngine.UI;

public class Coin : MonoBehaviour, IPointerEnterHandler
{
    public event Action<Coin> CoinDestroyed;
    public event Action<Coin> CoinCollected;

    public GameObject CoinObj;
    [HideInInspector]
    public float Cost;
    public float _timeToLive;
    private float _livingTime;

    private void Update()
    {
        if (_livingTime >= _timeToLive)
        {
            Destroy(gameObject);
            CoinDestroyed?.Invoke(this);
            return;
        }
        _livingTime += Time.deltaTime;
    }

    private void CollectCoin()
    {
        CoinCollected?.Invoke(this);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        CollectCoin();
    }
}
