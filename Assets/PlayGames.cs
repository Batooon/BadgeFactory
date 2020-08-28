using GooglePlayGames;
using GooglePlayGames.BasicApi;
using GooglePlayGames.BasicApi.SavedGame;
using System;
using UnityEngine;

//Developer: Antoshka

public class PlayGames : MonoBehaviour
{
    public static event Action<bool> LoggedIn;

    public const string DefaultFileName = "Save";
    private static ISavedGameClient _savedGameClient;
    private static ISavedGameMetadata _currentMetadata;
    private static CloudSavesUI _cloudUI;

    public static bool IsAuthenticated
    {
        get
        {
            if (PlayGamesPlatform.Instance != null)
                return PlayGamesPlatform.Instance.IsAuthenticated();
            return false;
        }
    }

    public static void UnlockAchievement(string id)
    {
        //Social.ReportProgress(id, 100,);
    }

    public static void IncrementAchievement(string id, int stepsToIncrement)
    {
        //PlayGamesPlatform.Instance.IncrementAchievement(id, stepsToIncrement, success => { 
    }

    public static void ShowAchievementsUI()
    {
        Social.ShowAchievementsUI();
    }

    public static void AddScoreToleaderBoard(long score)
    {
        Social.ReportScore(score, GPGSIds.leaderboard_level_leaders, (bool success)=>
        {
            if (success)
                Debug.Log("Posted successfuly!");
            else
                Debug.LogError("Can't post score to the leaderboard!");
        });
    }

    public static void ShowLeaderboardUI()
    {
        PlayGamesPlatform.Instance.ShowLeaderboardUI(GPGSIds.leaderboard_level_leaders);
    }

    public static void Initialize(bool debugMode)
    {
        PlayGamesClientConfiguration config = new PlayGamesClientConfiguration.Builder().EnableSavedGames().Build();
        PlayGamesPlatform.InitializeInstance(config);
        PlayGamesPlatform.DebugLogEnabled = debugMode;
        PlayGamesPlatform.Activate();
    }

    public static void Authenticate(Action<bool> success)
    {
        Social.localUser.Authenticate((bool authenticated) =>
        {
            if (authenticated)
                _savedGameClient = PlayGamesPlatform.Instance.SavedGame;
            Debug.Log(authenticated ?
                "Logged succsessfuly" :
                "Unable to Sign in in Google Play Games");

            LoggedIn?.Invoke(authenticated);
            success.Invoke(authenticated);
        });
    }

    public static void ShowSavesUI(Action<SavedGameRequestStatus, byte[]> callback, Action onDataCreated)
    {
        if (IsAuthenticated == false)
        {
            callback.Invoke(SavedGameRequestStatus.AuthenticationError, null);
            return;
        }

        _savedGameClient.ShowSelectSavedGameUI("Select saved game", _cloudUI.MaxDisplayCount, _cloudUI.AllowCreate, _cloudUI.AllowDelete, (status, metadata) =>
         {
             if (status == SelectUIStatus.SavedGameSelected && metadata != null)
             {
                 if (string.IsNullOrEmpty(metadata.Filename))
                     onDataCreated.Invoke();
                 else
                     ReadSavedData(metadata.Filename, callback);
             }
         });
    }

    public static void LogOut()
    {
        PlayGamesPlatform.Instance.SignOut();
    }

    public static void OpenSavedData(string filename, Action<SavedGameRequestStatus, ISavedGameMetadata> callback)
    {
        if (IsAuthenticated == false)
        {
            callback.Invoke(SavedGameRequestStatus.AuthenticationError, null);
            return;
        }

        ISavedGameClient savedGameClient = PlayGamesPlatform.Instance.SavedGame;
        savedGameClient.OpenWithAutomaticConflictResolution(filename,
                                                            DataSource.ReadCacheOrNetwork,
                                                            ConflictResolutionStrategy.UseLongestPlaytime,
                                                            callback);
    }

    public static void ReadSavedData(string filename, Action<SavedGameRequestStatus, byte[]> callback)
    {
        if (IsAuthenticated == false)
        {
            callback.Invoke(SavedGameRequestStatus.AuthenticationError, null);
            return;
        }

        OpenSavedData(filename, (status, metadata) =>
        {
             if (status == SavedGameRequestStatus.Success)
             {
                 _savedGameClient.ReadBinaryData(metadata, callback);
                 _currentMetadata = metadata;
             }
        });
    }

    public static void WriteSavedGame(byte[] savedData)
    {
        if (IsAuthenticated == false || savedData == null || savedData.Length == 0)
            return;

        Action OnDataWrited = () =>
        {
            SavedGameMetadataUpdate.Builder builder = new SavedGameMetadataUpdate.Builder()
                .WithUpdatedPlayedTime(TimeSpan.FromMinutes(_currentMetadata.TotalTimePlayed.Minutes + 1))
                .WithUpdatedDescription(string.Format("Saved at: {0}", DateTime.Now));

            //можно сохранить сриншот игры
            //byte[] pngData=<png as bytes>;
            //builder=builder.WithUpdatedPngCoverImage(pngData);

            SavedGameMetadataUpdate updateMetadata = builder.Build();

            ISavedGameClient savedGameClient = PlayGamesPlatform.Instance.SavedGame;
            savedGameClient.CommitUpdate(_currentMetadata, updateMetadata, savedData, (status, metadata) => _currentMetadata = metadata);
        };
        if (_currentMetadata == null)
        {
            OpenSavedData(DefaultFileName, (status, metadata) =>
            {
                 if (status == SavedGameRequestStatus.Success)
                 {
                     _currentMetadata = metadata;
                     OnDataWrited.Invoke();
                 }
            });
            return;
        }
        OnDataWrited.Invoke();
    }
}

[Serializable]
public struct CloudSavesUI
{
    public uint MaxDisplayCount { get; private set; }
    public bool AllowCreate { get; private set; }
    public bool AllowDelete { get; private set; }

    public CloudSavesUI(uint maxDisplayCount, bool allowCreate, bool allowDelete)
    {
        MaxDisplayCount = maxDisplayCount;
        AllowCreate = allowCreate;
        AllowDelete = allowDelete;
    }
}
