using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;
using UnityEditor;
using UnityEditor.Callbacks;
using UnityEngine;
using System.Runtime.InteropServices;
using Admix.AdmixCore;

namespace Admix.AdmixCore.WebViewDesktop
{
    /**
     * Getting CEF running on a build result requires some fiddling to get all the files in the right place.
     */
    class PostBuildStandalone
    {
        private static readonly List<string> ByBinFiles = new List<string>() {
        "natives_blob.bin",
        "snapshot_blob.bin",
        "v8_context_snapshot.bin",
        "icudtl.dat"
    };

        [PostProcessBuild(10)]
        // ReSharper disable once UnusedMember.Global
        public static void PostProcessLinuxBuild(BuildTarget target, string buildFile)
        {
            if (target != BuildTarget.StandaloneLinux64) return;
            //base info
            string buildType = "Linux";

            Debug.Log("ZFBrowser: Post processing " + buildFile + " as " + buildType);

            string buildName = Regex.Match(buildFile, @"\/([^\/]+?)(\.x86(_64)?)?$").Groups[1].Value;

            var buildPath = Directory.GetParent(buildFile);
            var dataPath = buildPath + "/" + buildName + "_Data";
            var pluginsPath = dataPath + "/Plugins/";

            //can't use FileLocations because we may not be building the same type as the editor
            var platformPluginsSrc = ZFFolder + "/AdmixAssets/Plugins/" + buildType;

            //(Unity will copy the .dll and .so files for us)

            //Copy "root" .bin files
            foreach (var file in ByBinFiles)
            {
                File.Copy(platformPluginsSrc + "/" + file, pluginsPath + file, true);
            }

            //Copy the needed resources
            var resSrcDir = platformPluginsSrc + "/CEFResources";
            foreach (var filePath in Directory.GetFiles(resSrcDir))
            {
                var fileName = new FileInfo(filePath).Name;
                if (fileName.EndsWith(".meta")) continue;

                File.Copy(filePath, pluginsPath + fileName, true);
            }

            //Slave process (doesn't get automatically copied by Unity like the shared libs)
            File.Copy(
                platformPluginsSrc + "/" + FileLocations.SlaveExecutable,
                pluginsPath + FileLocations.SlaveExecutable,
                true
            );
            MakeExecutable(pluginsPath + FileLocations.SlaveExecutable);

            //Locales
            var localesSrcDir = platformPluginsSrc + "/CEFResources/locales";
            var localesDestDir = dataPath + "/Plugins/locales";
            Directory.CreateDirectory(localesDestDir);
            foreach (var filePath in Directory.GetFiles(localesSrcDir))
            {
                var fileName = new FileInfo(filePath).Name;
                if (fileName.EndsWith(".meta")) continue;
                File.Copy(filePath, localesDestDir + "/" + fileName, true);
            }

            //Newer versions of Unity put the shared libs in the wrong place. Move them to where we expect them.
            if (File.Exists(pluginsPath + "x86_64/zf_cef.so"))
            {
                foreach (var libFile in new[] { "zf_cef.so", "libEGL.so", "libGLESv2.so", "libZFProxyWeb.so" })
                {
                    ForceMove(pluginsPath + "x86_64/" + libFile, pluginsPath + libFile);
                }
            }

            WriteBrowserAssets(dataPath + "/" + StandaloneWebResources.DefaultPath);
        }

        [PostProcessBuild(10)]
        // ReSharper disable once UnusedMember.Global
        public static void PostProcessWindowsBuild(BuildTarget target, string buildFile)
        {
            //prereq
            if (target != BuildTarget.StandaloneWindows && target != BuildTarget.StandaloneWindows64)
                return;

            //base info
            string buildType = "Win" + (target == BuildTarget.StandaloneWindows64 ? "64" : "32");

            string buildName = Regex.Match(buildFile, @"/([^/]+)\.exe$").Groups[1].Value;

            var buildPath = Directory.GetParent(buildFile);
            var dataPath = buildPath + "/" + buildName + "_Data";
            var pluginsPath = dataPath + "/Plugins/";

            //can't use FileLocations because we may not be building the same type as the editor
            var platformPluginsSrc = ZFFolder + "/AdmixAssets/Plugins/" + buildType;

            //(Unity will copy the .dll and .so files for us)

            //Copy "root" .bin files
            foreach (var file in ByBinFiles)
            {
                File.Copy(platformPluginsSrc + "/" + file, pluginsPath + file, true);
            }

            //Copy the needed resources
            var resSrcDir = platformPluginsSrc + "/CEFResources";
            foreach (var filePath in Directory.GetFiles(resSrcDir))
            {
                var fileName = new FileInfo(filePath).Name;
                if (fileName.EndsWith(".meta")) continue;

                File.Copy(filePath, pluginsPath + fileName, true);
            }

            //Slave process (doesn't get automatically copied by Unity like the shared libs)
            var exeExt = ".exe";
            File.Copy(
                platformPluginsSrc + "/" + FileLocations.SlaveExecutable + exeExt,
                pluginsPath + FileLocations.SlaveExecutable + exeExt,
                true
            );

            //Locales
            var localesSrcDir = platformPluginsSrc + "/CEFResources/locales";
            var localesDestDir = dataPath + "/Plugins/locales";
            Directory.CreateDirectory(localesDestDir);
            foreach (var filePath in Directory.GetFiles(localesSrcDir))
            {
                var fileName = new FileInfo(filePath).Name;
                if (fileName.EndsWith(".meta")) continue;
                File.Copy(filePath, localesDestDir + "/" + fileName, true);
            }

            WriteBrowserAssets(dataPath + "/" + StandaloneWebResources.DefaultPath);
        }

