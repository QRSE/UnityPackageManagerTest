using UnityEngine;
using System.Collections;
using System.Runtime.InteropServices;
using UnityEngine.UI;
using System;

namespace Native.SDK
{
    public class TextToSpeech : MonoBehaviour
    {
        #region Init
        private static bool g_Available = false;
        public static bool Available
        {
            get
            {
                return g_Available;
            }
        }
        
        static TextToSpeech _instance;
        public static TextToSpeech Instance
        {
            get
            {
                return _instance;
            }
        }
        public static void Init()
        {
            g_Available = true;

            GameObject obj = new GameObject();
            obj.name = "TextToSpeech";
            _instance = obj.AddComponent<TextToSpeech>();
        }
        void Awake()
        {
            _instance = this;

#if !UNITY_EDITOR && (UNITY_ANDROID || UNITY_IOS)
            DontDestroyOnLoad(transform.gameObject);
#endif

        }
        #endregion

        public Action onStartCallBack;
        public Action onDoneCallback;
        public Action<string> onSpeakRangeCallback;
        public Action<bool> onSettingCallback;

        [Range(0.5f, 2)]
        public float pitch = 1f; //[0.5 - 2] Default 1
        [Range(0.5f, 2)]
        public float rate = 1f; //[min - max] android:[0.5 - 2] iOS:[0 - 1]

        public void Setting(string language, float _pitch, float _rate)
        {
            if (g_Available)
            {
                Debug.Log("Setting ==== TextToSpeech");
                pitch = _pitch;
                rate = _rate;
#if UNITY_EDITOR
#elif UNITY_IOS
                _TAG_SettingSpeak(language, pitch, rate / 2);
#elif UNITY_ANDROID
                AndroidJavaClass javaUnityClass = new AndroidJavaClass("com.starseed.speechtotext.Bridge");
                javaUnityClass.CallStatic("SettingTextToSpeed", language, pitch, rate);
#endif
            }
        }
        public void StartSpeak(string _message)
        {
            if (g_Available)
            {
#if UNITY_EDITOR
#elif UNITY_IOS
                _TAG_StartSpeak(_message);
#elif UNITY_ANDROID
                AndroidJavaClass javaUnityClass = new AndroidJavaClass("com.starseed.speechtotext.Bridge");
                javaUnityClass.CallStatic("OpenTextToSpeed", _message);
#endif
            }
        }
        public void StopSpeak()
        {
            if (g_Available)
            {
#if UNITY_EDITOR
#elif UNITY_IOS
                _TAG_StopSpeak();
#elif UNITY_ANDROID
                AndroidJavaClass javaUnityClass = new AndroidJavaClass("com.starseed.speechtotext.Bridge");
                javaUnityClass.CallStatic("StopTextToSpeed");
#endif
            }
        }

        public void onSpeechRange(string _message)
        {
            if (onSpeakRangeCallback != null && _message != null)
            {
                onSpeakRangeCallback(_message);
            }
        }
        public void onStart(string _message)
        {
            if (onStartCallBack != null)
                onStartCallBack();
        }
        public void onDone(string _message)
        {
            if (onDoneCallback != null)
                onDoneCallback();
        }
        public void onError(string _message)
        {
        }
        public void onMessage(string _message)
        {
            Debug.Log("iOS Setting Message===>" + _message);
            if (_message == @"Setting Success")
            {
                onSettingResult("0");
            }
        }
        /** Denotes the language is available for the language by the locale, but not the country and variant. */
        public const int LANG_AVAILABLE = 0;
        /** Denotes the language data is missing. */
        public const int LANG_MISSING_DATA = -1;
        /** Denotes the language is not supported. */
        public const int LANG_NOT_SUPPORTED = -2;
        public void onSettingResult(string _params)
        {
            int _error = int.Parse(_params);
            string _message = "";
            if (_error == LANG_MISSING_DATA || _error == LANG_NOT_SUPPORTED)
            {
                _message = "This Language is not supported";
                onSettingCallback?.Invoke(false);
            }
            else
            {
                _message = "This Language valid";
                onSettingCallback?.Invoke(true);
            }
            Debug.Log(_message);
        }

#if UNITY_IOS
        [DllImport("__Internal")]
        private static extern void _TAG_StartSpeak(string _message);

        [DllImport("__Internal")]
        private static extern void _TAG_SettingSpeak(string _language, float _pitch, float _rate);

        [DllImport("__Internal")]
        private static extern void _TAG_StopSpeak();
#endif

        public void Destroy()
        {
            Destroy(gameObject);
        }
    }
}