using UnityEngine;
using UnityEngine.Events;
using System;
using UnityEngine.UI;
using UnityEngine.Advertisements;
using System.Collections.Generic;

//Developer: Antoshka

[Serializable]
public struct EnemyArgs
{
    public Sprite EnemySprite;
    public FloatReference Hp;
    public AudioClip[] PunchSounds;
    public AudioClip DeathSound;
    public FloatReference RewardCoinsAmount;
}

public class ClickButton : MonoBehaviour
{
    public DamageDealer _damageDealer;

    //TODO: Сделать List<EnemyArgs> и каждый раз, когда враг умирает брать другого рандомного(звуки, картинка), а хп и вознаграждение настраивать через баланс
    public List<EnemyArgs> EnemyData = new List<EnemyArgs>();

    private FloatReference StartingHp;
    private EnemyArgs CurrentEnemy;
    public FloatVariable CurrentHp;
    private Image _enemyImage;

    public UnityEvent DamageEvent;
    public UnityEvent DeathEvent;

    private int _clicks;

    private void Start()
    {
        _enemyImage = GetComponent<Image>();
        CurrentEnemy = GenerateNewEnemy();
        InitializeNewEnemy();

        _damageDealer = GetComponent<DamageDealer>();

        _clicks = 0;
    }

    public void ButtonClicked()
    {
        _clicks++;

        CurrentHp.ApplyChange(-_damageDealer.DamageAmount);
        DamageEvent.Invoke();

        if (CurrentHp.Value <= 0)
        {
            DeathEvent.Invoke();
            CurrentEnemy = GenerateNewEnemy();
            InitializeNewEnemy();
        }
    }

    private EnemyArgs GenerateNewEnemy()
    {
        int enemieslength = EnemyData.Count;
        return EnemyData[UnityEngine.Random.Range(0, enemieslength - 1)];
    }

    private void InitializeNewEnemy()
    {
        _enemyImage.sprite = CurrentEnemy.EnemySprite;
        StartingHp = CurrentEnemy.Hp;
        CurrentHp.SetValue(StartingHp);
    }
}
