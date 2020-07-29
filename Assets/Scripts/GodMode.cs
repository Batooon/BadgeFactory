using UnityEngine;

public class GodMode : MonoBehaviour
{
    public void AddSomeGold()
    {
        Data playerData = PlayerDataAccess.GetPlayerDatabase().GetPlayerData();
        playerData.GoldAmount += 100000;
    }
}
