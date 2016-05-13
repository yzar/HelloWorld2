using UnityEngine;
using UnityEditor;
using System.Collections.Generic;

public class Builder
{
	[MenuItem ("Builds/ALL")]
	public static void All()
	{
			AllFull();
			AllFree();
	}

	[MenuItem ("Builds/Full/ALL")]
	public static void AllFull()
	{
		Windows("Full");
		MacOS("Full");
		Linux("Full");
	}

	[MenuItem ("Builds/Free/ALL")]
	public static void AllFree()
	{
		SaveCurrentDefine();
		try
		{
			AddDefine("FREE_VERSION");
			Windows("Free");
			MacOS("Free");
			Linux("Free");
			RestoreDefine();
		}
		catch(System.Exception e)
		{
			RestoreDefine();
			throw e;
		}

	}

	public static void AllCLI()
	{
		try
		{
			All();
		}
		catch(System.Exception)
		{
			EditorApplication.Exit(-1);
		}
	}

	public static void AllFullCLI()
	{
		try
		{
			AllFull();
		}
		catch(System.Exception)
		{
			EditorApplication.Exit(-1);
		}
	}

	public static void AllFreeCLI()
	{
		try
		{
			AllFree();
		}
		catch(System.Exception)
		{
			EditorApplication.Exit(-1);
		}
	}

	public static void Windows(string dir)
	{
		Build(dir, "Game.exe", BuildTarget.StandaloneWindows64);
		Build(dir, "Game.exe", BuildTarget.StandaloneWindows);
	}

	public static void MacOS(string dir)
	{
		Build(dir, "Game.app", BuildTarget.StandaloneOSXUniversal);
	}

	public static void Linux(string dir)
	{
		Build(dir, "Game", BuildTarget.StandaloneLinuxUniversal);
	}

	public static string[] GetEnableScenesName()
	{
		EditorBuildSettingsScene[] buildSettingsScenes = EditorBuildSettings.scenes;
		List<string> enableSceneName = new List<string>();
		foreach (EditorBuildSettingsScene s in buildSettingsScenes)
		if (s.enabled)
			enableSceneName.Add(s.path);
		return enableSceneName.ToArray();
	}

	public static void Build(string dir, string filename, BuildTarget target)
	{
		string path = Application.dataPath.Replace("/Assets", "/Export");
		path = path + "/" + dir + "/" + target.ToString();
		System.IO.Directory.CreateDirectory(path);
		Debug.Log("START_BUILD " + target.ToString());
		BuildPipeline.BuildPlayer(
			GetEnableScenesName(),
			path + "/" + filename,
			target,
			BuildOptions.None);
		Debug.Log("END_BUILD");
	}

	private static string define;
	private static void SaveCurrentDefine()
	{
		define = PlayerSettings.GetScriptingDefineSymbolsForGroup(BuildTargetGroup.Standalone);
	}

	private static void RestoreDefine()
	{
		PlayerSettings.SetScriptingDefineSymbolsForGroup(BuildTargetGroup.Standalone, define);
	}

	private static void AddDefine(string newDefine)
	{
		string currentDefine = PlayerSettings.GetScriptingDefineSymbolsForGroup(BuildTargetGroup.Standalone);
		currentDefine += ";" + newDefine;
		PlayerSettings.SetScriptingDefineSymbolsForGroup(BuildTargetGroup.Standalone, currentDefine);
	}
}
