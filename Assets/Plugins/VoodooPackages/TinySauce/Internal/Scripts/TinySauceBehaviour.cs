using System;
using UnityEngine;
using Voodoo.Sauce.Internal.Analytics;

namespace Voodoo.Sauce.Internal
{
    internal class TinySauceBehaviour : MonoBehaviour
    {
        private const string TAG = "TinySauce";
        private static TinySauceBehaviour _instance;
        private TinySauceSettings _sauceSettings;

        private void Awake()
        {
            if (transform != transform.root)
                throw new Exception("TinySauce prefab HAS to be at the ROOT level!");

            _sauceSettings = TinySauceSettings.Load();

            if (_sauceSettings == null)
                throw new Exception("Can't find TinySauce sauceSettings file.");
            
            if (_instance != null)
            {
                Destroy(gameObject);
                return;
            }

            _instance = this;
            DontDestroyOnLoad(this);
            
            VoodooLog.Initialize(VoodooLogLevel.WARNING);
            // init TinySauce sdk
            InitAnalytics();
        }

        private void InitAnalytics()
        {
            VoodooLog.Log(TAG, "Initializing Analytics");
            Debug.Log("Initializing analytics");
            AnalyticsManager.Initialize(_sauceSettings, true);
        }

        private void OnApplicationPause(bool pauseStatus)
        {
            // Brought forward after soft closing
            if (pauseStatus == false)
            {
                AnalyticsManager.OnApplicationResume();
            }
        }
    }
}