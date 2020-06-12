using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerStatsPresentation : MonoBehaviour
{
    [SerializeField]
    private string _goldAmountTemplate;
    [SerializeField]
    private TextMeshProUGUI _goldAmount;
    [SerializeField]
    private string _levelTemplate;
    [SerializeField]
    private TextMeshProUGUI _levelText;
    [SerializeField]
    private Slider _levelProgress;

    private PlayerDataAccess _playerDataAccess;
    private Data _playerData;

    private void Start()
    {
        _playerDataAccess = PlayerDataAccess.GetPlayerDatabase();
        _playerData = _playerDataAccess.GetPlayerData();

        _levelProgress.maxValue = _playerData.MaxLevelProgress;
        _levelProgress.minValue = 0;
        _levelText.text = string.Format(_levelTemplate, _playerData.Level.ToString());
        _goldAmount.text = string.Format(_goldAmountTemplate, _playerData.GoldAmount.ConvertValue());
        _playerData.GoldAmountChanged += ChangeGoldAmount;
        _playerData.PlayerLevelProgressChanged += ChangeLevelProgress;
        _playerData.PlayerLevelChanged += ChangeLevel;
    }

    public void ChangeLevel(int newLevel)
    {
        _levelText.text = string.Format(_levelTemplate, newLevel.ToString());
        _levelProgress.value = PlayerDataAccess.GetPlayerDatabase().GetPlayerData().MaxLevelProgress;
    }

    public void ChangeGoldAmount(int newGoldAmount)
    {
        _goldAmount.text = string.Format(_goldAmountTemplate, newGoldAmount.ConvertValue());
    }

    public void ChangeLevelProgress(int newLevelProgressValue)
    {
        _levelProgress.value = newLevelProgressValue;
    }

    private void OnApplicationQuit()
    {
        _playerData.GoldAmountChanged -= ChangeGoldAmount;
        _playerData.PlayerLevelProgressChanged -= ChangeLevelProgress;
        _playerData.PlayerLevelChanged -= ChangeLevel;
    }
}
