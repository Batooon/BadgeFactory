using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;
using UnityEngine.Events;

[Serializable]
public struct EnemyData
{
    public Sprite EnemySprite;
    public AudioClip[] Sounds;
    public AudioClip DeathSounds;
    public FloatReference Hp;
    public FloatReference CoinsReward;
}

public class EnemyButtonData : MonoBehaviour
{
    public List<EnemyData> EnemyDataList = new List<EnemyData>();
    public UnityEvent InitializeNewEnemy;

    [SerializeField]
    private EnemyDataVariable CurrentEnemyData;

    public void SetUpEnemy()
    {
        int _enemiesLength = EnemyDataList.Count - 1;
        CurrentEnemyData.SetValue(EnemyDataList[UnityEngine.Random.Range(0, _enemiesLength)]);
        InitializeNewEnemy.Invoke();
    }

    private void Awake()
    {
        SetUpEnemy();
    }
}
