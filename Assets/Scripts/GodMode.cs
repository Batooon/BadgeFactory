using UnityEngine;

public class GodMode : MonoBehaviour
{
    private PlayerData _playerData;

    public void Init(PlayerData playerData)
    {
        _playerData = playerData;
    }

    public void AddSomeGold()
    {
        _playerData.Gold += 12478000000;
    }
}
