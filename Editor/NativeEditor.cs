using System.IO;
using System.Text.RegularExpressions;
using System.Xml;
using UnityEditor;
using UnityEditor.Callbacks;
using UnityEditor.iOS.Xcode;
using UnityEngine;

public class NativeEditor
{
	[MenuItem("NativeSDK/Settings")]
	public static void OpenSettings()
	{
		Selection.activeObject = NativeSettings.Instance;
	}
	
	[PostProcessBuild]
	public static void OnPostprocessBuild(BuildTarget target, string projectPath)
	{
		if (!NativeSettings.IsPostProcessingEnabled)
		{
			UnityEngine.Debug.Log("You have forbidden the SDK to perform post processing tasks.");
			
			return;
		}

		RunPostBuildScript(target:target, preBuild:false, projectPath:projectPath);
	}

	private static void RunPostBuildScript(BuildTarget target, bool preBuild, string projectPath)
	{
		if (target == BuildTarget.Android)
		{
			UnityEngine.Debug.Log("[NativeSDK]: Starting to perform post build tasks for Android platform.");
			RunPostProcessTasksAndroid();
		}
		else if (target == BuildTarget.iOS)
		{
#if UNITY_IOS			
			UnityEngine.Debug.Log("Starting to perform post build tasks for iOS platform.");
            
			string xcodeProjectPath = projectPath + "/Unity-iPhone.xcodeproj/project.pbxproj";

			PBXProject xcodeProject = new PBXProject();
			xcodeProject.ReadFromFile(xcodeProjectPath);
			
#if UNITY_2019_3_OR_NEWER
            string xcodeTarget = xcodeProject.GetUnityFrameworkTargetGuid();
#else
			string xcodeTarget = xcodeProject.TargetGuidByName("Unity-iPhone");
#endif
			
			UnityEngine.Debug.Log("Adding Speech.framework to Xcode project.");
			xcodeProject.AddFrameworkToProject(xcodeTarget, "Speech.framework", true);
			UnityEngine.Debug.Log("Speech.framework added successfully.");
			
			UnityEngine.Debug.Log("Adding StoreKit.framework to Xcode project.");
			xcodeProject.AddFrameworkToProject(xcodeTarget, "StoreKit.framework", true);
			UnityEngine.Debug.Log("StoreKit.framework added successfully.");

			UnityEngine.Debug.Log("Adding AppTrackingTransparency.framework to Xcode project.");
			xcodeProject.AddFrameworkToProject(xcodeTarget, "AppTrackingTransparency.framework", true);
			UnityEngine.Debug.Log("ppTrackingTransparency.framework added successfully.");
			
			UpdateIOSPlist(projectPath);
#endif
		}
	}

	private static void UpdateIOSPlist(string path)
	{
#if UNITY_IOS		
		string plistPath = Path.Combine(path, "Info.plist"); 
		PlistDocument pPlistDocument = new PlistDocument();
		pPlistDocument.ReadFromString(File.ReadAllText(plistPath));
		
		pPlistDocument.root.SetString("NSMicrophoneUsageDescription","Game requires access to the microphone library.");
		pPlistDocument.root.SetString("NSSpeechRecognitionUsageDescription","Game requires access to the speech recogniton library.");
		pPlistDocument.root.SetString("NSUserTrackingUsageDescription","Your data will only be used to deliver personalized ads to you.");
		
		File.WriteAllText(plistPath,pPlistDocument.WriteToString());
#endif
	}

	private static void RunPostProcessTasksAndroid()
	{
		bool isManifestUsed = false;
		string appManifestPath = Path.Combine(Application.dataPath, "Plugins/Android/AndroidManifest.xml");

		if (!File.Exists(appManifestPath))
		{
			isManifestUsed = true;
			
			UnityEngine.Debug.LogError("Please create AndroidManifest.xml in Plugins/Android");
		}

		if (!isManifestUsed)
		{
			XmlDocument manifestFile = new XmlDocument();
			manifestFile.Load(appManifestPath);

			bool manifestHasChanged = false;

			manifestHasChanged = AddPermissions(manifestFile);

			if (manifestHasChanged)
			{
				//Save the changes.
				manifestFile.Save(appManifestPath);
				
				//Clean the manifest file.
				CleanManifestFile(appManifestPath);
				
				UnityEngine.Debug.Log("App's AndroidManifest.xml file check and potential modification completed.");
			}
			else
			{
				UnityEngine.Debug.Log("App's AndroidManifest.xml file check completed.");
				UnityEngine.Debug.Log("No modifications performed due to app's AndroidManifest.xml file compatibility.");
			}
		}
	}

	private static bool AddPermissions(XmlDocument manifest)
	{
		//The SDK needs permissions to be added to manifest file.
		//<uses-permission android:name="android.permission.RECORD_AUDIO" />
		
		UnityEngine.Debug.Log("Checking if all permissions needed for AndroidManifest.xml file.");

		bool hasRecordAudioPermission = false;

		XmlElement manifestRoot = manifest.DocumentElement;

		foreach (XmlNode node in manifestRoot.ChildNodes)
		{
			if (node.Name == "uses-permission")
			{
				foreach (XmlAttribute attribute in node.Attributes)
				{
					if (attribute.Value.Contains("android.permission.RECORD_AUDIO"))
					{
						hasRecordAudioPermission = true;
					}
				}
			}
		}

		bool manifestHasChanged = false;

		// If android.permission.RECORD_AUDIO permission is missing, add it.
		if (!hasRecordAudioPermission)
		{
			XmlElement element = manifest.CreateElement("uses-permission");
			element.SetAttribute("android__name", "android.permission.RECORD_AUDIO");
			manifestRoot.AppendChild(element);
			UnityEngine.Debug.Log("android.permission.RECORD_AUDIO permission successfully added to your app's AndroidManifest.xml file.");
			manifestHasChanged = true;
		}
		else
		{
			UnityEngine.Debug.Log("[Adjust]: Your app's AndroidManifest.xml file already contains android.permission.RECORD_AUDIO permission.");
		}

		return manifestHasChanged;
	}

	private static void CleanManifestFile(string manifestPath)
	{
		// Due to XML writing issue with XmlElement methods which are unable
		// to write "android:[param]" string, we have wrote "android__[param]" string instead.
		// Now make the replacement: "android:[param]" -> "android__[param]"

		TextReader manifestReader = new StreamReader(manifestPath);
		string manifestContent = manifestReader.ReadToEnd();
		manifestReader.Close();

		Regex regex = new Regex("android__");
		manifestContent = regex.Replace(manifestContent, "android:");

		TextWriter manifestWriter = new StreamWriter(manifestPath);
		manifestWriter.Write(manifestContent);
		manifestWriter.Close();
	}
}
