using System;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class AutomationBase : MonoBehaviour, IAutomation
{
    public int Id;

    public event Action DpsChanged;
    public delegate void UpgradeCallback(AutomationBase automation);
    public UpgradeCallback Upgrade;

    [HideInInspector]
    public string Name;
    [SerializeField]
    private TextMeshProUGUI _nameText;

    [SerializeField]
    private IntReference _gold;

    [SerializeField]
    protected TextMeshProUGUI _dpsText;
    [SerializeField]
    protected TextMeshProUGUI _priceText;
    [HideInInspector]
    public int Cost;

    [HideInInspector]
    public int _startingCost;
    private float _costFactor = 1.07f;
    protected int _level;

    private Button _button;

    protected int Dps;
    [HideInInspector]
    public int _startingDps;

    private int _amountOfLevelsToUpgrade = 1;

    public int GetDps()
    {
        return Dps;
    }

    public void UpdateDps()
    {
        if (_amountOfLevelsToUpgrade > 1)
            AddMultipleLevelDps();
        else
            AddDps();
        UpgradeCostUpdate();
        Upgrade(this);
    }

    private void AddDps()
    {
        _level += 1;
        Dps = Mathf.RoundToInt(_startingDps * 1.07f * _level);
        _dpsText.text = Dps.ConvertValue();
    }

    private void AddMultipleLevelDps()
    {
        _level += _amountOfLevelsToUpgrade;
        Dps = _startingDps * _level;
        _dpsText.text = Dps.ConvertValue();
    }

    private void RecalculateCost()
    {
        float _levelFactor = _costFactor;
        for (int i = 0; i < _level - 1; i++)
        {
            _levelFactor *= _costFactor;
        }
        Cost = (int)(_startingCost * _levelFactor);
    }
    public void UpgradeCostUpdate()
    {
        _gold.Variable.ApplyChange(-Cost);
        if (_amountOfLevelsToUpgrade > 1)
            RecalculateCostToLevelsAmount(_amountOfLevelsToUpgrade);
        else
            RecalculateCost();
        _priceText.text = Cost.ConvertValue();
    }

    public void Subscribe()
    {
        DpsChanged += UpdateDps;
    }

    public void Unsubscibe()
    {
        DpsChanged -= UpdateDps;
        PlayerPrefs.SetInt($"{Name}_Level", _level);
        PlayerPrefs.SetInt($"{Name}_Dps", Dps);
        PlayerPrefs.SetInt($"{Name}_Cost", Cost);
    }

    public void CompareCost()
    {
        _button.interactable = _gold.Value >= Cost;
        if (Id >= 2 && _button.interactable)
        {
            GameEvents.current.ActivateNewAutomationEvent();
            Id = 0;
        }
    }

    protected void AfterInit()
    {
        _nameText.text = Name;
        _button = GetComponentInChildren<Button>();
        CompareCost();
    }

    private void OnEnable()
    {
        _button.onClick.AddListener(UpdateDps);
        CompareCost();
    }

    private void OnDisable()
    {
        _button.onClick.RemoveListener(UpdateDps);
        PlayerPrefs.SetInt("UpgradeLevel", _amountOfLevelsToUpgrade);
    }

    private void OnApplicationQuit()
    {
        _button.onClick.RemoveListener(UpdateDps);
        PlayerPrefs.SetInt("UpgradeLevel", _amountOfLevelsToUpgrade);
    }

    public virtual void Init()
    {
        Dps = PlayerPrefs.GetInt($"{Name}_Dps", 0);
        Cost = PlayerPrefs.GetInt($"{Name}_Cost", _startingCost);
        _level = PlayerPrefs.GetInt($"{Name}_Level", 0);
        _amountOfLevelsToUpgrade = PlayerPrefs.GetInt("UpgradeLevel", _amountOfLevelsToUpgrade);
        if (_level == 0)
        {
            _dpsText.text = "0";
            _priceText.text = $"HIRE {_startingCost.ConvertValue()}";
        }
        else
        {
            _dpsText.text = Dps.ConvertValue();
            _priceText.text = Cost.ConvertValue();
        }
        AfterInit();
    }

    public void RecalculateCostToLevelsAmount(int amount)
    {
        _amountOfLevelsToUpgrade = amount;
        int _cost = 0;
        int lvl = _level;
        float levelfactor;
        int currentCost;
        for (int i = 0; i < amount; i++)
        {
            levelfactor = _costFactor;
            for (int j = 0; j < lvl-1; j++)
            {
                levelfactor *= _costFactor;
            }
            currentCost = (int)(_startingCost * levelfactor);
            _cost += currentCost;
            lvl += 1;
        }
        Cost = _cost;
        _priceText.text = Cost.ConvertValue();
        CompareCost();
    }
}
