using UnityEngine;
using UnityEngine.Advertisements;

namespace Assets.Scripts
{
    public class InitializeAdsScript : MonoBehaviour
    {
        private string gameId = "3445518";
        private bool testMode = true;

        private void Awake()
        {
            Advertisement.Initialize(gameId, testMode);
        }
    }
}
