using System.IO;
using UnityEngine;
using UnityEditor;
using UnityEditor.Build;
using UnityEditor.Build.Reporting;
using Facebook.Unity.Editor;
using GooglePlayServices;

namespace Voodoo.Sauce.Internal.Editor
{
    public class AndroidPrebuild : IPreprocessBuildWithReport
    {
        private const string SourceFolderPath = "VoodooPackages/TinySauce/Internal/Android";
        private static readonly string SourceManifestPath = $"{SourceFolderPath}/AndroidManifest.xml";
        private static readonly string SourceGradlePath = $"{SourceFolderPath}/mainTemplate.gradle";

        private const string PluginFolderPath = "Plugins";
        private const string AndroidFolderPath = "Plugins/Android";
        private static readonly string DestManifestPath = $"{AndroidFolderPath}/AndroidManifest.xml";
        private static readonly string DestGradlePath = $"{AndroidFolderPath}/mainTemplate.gradle";

        public int callbackOrder => 0;

        public void OnPreprocessBuild(BuildReport report)
        {
            if (report.summary.platform != BuildTarget.Android) {
                return;
            }

            CreateAndroidFolder();
            UpdateManifest();
            UpdateGradle();
            PreparePlayerSettings();
            PrepareResolver();
        }

        private static void PreparePlayerSettings()
        {
            // Set Android ARM64/ARMv7 Architecture   
            PlayerSettings.SetScriptingBackend(EditorUserBuildSettings.selectedBuildTargetGroup, ScriptingImplementation.IL2CPP);
            PlayerSettings.Android.targetArchitectures = AndroidArchitecture.ARMv7 | AndroidArchitecture.ARM64;
            // Set Android min version
            if (PlayerSettings.Android.minSdkVersion < AndroidSdkVersions.AndroidApiLevel19) {
                PlayerSettings.Android.minSdkVersion = AndroidSdkVersions.AndroidApiLevel19;
            }
        }

        private static void PrepareResolver()
        {
            // Force playServices Resolver
            PlayServicesResolver.Resolve(null, true);
        }

        private static void CreateAndroidFolder()
        {
            string pluginPath = Path.Combine(Application.dataPath, PluginFolderPath);
            string androidPath = Path.Combine(Application.dataPath, AndroidFolderPath);
            if (!Directory.Exists(pluginPath))
                Directory.CreateDirectory(pluginPath);
            if (!Directory.Exists(androidPath))
                Directory.CreateDirectory(androidPath);
        }

        private static void UpdateManifest()
        {
            string sourcePath = Path.Combine(Application.dataPath, SourceManifestPath);
            string content = File.ReadAllText(sourcePath)
#if UNITY_2019_3_OR_NEWER
                                 .Replace("attribute='**APPLICATION_ATTRIBUTES**'", string.Empty);
#else
                                 .Replace("attribute='**APPLICATION_ATTRIBUTES**'", "android:icon=\"@mipmap/app_icon\" android:label=\"@string/app_name\"");
#endif
            string destPath = Path.Combine(Application.dataPath, DestManifestPath);
            File.Delete(destPath);
            File.WriteAllText(destPath, content);
            //Add Facebook Manifest to  application manifest
            ManifestMod.GenerateManifest();
        }

        private static void UpdateGradle()
        {
            string sourcePath = Path.Combine(Application.dataPath, SourceGradlePath);
            string content = File.ReadAllText(sourcePath)
#if UNITY_2019_3_OR_NEWER
                                 .Replace("**BUILD_SCRIPT_DEPS**", string.Empty)
                                 .Replace("**APPLY_PLUGINS**", "apply plugin: 'com.android.library'")
                                 .Replace("**APPLICATIONID**", string.Empty);
#else
                                 .Replace("**BUILD_SCRIPT_DEPS**", "classpath 'com.android.tools.build:gradle:3.4.2'")
                                 .Replace("**APPLY_PLUGINS**", "apply plugin: 'com.android.application'")
                                 .Replace("**APPLICATIONID**", "applicationId '**APPLICATIONID**'");
#endif

            string destPath = Path.Combine(Application.dataPath, DestGradlePath);
            File.Delete(destPath);
            File.WriteAllText(destPath, content);
        }
    }
}