using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Advertisements;

//Developer: Antoshka

public class AdsManager : MonoBehaviour, IUnityAdsListener
{

    private string _placemet = "rewardedVideo";

    public void ShowAd(string _placementId)
    {
        Advertisement.Show(_placementId);
    }

    public void OnUnityAdsDidError(string message)
    {
    }

    public void OnUnityAdsDidFinish(string placementId, ShowResult showResult)
    {
        if (showResult == ShowResult.Finished)
        {

        }
    }

    public void OnUnityAdsDidStart(string placementId)
    {
    }

    public void OnUnityAdsReady(string placementId)
    {
    }

    private void Start()
    {
        Advertisement.AddListener(this);
        Advertisement.Initialize("3445518", true);
    }
}
