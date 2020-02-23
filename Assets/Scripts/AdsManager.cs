using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Advertisements;

//Developer: Antoshka

public class AdsManager : MonoBehaviour, IUnityAdsListener
{
    public bool IsTestMode = true;

    private int _googlePlayId = 3445518;
    private int _appStoreId = 3445519;

    private string _rewardedVideoPlacement = "rewardedVideo";
    private string _bannerPlacement = "BottomBanner";
    private string _adVideoPlacement = "video";

    public void OnUnityAdsDidError(string message)
    {
    }

    public void OnUnityAdsDidFinish(string placementId, ShowResult showResult)
    {
    }

    public void OnUnityAdsDidStart(string placementId)
    {
    }

    public void OnUnityAdsReady(string placementId)
    {
    }

    private void Start()
    {
#if UNITY_EDITOR
        Advertisement.Initialize(_googlePlayId.ToString(), true);
#elif UNITY_ANDROID
        Advertisement.Initialize(_googlePlayId.ToString(), IsTestMode);
#else
        Advertisement.Initialize(_appStoreId.ToString(), IsTestMode);
#endif
    }
}
