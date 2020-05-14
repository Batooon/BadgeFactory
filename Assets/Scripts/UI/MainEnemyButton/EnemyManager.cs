using UnityEngine;
using UnityEngine.Events;
using System.Collections.Generic;
using UnityEngine.UI;

//Developer: Antoshka

public class EnemyManager : MonoBehaviour
{
    [SerializeField]
    //private PlayerData PlayerData;

    public List<CoinLogic> spawnedCoins = new List<CoinLogic>();

    public FloatReference CurrentHp;
    public IntReference MaxHp;

    public UnityEvent DamageEvent;
    public UnityEvent DeathEvent;

    [SerializeField]
    private GameObject _goldObj;
    [SerializeField]
    private IntReference _clickPower;
    [SerializeField]
    private IntReference _damagePerSecond;

    private EnemyDataVariable _currentEnemy;

    private Image _badgeImage;

    [SerializeField]
    private IntReference _level;
    [SerializeField]
    private IntReference _currentLevelProgress;
    private float exponent = 1.55f;

    public void SpawnCoins()
    {
        int amount = Random.Range(3, 5);
        int oneCoinCost;
        if (_currentEnemy.EnemyDataVar.CoinsReward >= amount)
            oneCoinCost = Mathf.CeilToInt(_currentEnemy.EnemyDataVar.CoinsReward / amount);
        else
        {
            amount = 1;
            oneCoinCost = _currentEnemy.EnemyDataVar.CoinsReward;
        }

        for (int i = 0; i < amount; i++)
        {
            GameObject coinGO = Instantiate(_goldObj, transform.position, Quaternion.identity);
            coinGO.transform.SetParent(transform.parent, false);
            CoinLogic coin = coinGO.GetComponent<CoinLogic>();
            coin.Cost = oneCoinCost;
            coin.CoinCollected += CollectCoin;
            coin.CoinDestroyed += DestroyCoin;
            spawnedCoins.Add(coin);
            LeanTween.move(coinGO, new Vector2(Random.Range(-1.5f, 1.5f), Random.Range(-1.5f, 1.5f)), .5f);
            Destroy(coinGO, 10f);
        }
    }

    private void CollectCoin(CoinLogic coin)
    {
        //PlayerData.CoinCollected(coin);
        spawnedCoins.Remove(coin);
        Destroy(coin.gameObject);
    }

    private void DestroyCoin(CoinLogic coin)
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
        {
            GameEvents.current.BadgeCreatedCallback(_currentEnemy);
            DeathEvent.Invoke();
        }
    }

    public void InitializeNewEnemy(EnemyDataVariable _newEnemy)
    {
        _currentEnemy = _newEnemy;
        ResetHP();
        int cost = Mathf.CeilToInt(_currentEnemy.EnemyDataVar.Hp * 0.0667f);
        _currentEnemy.EnemyDataVar.CoinsReward = cost;
    }

    private void ResetHP()
    {
        exponent = 1f;
        for (int i = 0; i < _level - 1; i++)
        {
            exponent *= 1.55f;
        }

        int hp = (int)(10 * ((_level - 1) + exponent));
        if (_level.Value % 5 == 0 && _currentLevelProgress == 10)
        {
            hp *= 10;
        }
        _currentEnemy.EnemyDataVar.Hp = hp;
        MaxHp.Variable.SetValue(hp);
        CurrentHp.Variable.SetValue(0);
    }

    private void Awake()
    {
        MaxHp.Variable.SetValue(PlayerPrefs.GetInt("MAXHP", 10));
        _badgeImage = GetComponent<Image>();
    }

    private void OnApplicationQuit()
    {
        PlayerPrefs.SetFloat("MAXHP", MaxHp.Value);
    }

    private void OnApplicationPause(bool pause)
    {
        if (pause)
            PlayerPrefs.SetFloat("MAXHP", MaxHp.Value);
    }

    private void Update()
    {
        DealDamage(_damagePerSecond * Time.deltaTime);
    }
}
