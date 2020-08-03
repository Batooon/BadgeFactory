using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerStatsPresentation : MonoBehaviour
{
    [SerializeField] private string _goldAmountTemplate;
    [SerializeField] private string _levelTemplate;
    [SerializeField] private TextMeshProUGUI _goldAmount;
    [SerializeField] private TextMeshProUGUI _levelText;
    [SerializeField] private Slider _levelProgress;

    private PlayerData _playerData;

    public void Init(PlayerData playerData)
    {
        _playerData = playerData;

        _playerData.GoldChanged += ChangeGoldAmount;
        _playerData.LevelProgressChanged += ChangeLevelProgress;
        _playerData.LevelChanged += ChangeLevel;

        _levelProgress.maxValue = _playerData.MaxLevelProgress;
        _levelProgress.minValue = 0;

        ChangeGoldAmount(_playerData.Gold);
        ChangeLevel(_playerData.Level);
    }

    private void Start()
    {
        ChangeLevelProgress(_playerData.LevelProgress);
    }

    private void OnEnable()
    {
        if (_playerData == null)
            return;

        _playerData.GoldChanged += ChangeGoldAmount;
        _playerData.LevelProgressChanged += ChangeLevelProgress;
        _playerData.LevelChanged += ChangeLevel;

        _levelProgress.maxValue = _playerData.MaxLevelProgress;
        _levelProgress.minValue = 0;

        ChangeGoldAmount(_playerData.Gold);
        ChangeLevelProgress(_playerData.LevelProgress);
        ChangeLevel(_playerData.Level);
    }

    private void OnDisable()
    {
        _playerData.GoldChanged -= ChangeGoldAmount;
        _playerData.LevelProgressChanged -= ChangeLevelProgress;
        _playerData.LevelChanged -= ChangeLevel;
    }

    public void ChangeLevel(int newLevel)
    {
        _levelText.text = string.Format(_levelTemplate, newLevel.ToString());
        _levelProgress.value = _playerData.MaxLevelProgress;
    }

    public void ChangeGoldAmount(long newGoldAmount)
    {
        _goldAmount.text = string.Format(_goldAmountTemplate, newGoldAmount.ConvertValue());
    }

    public void ChangeLevelProgress(int newLevelProgressValue)
    {
        _levelProgress.value = newLevelProgressValue;
    }
}
