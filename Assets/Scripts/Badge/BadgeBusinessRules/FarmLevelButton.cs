using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Image))]
[RequireComponent(typeof(Button))]
public class FarmLevelButton : MonoBehaviour
{
    [SerializeField]
    private Sprite _farm;
    [SerializeField]
    private Sprite _levelIncrease;

    private IPlayerDataProvider _playerData;

    private Button _button;
    private Image _image;

    private void Awake()
    {
        _playerData = PlayerDataAccess.GetPlayerDatabase();
        _image = GetComponent<Image>();
        _button=GetComponent<Button>();
        _button.onClick.AddListener(ChangeFarmLevelButton);
    }

    private void Start()
    {
        Data playerData=_playerData.GetPlayerData();
        ChangeSprite(playerData.NeedToIncreaseLevel);
    }

    public void ChangeFarmLevelButton()
    {
        Data playerData = _playerData.GetPlayerData();
        playerData.NeedToIncreaseLevel = !playerData.NeedToIncreaseLevel;
        ChangeSprite(playerData.NeedToIncreaseLevel);
        _playerData.SavePlayerData(in playerData);
    }

    private void ChangeSprite(bool needToIncreaseLevel)
    {
        if(needToIncreaseLevel)
            _image.sprite=_levelIncrease;
        else
            _image.sprite=_farm;
    }
}