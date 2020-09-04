using UnityEngine;

[RequireComponent(typeof(BoxRewardPresentation))]
public class BoxReward : MonoBehaviour
{
    private PlayerData _playerData;
    private long _rewardValue;

    public void Init(PlayerData playerData)
    {
        _playerData = playerData;
    }

    public void SetRewardValue(long reward)
    {
        _rewardValue = reward;
    }

    public void CollectReward()
    {
        _playerData.Gold += _rewardValue;
    }
}
