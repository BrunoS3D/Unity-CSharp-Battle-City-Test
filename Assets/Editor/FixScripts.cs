#define UNITY_3_PLUS
#define UNITY_4_PLUS
#define UNITY_5_PLUS
#if UNITY_2_6
#define UNITY_2_X
#undef UNITY_3_PLUS
#undef UNITY_4_PLUS
#undef UNITY_5_PLUS
#elif UNITY_3_0 || UNITY_3_1 || UNITY_3_2 || UNITY_3_3 || UNITY_3_4 || UNITY_3_5
#define UNITY_3_X
#undef UNITY_4_PLUS
#undef UNITY_5_PLUS
#elif UNITY_4_0 || UNITY_4_1 || UNITY_4_2 || UNITY_4_3 || UNITY_4_4 || UNITY_4_5 || UNITY_4_6 || UNITY_4_7 || UNITY_4_8 || UNITY_4_9
#define UNITY_4_X
#undef UNITY_5_PLUS
#elif UNITY_5_0 || UNITY_5_1 || UNITY_5_2 || UNITY_5_3 || UNITY_5_4 || UNITY_5_5 || UNITY_5_6 || UNITY_5_7 || UNITY_5_8 || UNITY_5_9
#define UNITY_5_X
#endif

using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEditor;
using UnityEngine;

public class FixScripts : EditorWindow {

	private Vector2 scroll = Vector2.zero;
	private List<MonoScript> scripts = new List<MonoScript>();

	[MenuItem("Tools/FixScripts")]
	static void Init() {
		FixScripts window = CreateInstance<FixScripts>();
		#if (UNITY_5_X)
		window.titleContent = new GUIContent("FixScripts");
		#endif
		window.minSize = new Vector2(300.0f, 145.0f);
		window.Show();
	}

	private void OnEnable() {
		if (scripts.Count == 0 || scripts[scripts.Count - 1] != null) {
			scripts.Add(null);
		}
	}

	private void OnGUI() {
		Rect area = new Rect(20.0f, 20.0f, position.width - 40.0f, position.height - 40.0f);
		GUILayout.BeginArea(area);
		GUILayout.BeginVertical("HelpBox");
		GUILayout.Space(10.0f);
		GUILayout.BeginHorizontal();
		GUILayout.FlexibleSpace();
		GUI.enabled = scripts.Count > 1;

		if (GUILayout.Button("Clear", "minibutton")) {
			scripts.Clear();
		}

		GUI.enabled = true;
		GUILayout.EndHorizontal();
		GUILayout.BeginVertical("HelpBox");
		scroll = EditorGUILayout.BeginScrollView(scroll);

		if (scripts.Count == 0 || scripts[scripts.Count - 1] != null) {
			scripts.Add(null);
		}

		for (int id = 0; id < scripts.Count; id++) {
			GUILayout.BeginHorizontal(GUILayout.MaxHeight(EditorGUIUtility.singleLineHeight));
			GUI.changed = false;
			scripts[id] = (MonoScript)EditorGUILayout.ObjectField(scripts[id], typeof(MonoScript), true);
			GUILayout.FlexibleSpace();

			if (GUILayout.Button("", "OL Minus")) {
				scripts.RemoveAt(id);
			}

			GUILayout.EndHorizontal();
			GUILayout.Space(2.0f);
		}

		GUILayout.FlexibleSpace();
		EditorGUILayout.EndScrollView();
		GUILayout.EndVertical();
		GUILayout.Space(5.0f);
		GUI.enabled = scripts.Count > 1;

		if (GUILayout.Button("Fix it!", GUILayout.Height(30.0f))) {
			FixList();
		}

		GUI.enabled = true;
		GUILayout.Space(5.0f);
		GUILayout.EndVertical();
		GUILayout.EndArea();
	}

	public void FixList() {
		if (scripts.Count > 0) {
			foreach (MonoScript script in scripts) {
				if (script != null) {
					Fix(script);
				}
			}
		}
		AssetDatabase.Refresh();
	}

	public static void Fix(MonoScript script) {
		string path = Application.dataPath.Replace("Assets", "") + AssetDatabase.GetAssetPath(script);
		File.WriteAllText(path, script.text.Replace("\r\n", "\n").Replace("\n", "\r\n"));
	}
}

public class FixScriptsPostprocessor : AssetPostprocessor {
	private static void OnPostprocessAllAssets(string[] importedAssets, string[] deletedAssets, string[] movedAssets, string[] movedFromAssetPaths) {
				#if (UNITY_5_X)
				IEnumerable<MonoScript> assets = (from path in importedAssets.Union(movedAssets) where AssetDatabase.LoadAssetAtPath<Object>(path) is MonoScript select AssetDatabase.LoadAssetAtPath<Object>(path) as MonoScript);
				#else
				IEnumerable<MonoScript> assets = (from path in importedAssets.Union(movedAssets) where Resources.LoadAssetAtPath<Object>(path) is MonoScript select Resources.LoadAssetAtPath<Object>(path) as MonoScript);
				#endif
				if (assets.Count() > 0) {
			foreach (var asset in assets) {
				FixScripts.Fix(asset);
			}
			AssetDatabase.Refresh();
		}
	}
}