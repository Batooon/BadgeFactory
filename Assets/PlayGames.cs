using GooglePlayGames;
using GooglePlayGames.BasicApi;
using System;
using UnityEngine;

//Developer: Antoshka

public class PlayGames : MonoBehaviour
{
    public static event Action<bool> LoggedIn;

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

    public static void AuthenticateUser(Action<bool> Success)
    {
        PlayGamesClientConfiguration config = new PlayGamesClientConfiguration.Builder().Build();
        PlayGamesPlatform.InitializeInstance(config);
        PlayGamesPlatform.Activate();

        Social.localUser.Authenticate((bool success) =>
        {
            Debug.Log(success ?
                "Logged succsessfuly" :
                "Unable to Sign in in Google Play Games");

            LoggedIn?.Invoke(success);
        });
    }

    public static void LogOut()
    {
        PlayGamesPlatform.Instance.SignOut();
    }
}