        [PostProcessBuild(10)]
        // ReSharper disable once UnusedMember.Global
        // ReSharper disable once InconsistentNaming
        public static void PostProcessOSXBuild(BuildTarget target, string buildFile)
        {
            if (target != BuildTarget.StandaloneOSX) return;

            var buildPath = buildFile;
            var platformPluginsSrc = ZFFolder + "/AdmixAssets/Plugins/OSX";

            //Copy app bits
            CopyDirectory(
                platformPluginsSrc + "/BrowserLib.app/Contents/Frameworks/Chromium Embedded Framework.framework",
                buildPath + "/Contents/Frameworks/Chromium Embedded Framework.framework"
            );
            CopyDirectory(
                platformPluginsSrc + "/BrowserLib.app/Contents/Frameworks/ZFGameBrowser.app",
                buildPath + "/Contents/Frameworks/ZFGameBrowser.app"
            );

            MakeExecutable(buildPath + "/BrowserLib.app/Contents/Frameworks/ZFGameBrowser.app/Contents/MacOS/ZFGameBrowser");

            if (!Directory.Exists(buildPath + "/Contents/Plugins")) Directory.CreateDirectory(buildPath + "/Contents/Plugins");
            File.Copy(platformPluginsSrc + "/libZFProxyWeb.dylib", buildPath + "/Contents/Plugins/libZFProxyWeb.dylib", true);

            //BrowserAssets
            WriteBrowserAssets(buildPath + "/Contents/" + StandaloneWebResources.DefaultPath);
        }


        private static void WriteBrowserAssets(string path)
        {
            var htmlDir = Application.dataPath + "/../BrowserAssets";
            var allData = new Dictionary<string, byte[]>();
            if (Directory.Exists(htmlDir))
            {
                foreach (var file in Directory.GetFiles(htmlDir, "*", SearchOption.AllDirectories))
                {
                    var localPath = file.Substring(htmlDir.Length).Replace("\\", "/");
                    allData[localPath] = File.ReadAllBytes(file);
                }
            }

            var wr = new StandaloneWebResources(path);
            wr.WriteData(allData);
        }

        private static void ForceMove(string src, string dest)
        {
            if (File.Exists(dest)) File.Delete(dest);
            File.Move(src, dest);
        }

        private static string ZFFolder
        {
            get
            {
                var path = new System.Diagnostics.StackTrace(true).GetFrame(0).GetFileName();
                path = Directory.GetParent(path).Parent.Parent.FullName;
                return path;
            }
        }

        private static void CopyDirectory(string src, string dest)
        {
            foreach (var dir in Directory.GetDirectories(src, "*", SearchOption.AllDirectories))
            {
                Directory.CreateDirectory(dir.Replace(src, dest));
            }

            foreach (var file in Directory.GetFiles(src, "*", SearchOption.AllDirectories))
            {
                if (file.EndsWith(".meta")) continue;
                File.Copy(file, file.Replace(src, dest), true);
            }
        }

        private static void MakeExecutable(string fileName)
        {
#if UNITY_EDITOR_WIN
            Debug.LogWarning("Be sure to mark the file \"" + fileName + "\" as executable (chmod +x) when you distribute it. If it's not executable the browser won't work.");
#else
            //dec 493 = oct 755 = -rwxr-xr-x
            chmod(fileName, 493);
#endif
        }

        [DllImport("__Internal")] static extern int symlink(string destStr, string symFile);
        [DllImport("__Internal")] static extern int chmod(string file, int mode);
    }
}