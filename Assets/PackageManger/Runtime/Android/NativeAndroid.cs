#if UNITY_ANDROID
using UnityEngine;

namespace Native.SDK
{
    public class NativeAndroid
    {
        /// <summary>
        /// 获取Android深度链接
        /// </summary>
        /// <param name="gameObject"></param>
        public static string GetAndroidDeeplinkData(MonoBehaviour gameObject)
        {
            AndroidJavaClass javaUnityClass = new AndroidJavaClass("com.starseed.speechtotext.Bridge");
            return javaUnityClass.CallStatic<string>("GetDeeplinkData",gameObject.name);
        }
    }
}
#endif
