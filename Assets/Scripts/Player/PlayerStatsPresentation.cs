using System.Numerics;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerStatsPresentation : MonoBehaviour, IObserver
{
    [SerializeField] private TextMeshProUGUI _goldAmount;
    [SerializeField] private string _goldAmountTemplate;
    [SerializeField] private TextMeshProUGUI _levelText;
    [SerializeField] private string _levelTemplate;
    [SerializeField] private Slider _levelProgress;

    private PlayerData _playerData;

    public void Init(PlayerData playerData)
    {
        _playerData = playerData;
        _levelProgress.maxValue = _playerData.MaxLevelProgress;
        _levelProgress.minValue = 0;

    }

    private void Start()
    {
        _levelProgress.maxValue = _playerData.MaxLevelProgress;
    }

    private void OnEnable()
    {
        _playerData.Attach(this);

        ChangeGoldAmount(_playerData.Gold);
        ChangeLevelProgress(_playerData.LevelProgress);
        ChangeLevel(_playerData.Level);
    }

    private void OnDisable()
    {
        _playerData.Detach(this);
    }

    public void ChangeLevel(int newLevel)
    {
        _levelText.text = string.Format(_levelTemplate, newLevel.ToString());
        _levelProgress.value = _playerData.MaxLevelProgress;
    }

    public void ChangeGoldAmount(BigInteger newGoldAmount)
    {
        _goldAmount.text = string.Format(_goldAmountTemplate, newGoldAmount.ConvertValue());
    }

    public void ChangeLevelProgress(int newLevelProgressValue)
    {
        _levelProgress.value = newLevelProgressValue;
    }

    public void Fetch(ISubject subject)
    {
        PlayerData playerData = subject as PlayerData;
        ChangeGoldAmount(playerData.Gold);
        ChangeLevelProgress(playerData.LevelProgress);
        ChangeLevel(playerData.Level);
    }
}
