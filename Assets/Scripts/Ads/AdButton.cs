using UnityEngine;
using UnityEngine.Advertisements;
using UnityEngine.UI;

//Developer: Antoshka

public class AdButton : MonoBehaviour,IUnityAdsListener
{
    private Button _adsButton;
    public string PlacementId = "rewardedVideo";

    public void OnUnityAdsDidError(string message)
    {

    }

    public void OnUnityAdsDidFinish(string placementId, ShowResult showResult)
    {
        if (showResult == ShowResult.Finished)
        {

        }
        else if (showResult == ShowResult.Skipped)
        {

        }
        else
            Debug.LogWarning("The ad did not finisf due to an error");
    }

    public void OnUnityAdsDidStart(string placementId)
    {

    }

    public void OnUnityAdsReady(string placementId)
    {
        if (PlacementId == placementId)
            _adsButton.interactable = true;
    }

    private void Start()
    {
        _adsButton = GetComponent<Button>();
        _adsButton.interactable = Advertisement.IsReady(PlacementId);

        if (_adsButton)
            _adsButton.onClick.AddListener(ShowRewardedVideo);

        Advertisement.AddListener(this);
    }

    private void ShowRewardedVideo()
    {
        Advertisement.Show(PlacementId);
    }
}
