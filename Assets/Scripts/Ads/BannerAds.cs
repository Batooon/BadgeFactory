using UnityEngine;
using UnityEngine.Advertisements;
using System.Collections;

public class BannerAds : MonoBehaviour
{
    public string PlacementId = "BottomBanner";

    private void Start()
    {
        Advertisement.Banner.SetPosition(BannerPosition.BOTTOM_CENTER);
        StartCoroutine(ShowBannerWhenReady());
    }

    IEnumerator ShowBannerWhenReady()
    {
        while (!Advertisement.IsReady(PlacementId))
            yield return new WaitForSeconds(.5f);

        Advertisement.Banner.Show(PlacementId);
    }
}
