using System;
using UnityEngine;

namespace Native.SDK
{
	public class NativeSDK
	{
		/// <summary>
		/// 获取深度链接参数
		/// </summary>
		/// <param name="gameObject"></param>
		public static string GetDeeplinkData(MonoBehaviour gameObject)
		{
#if UNITY_ANDROID && !UNITY_EDITOR	
            return NativeAndroid.GetAndroidDeeplinkData(gameObject);
#elif UNITY_IOS && !UNITY_EDITOR	
            return NativeiOS.GetIOSDeeplinkData(gameObject);
#endif
			return "null";
		}
		
		/// <summary>
		/// 获取IOS广告追踪状态
		/// </summary>
		/// <param name="gameObject"></param>
		public static void RequestAdvertisingStatus(MonoBehaviour gameObject,Action<AdvertisingStatus> callback)
		{
#if UNITY_IOS && !UNITY_EDITOR			
			NativeiOS.SetAdvertisingStatusCallback(callback);
			NativeiOS.RequestAdvertisingStatus(gameObject);
#endif
		}
		
		/// <summary>
		/// IOS14 请求广告权限
		/// </summary>
		/// <param name="gameObject"></param>
		public static void RequestAdvertisingAuthority(MonoBehaviour gameObject,Action<AdvertisingAuthority> callback)
		{
#if UNITY_IOS && !UNITY_EDITOR	
			NativeiOS.SetAdvertisingAuthorityCallback(callback);
			NativeiOS.RequestAdvertisingAuthority(gameObject);
#endif
		}
		
		/// <summary>
		/// 获取IOS设备系统版本
		/// </summary>
		/// <param name="gameObject"></param>
		public static void GetDeviceSystem(MonoBehaviour gameObject)
		{
#if UNITY_IOS && !UNITY_EDITOR				
			NativeiOS.GetDeviceSystem(gameObject);
#endif
		}
		
		/// <summary>
		/// 初始化语言转文字插件
		/// </summary>
		public static void InitTextSpeech()
		{
			SpeechToText.Init();
			TextToSpeech.Init();
		}
	}
}

