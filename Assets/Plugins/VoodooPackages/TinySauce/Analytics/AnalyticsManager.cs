using System;
using System.Collections.Generic;
using Facebook.Unity;
using UnityEngine;

namespace Voodoo.Sauce.Internal.Analytics
{
    internal static class AnalyticsManager
    {
        private const string NO_GAME_LEVEL = "game";
        
        internal static event Action<string> OnGameStartedEvent;

        internal static event Action<bool, float, string, Dictionary<string, object>> OnGameFinishedEvent;
        
        internal static event Action<string, float> OnTrackCustomValueEvent;
        
        internal static event Action<string> OnTrackCustomEvent;
        
        internal static event Action OnApplicationResumeEvent;

        private static readonly List<IAnalyticsProvider> _analyticsProviders = new List<IAnalyticsProvider>()
        {
            new GameAnalyticsProvider()
        };

        internal static void Initialize(TinySauceSettings sauceSettings, bool consent)
        {
            // Initialize providers
            _analyticsProviders.ForEach(provider => provider.Initialize( consent));

            FB.Init();
        }

        internal static void OnGameStarted(string levelNumber)
        {
            OnGameStartedEvent?.Invoke(levelNumber ?? NO_GAME_LEVEL);
        }

        internal static void OnGameFinished(bool levelComplete, float score, string levelNumber, Dictionary<string, object> eventProperties)
        {
            OnGameFinishedEvent?.Invoke(levelComplete, score, levelNumber ?? NO_GAME_LEVEL, eventProperties);
        }

        internal static void TrackCustomEvent(string eventName)
        {
            OnTrackCustomEvent?.Invoke(eventName);
        }
        internal static void TrackCustomEvent(string eventName, float value)
        {
            OnTrackCustomValueEvent?.Invoke(eventName, value);
        }
        
        internal static void OnApplicationResume()
        {
            OnApplicationResumeEvent?.Invoke();
        }

        private static void ActivateFacebook()
        {
            //Init Facebook
            Debug.Log("Initializing Facebook");

            if (FB.IsInitialized)
                FB.ActivateApp();
            else
                FB.Init(() => FB.ActivateApp());
        }
    }
}