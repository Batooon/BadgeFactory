using UnityEngine;
using UnityEngine.Events;

public class AdvertisementWrapper : MonoBehaviour
{
    [SerializeField] private UnityEvent OnRewardedAdFinished;

    public void ShowRewardedAd()
    {
        AdsManager.Instance.ShowRewardedAd(OnRewardedAdFinished);
    }
}
