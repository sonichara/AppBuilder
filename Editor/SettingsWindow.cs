using UnityEditor;
using UnityEngine;
using System.Collections.Generic;


namespace AppBuilder {
	public class SettingsWindow : EditorWindow {
		private Dictionary<BuildTarget, string> _targets;
		private const string Prefix = "AppBuilder_";

		public static SettingsWindow instance;

		public static void ShowSettings() {
			instance = (SettingsWindow)EditorWindow.GetWindow (typeof(SettingsWindow));
			instance.titleContent = new GUIContent ("AppBuilder");
		}

		private void OnEnable() {
			InitTargets ();
		}

		private void InitTargets() {
			_targets = new Dictionary<BuildTarget, string> ();
			_targets.Add (BuildTarget.StandaloneWindows, "Windows");
			_targets.Add (BuildTarget.StandaloneLinux, "Linux");
			_targets.Add (BuildTarget.StandaloneOSXIntel, "MacOS");
			_targets.Add (BuildTarget.Android, "Android");
		}

		private void DrawPlatformToggle(BuildTarget target, string label) {
			string key = Prefix + target.ToString ();
			bool currentValue = EditorPrefs.GetBool (key, false);
			EditorPrefs.SetBool (key, GUILayout.Toggle (currentValue, label));
		}

		private bool GetPlatformToggleValue(BuildTarget target) {
			string key = Prefix + target.ToString ();
			return EditorPrefs.GetBool (key, false);
		}

		private void OnGUI() {
			DrawPlatformsGUI ();
			DrawButtonsGUI ();
		}

		private void DrawPlatformsGUI() {
			EditorGUILayout.LabelField ("Platforms", EditorStyles.boldLabel);
			EditorGUILayout.BeginVertical ("box");
			foreach (KeyValuePair<BuildTarget, string> entry in _targets) {
				DrawPlatformToggle (entry.Key, entry.Value);
			}
			EditorGUILayout.EndVertical ();
		}

		private void DrawButtonsGUI() {
			if (GUILayout.Button ("Build", GUILayout.Height (40))) {
				StartBuildProcess ();
			}
		}

		private void StartBuildProcess() {
			Builder.CreateBuildFolder ();
			foreach (KeyValuePair<BuildTarget, string> entry in _targets) {
				if (GetPlatformToggleValue (entry.Key)) {
					Builder.Build (entry.Key, "build");
				}
			}
			EditorUtility.DisplayDialog ("AppBuilder", "Build process has finished!", "OK");
		}
	
	}
}
