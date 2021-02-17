using System;
using System.IO;
using UnityEditor;
using UnityEngine;

public class TakeScreenshotInEditor : ScriptableObject
{
    [MenuItem("Custom/Take Screenshot")]
    private static void TakeScreenshot()
    {
        string folderPath = Path.Combine(Directory.GetCurrentDirectory(), @"Screenshots\");

        if (Directory.Exists(folderPath) == false)
            Directory.CreateDirectory(folderPath);

        var screenshotName =
            "Screenshot_" +
            DateTime.Now.ToString("dd-MM-yyyy-HH-mm-ss") +
            ".png";

        ScreenCapture.CaptureScreenshot(Path.Combine(folderPath, screenshotName));
        Debug.Log(folderPath + screenshotName);
    }
}
