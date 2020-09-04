using UnityEditor;
using System.IO;
using UnityEditor.Callbacks;
using Admix.AdmixCore.Editor;

#if UNITY_IOS
using UnityEditor.iOS.Xcode;
#endif

namespace Admix.WebView
{
    public class AdmixBuildPostProcessor
    {
        [PostProcessBuild(700)]
        public static void OnPostProcessBuild(BuildTarget target, string pathToBuiltProject)
        {
            MaterialBuildHelper.TurnOnPlacementsTextures();
#if UNITY_IOS

            if (target != BuildTarget.iOS)
            {
                return;
            }
            string projPath = pathToBuiltProject + "/Unity-iPhone.xcodeproj/project.pbxproj";
            var proj = new PBXProject();
            proj.ReadFromString(File.ReadAllText(projPath));
            string targetGuid = proj.TargetGuidByName("Unity-iPhone");
            proj.AddBuildProperty(targetGuid, "OTHER_LDFLAGS", "-ObjC");
            File.WriteAllText(projPath, proj.WriteToString());
#endif
        }
    }
}
