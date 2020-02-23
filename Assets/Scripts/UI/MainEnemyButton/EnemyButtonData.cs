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
    public float Hp;
    public float CoinsReward;
}

public class EnemyButtonData : MonoBehaviour
{
    public List<EnemyData> EnemyDataList = new List<EnemyData>();
    public List<EnemyData> BossDataList = new List<EnemyData>();
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
            InitializeNewEnemy.Invoke();
        }
        else
        {
            int _enemiesLength = EnemyDataList.Count - 1;
            CurrentEnemyData.SetValue(EnemyDataList[UnityEngine.Random.Range(0, _enemiesLength)]);
            InitializeNewEnemy.Invoke();
        }
    }

    public void IsBossDefeated()
    {
        /*if (_isBoss)
        {
            if (_countdown > 0)
                _isBossDefeated = true;
            else
            {
                _isBossDefeated = false;
                BossNotDefeated.Invoke();
            }
        }*/
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
    {
        SetUpEnemy();
    }
}
