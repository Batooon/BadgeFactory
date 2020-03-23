using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using TMPro;
using UnityEngine.Events;

[Serializable]
public struct EnemyData
{
    public Sprite EnemySprite;
    public AudioClip[] Sounds;
    public AudioClip DeathSounds;
    public int Hp;
    public int CoinsReward;
    public bool isRare;
    public int Id;
}

public class EnemyButtonData : MonoBehaviour
{
    public List<EnemyData> BossDataList = new List<EnemyData>();
    public List<EnemyData> EnemyDataList = new List<EnemyData>();
    public UnityEvent InitializeNewEnemy;
    public UnityEvent BossNotDefeated;

    [SerializeField]
    private EnemyDataVariable CurrentEnemyData;
    [SerializeField]
    private IntReference _level;
    [SerializeField]
    private IntReference _currentLevelProgress;

    [SerializeField]
    private TextMeshProUGUI _countdownText;

    private float _countdown = 30f;
    private bool _isBossDefeated;
    private bool _isBoss = false;

    private void LoadResources()
    {
        Sprite[] badges = Resources.LoadAll<Sprite>("значки");
        InitSprites(badges);
        Sprite[] badges1 = Resources.LoadAll<Sprite>("значки1");
        InitSprites(badges1);
        Sprite[] badge2 = Resources.LoadAll<Sprite>("значки2");
        InitSprites(badge2);
        Sprite[] badge3 = Resources.LoadAll<Sprite>("значки3");
        InitSprites(badge3);
        Sprite[] badge4 = Resources.LoadAll<Sprite>("значки4");
        InitSprites(badge4);
        Sprite[] badge5 = Resources.LoadAll<Sprite>("значки5");
        InitSprites(badge5);
        Sprite[] badge6 = Resources.LoadAll<Sprite>("значки6");
        InitSprites(badge6);
        Sprite[] badge7 = Resources.LoadAll<Sprite>("значки7");
        InitSprites(badge7);

        Sprite[] bosses = Resources.LoadAll<Sprite>("босс");
        InitBossesSprites(bosses);
    }

    private void InitSprites(Sprite[] sprites)
    {
        for (int i = 0; i < sprites.Length; i++)
        {
            EnemyData enemyData = new EnemyData();
            enemyData.EnemySprite = sprites[i];
            enemyData.Id = i;
            EnemyDataList.Add(enemyData);
        }
    }

    private void InitBossesSprites(Sprite[] sprites)
    {
        for (int i = 0; i < sprites.Length; i++)
        {
            EnemyData bossData = new EnemyData();
            bossData.EnemySprite = sprites[i];
            bossData.Id = i;
            bossData.isRare = true;
            BossDataList.Add(bossData);
        }
    }

    public void SetUpEnemy()
    {
        _isBoss = false;
        _countdown = 30f;
        StopAllCoroutines();
        if (_countdownText.gameObject.activeInHierarchy)
            _countdownText.gameObject.SetActive(false);
        if (_level.Value % 5 == 0 && _currentLevelProgress.Value == 10)
        {
            _isBoss = true;
            _countdownText.text = _countdown.ToString();
            _countdownText.gameObject.SetActive(true);
            StartCoroutine("Countdown", _countdown);
            int bossesLength = BossDataList.Count - 1;
            CurrentEnemyData.SetValue(BossDataList[UnityEngine.Random.Range(0, bossesLength)]);
        }
        else
        {
            int _enemiesLength = EnemyDataList.Count - 1;
            CurrentEnemyData.SetValue(EnemyDataList[UnityEngine.Random.Range(0, _enemiesLength)]);
        }
        InitializeNewEnemy.Invoke();
    }

    private IEnumerator Countdown(float time)
    {
        while (time > 0)
        {
            time--;
            _countdownText.text = time.ToString();
            yield return new WaitForSeconds(1);
        }
        _countdownText.gameObject.SetActive(false);
        BossNotDefeated.Invoke();
        InitializeNewEnemy.Invoke();
    }

    private void Awake()
    {/*
        LoadResources();
        SetUpEnemy();*/
    }
}
