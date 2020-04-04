using System;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System.Timers;

//Developer: Antoshka
[Serializable]
public class Save
{
    public int[] date = new int[6];
    public bool isNewPlayer = true;
}
/*
public class PlayerData : MonoBehaviour
{
    public static event Action DoubleGoldRewardVideoWatched;

    Save saveData = new Save();

    [SerializeField]
    //private List<AutomationBase> _automations = new List<AutomationBase>();

    public IntReference _dps;
    [SerializeField]
    private TextMeshProUGUI _dpsText;

    [SerializeField]
    private IntReference _clickPower;
    [SerializeField]
    private TextMeshProUGUI _clickPowerText;

    [SerializeField]
    private IntReference _goldAmount;
    [SerializeField]
    private TextMeshProUGUI _goldText;

    [SerializeField]
    private EnemyDataVariable _currentEnemy;

    [SerializeField]
    private UpgradeLevelsAmount _levelsAmountToUpgradeController;

    [SerializeField]
    private GameObject ReturningScreen;
    [SerializeField]
    private TextMeshProUGUI _gainedGoldText;
    [SerializeField]
    private TextMeshProUGUI _dragonText;

    [SerializeField]
    private GameObject _collectedCoinText;
    [SerializeField]
    private GameObject _coinTextParent;

    [SerializeField]
    private IntReference _level;
    [SerializeField]
    private TextMeshProUGUI _levelText;
    [SerializeField]
    private Image _currentLevelProgress;
    [SerializeField]
    private IntReference _currentLevelProgressValue;

    [SerializeField]
    private Sprite _farmSprite;
    [SerializeField]
    private Sprite _activeSprite;
    [SerializeField]
    private Image _farmButtonImage;

    private bool _farm = false;

    private int _gainedGold;

    public void SerializeAutomations()
    {
        TextAsset _excelData = Resources.Load<TextAsset>("AutomationsData");

        string[] data = _excelData.text.Split('\n');

        for (int i = 1, j = 0; i < data.Length - 1 && j < _automations.Count; i++, j++)
        {
            string[] row = data[i].Split(';');

            _automations[j].Name = row[0];
            _automations[j]._startingCost = int.Parse(row[1]);
            _automations[j]._startingDps = int.Parse(row[2]);
        }
    }

    public void CalculateDps(AutomationBase automation)
    {
        RecalculateDps();
        _dpsText.text = _dps.Value.ConvertValue();
        _clickPowerText.text = _clickPower.Value.ConvertValue();
        _goldText.text = _goldAmount.Value.ConvertValue();

        foreach (var item in _automations)
        {
            if (item.gameObject.activeInHierarchy)
                item.CompareCost();
        }
    }

    public void SetFarmOnOff()
    {
        _farm = !_farm;
        if (_farm)
        {
            _currentLevelProgress.fillAmount = 1;
            _farmButtonImage.sprite = _farmSprite;
        }
        else
        {
            _currentLevelProgress.fillAmount = Mathf.Clamp01(Mathf.InverseLerp(0, 10, _currentLevelProgressValue));
            _farmButtonImage.sprite = _activeSprite;
        }
    }

    private void RecalculateDps()
    {
        int _currentDps = 0;
        int _currentClickPower = 0;
        foreach (var item in _automations)
        {
            if (item is ClickPower)
            {
                _currentClickPower += item.GetDps();
            }
            else
            {
                _currentDps += item.GetDps();
            }
        }
        _dps.Variable.SetValue(_currentDps);
        _clickPower.Variable.SetValue(_currentClickPower);
    }

    private void Awake()
    {
        SerializeAutomations();
        Init();
    }

    private void Start()
    {
        SerializeData();
        CalculateAbsenseTime();
        if (saveData.date[0] != 0)
            ActivateReturningPlayerWindow(_gainedGold);
        GameEvents.current.AddAdditionalGold += GainAdditionalGold;
    }

    private void ActivateReturningPlayerWindow(int gainedGold)
    {
        ReturningScreen.SetActive(true);
        _gainedGoldText.text = gainedGold.ConvertValue();
        _dragonText.text = $"Tap on me to watch an ad and get additional {gainedGold.ConvertValue()}";
    }

    public void GainAdditionalGold()
    {
        _goldAmount.Variable.ApplyChange(_gainedGold);
        _goldText.text = _goldAmount.Value.ConvertValue();
    }

    private void CalculateAbsenseTime()
    {
        if (!saveData.isNewPlayer)
        {
            DateTime lastTimeVisited = new DateTime(saveData.date[0], saveData.date[1], saveData.date[2],
                saveData.date[3], saveData.date[4], saveData.date[5]);
            TimeSpan timeDifference = DateTime.Now - lastTimeVisited;

            int gainedGold = (int)(timeDifference.TotalSeconds * _dps.Value / _currentEnemy.EnemyDataVar.Hp);
            _goldAmount.Variable.ApplyChange(gainedGold);
            _goldText.text = _goldAmount.Value.ConvertValue();
            _gainedGold = gainedGold;
        }
    }

    private void OnApplicationPause(bool pause)
    {
        if (pause)
        {
            RememberDate();
            SaveData();
        }
    }

    private void OnApplicationQuit()
    {
        RememberDate();
        SaveData();
    }

    private void RememberDate()
    {
        PlayerPrefs.SetInt("YEAR", DateTime.Now.Year);
        PlayerPrefs.SetInt("MONTH", DateTime.Now.Month);
        PlayerPrefs.SetInt("DAY", DateTime.Now.Day);
        PlayerPrefs.SetInt("HOUR", DateTime.Now.Hour);
        PlayerPrefs.SetInt("MINUTE", DateTime.Now.Minute);
        PlayerPrefs.SetInt("SECOND", DateTime.Now.Second);
        PlayerPrefs.SetInt("NEWPLAYER", 0);
    }

    private void SerializeData()
    {
        saveData.date[0] = PlayerPrefs.GetInt("YEAR", 0);
        saveData.date[1] = PlayerPrefs.GetInt("MONTH", 0);
        saveData.date[2] = PlayerPrefs.GetInt("DAY", 0);
        saveData.date[3] = PlayerPrefs.GetInt("HOUR", 0);
        saveData.date[4] = PlayerPrefs.GetInt("MINUTE", 0);
        saveData.date[5] = PlayerPrefs.GetInt("SECOND", 0);
        saveData.isNewPlayer = Convert.ToBoolean(PlayerPrefs.GetInt("NEWPLAYER", 1));
    }

    public void CoinCollected(Coin coin)
    {
        Vector3 coinPosition = coin.transform.position;
        _goldAmount.Variable.ApplyChange(coin.Cost);
        _goldText.text = _goldAmount.Value.ConvertValue();

        foreach (var item in _automations)
        {
            if (item.gameObject.activeInHierarchy)
                item.CompareCost();
        }

        CollectedCoinText collectedText = Instantiate(_collectedCoinText, coinPosition,
            Quaternion.identity, _coinTextParent.transform).GetComponent<CollectedCoinText>();
        //collectedText.StartMotion(coin.Cost);
    }

    public void StepLevelBack()
    {
        _level.Variable.ApplyChange(-1);
        _currentLevelProgressValue.Variable.SetValue(0);
        _levelText.text = $"Level {_level.Value}";
        _farm = true;
        _farmButtonImage.sprite = _farmSprite;
        _currentLevelProgress.fillAmount = 1;
    }

    public void OnEnemyDie()
    {
        if (_farm)
            return;
        if (_currentLevelProgressValue.Value == 10)
            _currentLevelProgressValue.Variable.SetValue(0);
        else
            _currentLevelProgressValue.Variable.ApplyChange(1);

        if (_currentLevelProgressValue == 0)
        {
            _level.Variable.ApplyChange(1);
            _levelText.text = $"Level {_level.Value}";
            PlayGames.AddScoreToleaderBoard(_level.Value);
        }
        _currentLevelProgress.fillAmount = Mathf.Clamp01(Mathf.InverseLerp(0, 10, _currentLevelProgressValue));
    }

    public void Init()
    {
        for (int i = 0; i < _automations.Count; i++)
        {
            _automations[i].Subscribe();
            _automations[i].Upgrade = CalculateDps;
            _automations[i].Init();
            //_levelsAmountToUpgradeController.UpgradeLevelsAmountChanged += _automations[i].RecalculateCostToLevelsAmount;
        }
        _dps.Variable.SetValue(PlayerPrefs.GetInt("DPS", 0));
        _clickPower.Variable.SetValue(PlayerPrefs.GetInt("CLICKPOWER", 1));
        _goldAmount.Variable.SetValue(PlayerPrefs.GetInt("GOLD", 100000000));
        _dpsText.text = _dps.Value.ConvertValue();
        _clickPowerText.text = _clickPower.Value.ConvertValue();
        _goldText.text = _goldAmount.Value.ConvertValue();
        _level.Variable.SetValue(PlayerPrefs.GetInt("LEVEL", 1));
        _levelText.text = $"Level {_level.Value}";
        _currentLevelProgressValue.Variable.SetValue(PlayerPrefs.GetInt("LEVELPROGRESS", 0));
        _currentLevelProgress.fillAmount = Mathf.Clamp01(Mathf.InverseLerp(0, 10, _currentLevelProgressValue));
    }

    private void SaveData()
    {
        for (int i = 0; i < _automations.Count; i++)
        {
            _automations[i].Unsubscibe();
            _automations[i].Upgrade = null;
        }
        PlayerPrefs.SetInt("DPS", _dps.Value);
        PlayerPrefs.SetInt("CLICKPOWER", _clickPower.Value);
        PlayerPrefs.SetInt("GOLD", _goldAmount.Value);
        PlayerPrefs.SetInt("LEVEL", _level);
        PlayerPrefs.SetInt("LEVELPROGRESS", _currentLevelProgressValue);
        GameEvents.current.AddAdditionalGold -= GainAdditionalGold;
        PlayerPrefs.Save();
    }
}*/
