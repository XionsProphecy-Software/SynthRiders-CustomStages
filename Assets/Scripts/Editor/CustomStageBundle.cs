using UnityEditor;
using System.IO;
using System.Linq;
using UnityEngine;
using UnityEngine.XR;
using System;
using System.Collections;
using System.Collections.Generic;

public class CustomStageBundle : EditorWindow
{
    private const string AssetsPath = "Assets/_CustomStageElements";
    private string ExportPath = "Build";
    private static string BundleName { get; set; }
    private bool IsForSpinMode { get; set; }

    [MenuItem("SynthRiders/Export Stage Bundle")]
    static void ExportCustomStageBundle()
    {
        // AssetImporter.GetAtPath(AssetsPath).;
        CustomStageBundle window = ScriptableObject.CreateInstance<CustomStageBundle>();
        window.titleContent = new GUIContent("Export Settings");
        window.position = new Rect(Screen.width / 2, Screen.height / 2, 500, 200);
        window.Show();

    }

    void OnGUI()
    {
        GUILayout.Space(20);
        ExportPath = EditorGUILayout.TextField("Export Path: ", ExportPath);
        GUILayout.Space(10);
        BundleName = EditorGUILayout.TextField("Custom Stage Name: ", BundleName);
        GUILayout.Space(10);
        IsForSpinMode = EditorGUILayout.Toggle("For spin mode?", IsForSpinMode);
        
        GUILayout.Space(70);
        if (GUILayout.Button("Export PC")) {
            if(BundleName == null || BundleName.Equals(string.Empty)) {
                Debug.LogError("Please set the bundle name");
                EditorUtility.DisplayDialog("Custom Stage Name Empty", "The Custom Stage name must not be empty", "Ok");

            } else {
                Debug.Log($"Bundle Name set to {BundleName}");

                string extention = "stage";
                if(IsForSpinMode) {
                    extention = "spinstage";
                }
                string sanitizeBundleName = SanitizeString(BundleName);
                AssetImporter.GetAtPath(AssetsPath).SetAssetBundleNameAndVariant($"{sanitizeBundleName}.{extention}", "");
                string bundleExportPath = $"{Application.dataPath}/../{ExportPath}/{sanitizeBundleName}/";

                if(!Directory.Exists(bundleExportPath))
                {
                    Directory.CreateDirectory(bundleExportPath);
                } 

                BuildPipeline.BuildAssetBundles(bundleExportPath, BuildAssetBundleOptions.None, BuildTarget.StandaloneWindows);
                EditorUtility.RevealInFinder($"{bundleExportPath}");
                Debug.Log($"Export complete, to directory {bundleExportPath}");
                this.Close();
            }             
        }

        if (GUILayout.Button("Export Quest")) {
            if(BundleName == null || BundleName.Equals(string.Empty)) {
                Debug.LogError("Please set the bundle name");
                EditorUtility.DisplayDialog("Custom Stage Name Empty", "The Custom Stage name must not be empty", "Ok");
            } else {
                Debug.Log($"Bundle Name set to {BundleName}");

                string extention = "stagequest";
                if(IsForSpinMode) {
                    extention = "spinstagequest";
                }
                string sanitizeBundleName = SanitizeString(BundleName);

                AssetImporter.GetAtPath(AssetsPath).SetAssetBundleNameAndVariant($"{sanitizeBundleName}.{extention}", "");
                string bundleExportPath = $"{Application.dataPath}/../{ExportPath}/{sanitizeBundleName}/";

                //Check if Android Build support is install before exporting so that people
                //do not get confused when it doesn't export when Android SDK isn't installed
                //Debug.Log(BuildPipeline.IsBuildTargetSupported(BuildTargetGroup.Android, BuildTarget.Android));
                if (BuildPipeline.IsBuildTargetSupported(BuildTargetGroup.Android, BuildTarget.Android) == true)
                {
                    Debug.Log("Andriod is Supported meaning Android SDK is INSTALLED!");
                    if (!Directory.Exists(bundleExportPath))
                    {
                        Directory.CreateDirectory(bundleExportPath);
                    }
                    try
                    {
                        BuildPipeline.BuildAssetBundles(bundleExportPath, BuildAssetBundleOptions.None, BuildTarget.Android);
                        EditorUtility.RevealInFinder($"{bundleExportPath}");
                        Debug.Log($"Export complete, to directory {bundleExportPath}");
                    }
                    catch (Exception E)
                    {
                        Debug.LogError("FAILED TO EXPORT! Caused by: " + E);
                    }
                }
                else
                {
                    Debug.LogError("ANDROID SDK NOT INSTALLED!");
                    EditorUtility.DisplayDialog("EXPORT FAILED!", "Exporting to Quest failed because the Android SDK is not installed! Install it from Unity Hub and restart the editor to continue", "Ok");
                }
                
                
                this.Close();
            }             
        }

        if (GUILayout.Button("Cancel")) {
            this.Close();           
        }
        this.Repaint();
    }

    private static string SanitizeString(string dirtyString)
	{
		return new String(dirtyString.Where(Char.IsLetterOrDigit).ToArray());
	}
}
