using System.IO;
using UnityEditor;
using UnityEngine;

public class NativeSettings : ScriptableObject
{
	private static NativeSettings instance;

	[SerializeField] private bool isPostProcessingEnabled = false;
	
	private static string nativeSDKPath;
	private static readonly string SettingsPath = NativeSdkPath + "/NativeSettings.asset";

	public static string NativeSdkPath
	{
		get
		{
			if (string.IsNullOrEmpty(nativeSDKPath))
			{
				nativeSDKPath = FindFolderPath("Assets", "NativeSDKTest");
			}

			return nativeSDKPath;
		}
	}

	public static NativeSettings Instance
	{
		get
		{
			var instance = AssetDatabase.LoadAssetAtPath<NativeSettings>(SettingsPath);
			if (instance == null)
			{
				instance = CreateInstance<NativeSettings>();
				AssetDatabase.CreateAsset(instance, SettingsPath);
				AssetDatabase.SaveAssets();
			}
			return instance;
		}
	}

	private static string FindFolderPath(string parentFolder, string searchFolder)
	{
		string resultFolder = parentFolder + "/" + searchFolder;
		
		if (!Directory.Exists(resultFolder))
		{
			Directory.CreateDirectory(parentFolder + "/" + searchFolder);
		}

		return resultFolder;
	}

	public static bool IsPostProcessingEnabled
	{
		get
		{
			return Instance.isPostProcessingEnabled;
		}
	}
}

	
