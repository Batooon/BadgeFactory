using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Image))]
[RequireComponent(typeof(Button))]
public class FarmLevelButton : MonoBehaviour
{
    [SerializeField] private Sprite _farm;
    [SerializeField] private Sprite _levelIncrease;

    private PlayerData _playerData;
    private Button _button;
    private Image _image;

    public void Init(PlayerData playerData)
    {
        _playerData = playerData;
        _image = GetComponent<Image>();
        _button = GetComponent<Button>();
        _button.onClick.AddListener(ChangeFarmLevelButton);
        ChangeSprite(_playerData.NeedToIncreaseLevel);
    }

    public void ChangeFarmLevelButton()
    {
        _playerData.NeedToIncreaseLevel = !_playerData.NeedToIncreaseLevel;
        ChangeSprite(_playerData.NeedToIncreaseLevel);
    }

    private void ChangeSprite(bool needToIncreaseLevel)
    {
        if(needToIncreaseLevel)
            _image.sprite=_levelIncrease;
        else
            _image.sprite=_farm;
    }
}