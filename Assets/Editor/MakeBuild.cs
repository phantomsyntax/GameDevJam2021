#if UNITY_EDITOR || UNITY_EDITOR_64
using UnityEditor;
#endif
using UnityEditor.Build.Reporting;
using UnityEngine;

public class MakeBuild : MonoBehaviour {
    private static readonly string[] SceneArray =
        {"Assets/PhantomSyntax/Scenes/00_PhantomSyntax/00_PhantomSyntax.unity",
            "Assets/PhantomSyntax/Scenes/01_MainMenu/01_MainMenu.unity",
            "Assets/PhantomSyntax/Scenes/03_RainBro/03_RainBro.unity"};
    
    [MenuItem("Build/Standalone Linux x64")]
    static void Linux() {
        BuildPlayerOptions buildPlayerOptions = new BuildPlayerOptions();
        buildPlayerOptions.scenes = SceneArray;
        // TODO: pull project/title name by code
        buildPlayerOptions.locationPathName = "Builds/Linux/GlitterSkate";
        buildPlayerOptions.target = BuildTarget.StandaloneLinux64;
        buildPlayerOptions.options = BuildOptions.None;

        BuildReport report = BuildPipeline.BuildPlayer(buildPlayerOptions);
        BuildSummary summary = report.summary;
        
        if (summary.result == BuildResult.Succeeded) {
            Debug.Log("Build succeeded: " + summary.totalSize + " bytes");
        }

        if (summary.result == BuildResult.Failed) {
            Debug.Log("Build failed");
            foreach (var buildStep in report.steps) {
                Debug.Log(buildStep.ToString());
            }
        }
    }

    [MenuItem("Build/Standalone Windows x64")]
    static void Windows() {
        BuildPlayerOptions buildPlayerOptions = new BuildPlayerOptions();
        buildPlayerOptions.scenes = SceneArray;
        buildPlayerOptions.locationPathName = "Builds/Windows/GlitterSkate.exe";
        buildPlayerOptions.target = BuildTarget.StandaloneWindows64;
        buildPlayerOptions.options = BuildOptions.None;

        BuildReport report = BuildPipeline.BuildPlayer(buildPlayerOptions);
        BuildSummary summary = report.summary;

        if (summary.result == BuildResult.Succeeded) {
            Debug.Log("Build succeeded: " + summary.totalSize + " bytes");
        }

        if (summary.result == BuildResult.Failed) {
            Debug.Log("Build failed");
            foreach (var buildStep in report.steps) {
                Debug.Log(buildStep.ToString());
            }
        }
    }
}