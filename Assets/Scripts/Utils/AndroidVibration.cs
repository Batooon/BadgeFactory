using UnityEngine;

public static class AndroidVibration
{
#if UNITY_ANDROID && !UNITY_EDITOR
    private static AndroidJavaClass _unityPlayer = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
    private static AndroidJavaObject _currentActivity = unityPlayer.GetStatic<AndroidJavaObject>("currentActivity");
    private static AndroidJavaObject _vibrator = currentActivity.Call<AndroidJavaObject>("getSystemService", "vibrator");
#else
    private static AndroidJavaClass _unityPlayer;
    private static AndroidJavaObject _currentActivity;
    private static AndroidJavaObject _vibrator;
#endif

    public static void Vibrate()
    {
        if (IsAndroid())
            _vibrator.Call("vibrate");
        else
            Handheld.Vibrate();
    }


    public static void Vibrate(long milliseconds)
    {
        if (IsAndroid())
            _vibrator.Call("vibrate", milliseconds);
        else
            Handheld.Vibrate();
    }

    public static void Vibrate(long[] pattern, int repeat)
    {
        if (IsAndroid())
            _vibrator.Call("vibrate", pattern, repeat);
        else
            Handheld.Vibrate();
    }

    public static bool HasVibrator()
    {
        return IsAndroid();
    }

    public static void Cancel()
    {
        if (IsAndroid())
            _vibrator.Call("cancel");
    }

    private static bool IsAndroid()
    {
#if UNITY_ANDROID && !UNITY_EDITOR
	    return true;
#else
        return false;
#endif
    }
}
