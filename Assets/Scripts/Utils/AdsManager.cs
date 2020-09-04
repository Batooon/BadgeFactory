using System.Collections;
using UnityEngine;
using UnityEngine.Advertisements;
using UnityEngine.Events;
using GoogleMobileAds.Api;
using GoogleMobileAds.Placement;

//Developer: Antoshka

public class AdsManager : MonoBehaviour, IUnityAdsListener
{
    public static AdsManager Instance;

    [SerializeField] private BannerPosition _bottomBannerPosition;
    [SerializeField] private BannerAdGameObject _banner;

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
    private readonly string _bottomBannerPlacement = "BottomBanner";
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
        MobileAds.Initialize((initStatus) =>
        {
            Debug.Log("Google ads initialization completed");
        });
    }

    private void Start()
    {
        StartCoroutine(ActivateBanners());
        _banner.LoadAd();
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
        if (Advertisement.IsReady(_bottomBannerPlacement)) 
            Advertisement.Banner.Show(_bottomBannerPlacement);
        //_banner.LoadAd();
        _banner.Show();
    }

    public void HideBanner()
    {
        Advertisement.Banner.Hide();
    }

    public void HideBottomBanner()
    {
        _banner.Hide();
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

    private IEnumerator ActivateBanners()
    {
        while (Advertisement.IsReady(_bottomBannerPlacement) == false)
            yield return new WaitForSeconds(.5f);

        Advertisement.Banner.SetPosition(_bottomBannerPosition);
        Advertisement.Banner.Hide();
    }
}
