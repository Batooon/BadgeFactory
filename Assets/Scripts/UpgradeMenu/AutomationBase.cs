using System;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class AutomationBase : MonoBehaviour, IAutomation
{
    public event Action DpsChanged;
    public delegate void UpgradeCallback(AutomationBase automation);
    public UpgradeCallback Upgrade;

    [HideInInspector]
    public string Name;
    [SerializeField]
    private TextMeshProUGUI _nameText;

    [SerializeField]
    private FloatReference _gold;

    [SerializeField]
    private TextMeshProUGUI _dpsText;
    [SerializeField]
    private TextMeshProUGUI _priceText;
    [HideInInspector]
    public float Cost;

    [HideInInspector]
    public float _startingCost;
    private float _costFactor = 1.07f;
    protected int _level;

    private Button _button;

    protected float Dps;
    [HideInInspector]
    public float _startingDps;

    public float GetDps()
    {
        return Dps;
    }

    public void UpdateDps()
    {
        _level += 1;
        Dps = _startingDps * _level;
        _dpsText.text = Mathf.Round(Dps).ToString();
        UpgradeCostUpdate();
        Upgrade(this);
    }

    public void UpgradeCostUpdate()
    {
        _gold.Variable.ApplyChange(-Cost);
        float levelFactor = _costFactor;
        for (int i = 0; i < _level - 1; i++)
        {
            levelFactor *= _costFactor;
        }

        Cost = _startingCost * levelFactor;
        _priceText.text = Mathf.Round(Cost).ToString();
    }

    public void Subscribe()
    {
        DpsChanged += UpdateDps;
    }

    public void Unsubscibe()
    {
        DpsChanged -= UpdateDps;
        PlayerPrefs.SetInt($"{Name}_Level", _level);
        PlayerPrefs.SetFloat($"{Name}_Dps", Dps);
        PlayerPrefs.SetFloat($"{Name}_Cost", Cost);
    }

    public void CompareCost()
    {
        _button.interactable = Mathf.Round(_gold.Value) >= Mathf.Round(Cost);
    }

    private void AfterInit()
    {
        _nameText.text = Name;
        _button = GetComponentInChildren<Button>();
        _button.onClick.AddListener(UpdateDps);
        CompareCost();
    }

    private void OnEnable()
    {
        Init();
    }

    private void OnDisable()
    {
        _button.onClick.RemoveListener(UpdateDps);
    }

    public void Init()
    {
        Dps = PlayerPrefs.GetFloat($"{Name}_Dps", 0);
        Cost = PlayerPrefs.GetFloat($"{Name}_Cost", _startingCost);
        _level = PlayerPrefs.GetInt($"{Name}_Level", 0);
        if (_level == 0)
        {
            _dpsText.text = "0";
            _priceText.text = $"HIRE {_startingCost}";
        }
        else
        {
            _dpsText.text = Mathf.Round(Dps).ToString();
            _priceText.text = Mathf.Round(Cost).ToString();
        }
        AfterInit();
    }
}
