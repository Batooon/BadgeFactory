using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public delegate void CoinCollectedCallback();
public delegate void CollectedCoinTextSpawned(GameObject text);

[RequireComponent(typeof(CoinView))]
public class CoinController : MonoBehaviour
{
    public event Action<int> CoinCollected;

    private CoinModel _coinModel;
    private CoinView _coinView;

    public void Init(int coinCost, float timeToLive)
    {
        _coinModel = new CoinModel(timeToLive, coinCost);
        _coinView = GetComponent<CoinView>();
        _coinView.CoinCollected = OnCoinCollected;
        _coinView.TextSpawnedCallback = OnTextSpawned;

        _coinView.StartMotion();
        Destroy(gameObject, timeToLive);
    }

    private void OnCoinCollected()
    {
        CoinCollected?.Invoke(_coinModel.Cost);
        _coinView.SpawnCollectedText(_coinModel.Cost);
        Destroy(gameObject);
    }

    private void OnTextSpawned(GameObject text)
    {
        _coinView.StartTextMotion(text, _coinModel.CollectedTextSpeed, _coinModel.CollectedTextLifetime);
    }
}
