using UnityEngine;
using UnityEngine.Events;
using System.Collections.Generic;
using System;
using UnityEngine.UI;

//Developer: Antoshka

public class EnemyManager : MonoBehaviour
{
    [SerializeField]
    private PlayerData PlayerData;

    public List<Coin> spawnedCoins = new List<Coin>();

    public FloatReference CurrentHp;
    public FloatReference MaxHp;

    public UnityEvent DamageEvent;
    public UnityEvent DeathEvent;

    [SerializeField]
    private GameObject _goldObj;
    [SerializeField]
    private FloatReference _clickPower;
    [SerializeField]
    private FloatReference _damagePerSecond;

    private EnemyDataVariable _currentEnemy;

    private Image _badgeImage;

    private int _level;
    private float exponent = 1.55f;

    public void SpawnCoins()
    {
        int amount = UnityEngine.Random.Range(3, 5);
        float oneCoinCost;
        if (_currentEnemy.EnemyDataVar.CoinsReward >= amount)
            oneCoinCost = Mathf.Round(_currentEnemy.EnemyDataVar.CoinsReward) / amount;
        else
        {
            amount = 1;
            oneCoinCost = _currentEnemy.EnemyDataVar.CoinsReward;
        }

        for (int i = 0; i < amount; i++)
        {
            GameObject coinGO = Instantiate(_goldObj, transform.position, Quaternion.identity);
            coinGO.transform.SetParent(transform.parent, false);
            Coin coin = coinGO.GetComponent<Coin>();
            coin.Cost = oneCoinCost;
            coin.CoinCollected += CollectCoin;
            coin.CoinDestroyed += DestroyCoin;
            spawnedCoins.Add(coin);
            LeanTween.move(coinGO, new Vector2(UnityEngine.Random.Range(-1.5f, 1.5f), UnityEngine.Random.Range(-1.5f, 1.5f)), .5f);
        }
    }

    private void CollectCoin(Coin coin)
    {
        PlayerData.CoinCollected(coin);
        spawnedCoins.Remove(coin);
        Destroy(coin.gameObject);
    }

    private void DestroyCoin(Coin coin)
    {
        spawnedCoins.Remove(coin);
    }

    public void ClickDamage()
    {
        DealDamage(_clickPower.Value);
    }

    private void DealDamage(float amountOfDamage)
    {
        CurrentHp.Variable.ApplyChange(amountOfDamage);
        DamageEvent.Invoke();

        var tempColor = _badgeImage.color;
        tempColor.a = Mathf.Clamp01(Mathf.InverseLerp(0, MaxHp.Value, CurrentHp.Value));
        _badgeImage.color = tempColor;


        if (CurrentHp.Value >= MaxHp.Value)
            DeathEvent.Invoke();
    }

    public void InitializeNewEnemy(EnemyDataVariable _newEnemy)
    {
        _level += 1;
        _currentEnemy = _newEnemy;
        ResetHP();
        float cost= _currentEnemy.EnemyDataVar.Hp * 0.0667f;
        _currentEnemy.EnemyDataVar.CoinsReward = cost;
    }

    private void ResetHP()
    {
        exponent = 1f;
        for (int i = 0; i < _level - 1; i++)
        {
            exponent *= 1.55f;
        }

        float hp = 10 * ((_level - 1) + exponent);
        _currentEnemy.EnemyDataVar.Hp = hp;
        MaxHp.Variable.SetValue(hp);
        CurrentHp.Variable.SetValue(0);
    }

    private void Awake()
    {
        MaxHp.Variable.SetValue(PlayerPrefs.GetFloat("MAXHP", 10f));
        _level = PlayerPrefs.GetInt("LEVEL", 1);
        _badgeImage = GetComponent<Image>();
    }

    private void OnDisable()
    {
        PlayerPrefs.SetInt("LEVEL", _level);
        PlayerPrefs.SetFloat("MAXHP", MaxHp.Value);
    }

    private void Update()
    {
        DealDamage(_damagePerSecond.Value * Time.deltaTime);
    }
}
