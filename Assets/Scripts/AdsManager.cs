using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Advertisements;
using UnityEngine.Events;

//Developer: Antoshka

public class AdsManager : MonoBehaviour, IUnityAdsListener
{
    public static AdsManager Instance;

    [SerializeField] private BannerPosition _bannerPosition;

#if UNITY_EDITOR
    public bool _testMode = true;
#else
    public bool _testMode = false;
#endif

#if UNITY_ANDROID
    private readonly string _storeId = "3445518";
#elif UNITY_IOS
    private readonly string _storeId = "3445519";
#endif
    private readonly string _rewardedVideoPlacement = "rewardedVideo";
    private readonly string _bannerPlacement = "BottomBanner";
    private readonly string _adVideoPlacement = "video";
    private UnityEvent _rewardedAdFinished;

    public void Init()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }

        Advertisement.AddListener(this);
        Advertisement.Initialize(_storeId, _testMode);
    }

    public void ShowRewardedAd(UnityEvent adFinished)
    {
        if (Advertisement.IsReady(_rewardedVideoPlacement))
        {
            Instance._rewardedAdFinished = adFinished;
            Advertisement.Show(_rewardedVideoPlacement);
        }
    }

    public void ShowBanner()
    {
        StartCoroutine(ActivateBanner());
    }

    public void HideBanner()
    {
        Advertisement.Banner.Hide();
    }

    public void OnUnityAdsDidFinish(string placementId, ShowResult showResult)
    {
        if (placementId == _rewardedVideoPlacement)
        {
            if (showResult == ShowResult.Finished)
            {
                _rewardedAdFinished?.Invoke();
            }
        }
    }

    public void OnUnityAdsReady(string placementId) { }
    public void OnUnityAdsDidStart(string placementId) { }
    public void OnUnityAdsDidError(string message) { }

    private IEnumerator ActivateBanner()
    {
        while (Advertisement.IsReady(_bannerPlacement) == false)
            yield return new WaitForSeconds(.5f);

        Advertisement.Banner.SetPosition(_bannerPosition);
        Advertisement.Banner.Show(_bannerPlacement);
    }
}
