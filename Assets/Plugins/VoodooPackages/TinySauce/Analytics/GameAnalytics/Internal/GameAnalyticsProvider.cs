using System.Collections.Generic;
using GameAnalyticsSDK;

namespace Voodoo.Sauce.Internal.Analytics
{
    internal class GameAnalyticsProvider : IAnalyticsProvider
    {
        private const string TAG = "GameAnalyticsProvider";
        
        internal GameAnalyticsProvider()
        {
            RegisterEvents();
        }

        public void Initialize(bool consent)
        {
            if (!GameAnalyticsWrapper.Initialize(consent)) {
                UnregisterEvents();
            }
        }

        private static void RegisterEvents()
        {
            AnalyticsManager.OnGameStartedEvent += OnGameStarted;
            AnalyticsManager.OnGameFinishedEvent += OnGameFinished;
            AnalyticsManager.OnTrackCustomValueEvent += TrackCustomEvent;
            AnalyticsManager.OnTrackCustomEvent += TrackCustomEvent;
        }

        private static void UnregisterEvents()
        {
            AnalyticsManager.OnGameStartedEvent -= OnGameStarted;
            AnalyticsManager.OnGameFinishedEvent -= OnGameFinished;
            AnalyticsManager.OnTrackCustomValueEvent -= TrackCustomEvent;
            AnalyticsManager.OnTrackCustomEvent -= TrackCustomEvent;
        }
        
        private static void OnGameStarted(string levelNumber)
        {
            GameAnalyticsWrapper.TrackProgressEvent(GAProgressionStatus.Start, levelNumber, null);
        }

        private static void OnGameFinished(bool levelComplete, float score, string levelNumber, Dictionary<string, object> eventProperties)
        {
            GameAnalyticsWrapper.TrackProgressEvent(levelComplete ? GAProgressionStatus.Complete : GAProgressionStatus.Fail, levelNumber, (int) score);
        }

        private static void TrackCustomEvent(string eventName)
        {
            GameAnalyticsWrapper.TrackDesignEvent(eventName, null);
        }
        private static void TrackCustomEvent(string eventName, float value)
        {
            GameAnalyticsWrapper.TrackDesignEvent(eventName, value);
        }
        
        
    }
}