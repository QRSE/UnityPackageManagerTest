using System;
using System.Runtime.InteropServices;
using AOT;
using UnityEngine;

namespace Native.SDK
{
#if UNITY_IOS
    public class NativeiOS
    {
        private static Action<AdvertisingStatus> AdvertisingStatusCallback;
        private static Action<AdvertisingAuthority> AdvertisingAuthorityCallback;
        
        /// <summary>
        /// 获取广告追踪状态
        /// </summary>
        /// <param name="gameObject"></param>
        public static void RequestAdvertisingStatus(MonoBehaviour gameObject)
        {
            _requestAdvertisingStatus(gameObject.name);
        }

        /// <summary>
        /// IOS14 请求广告权限
        /// </summary>
        /// <param name="gameObject"></param>
        public static void RequestAdvertisingAuthority(MonoBehaviour gameObject)
        {
            _requestAdvertisingAuthority(gameObject.name);
        }

        /// <summary>
        /// 获取IOS设备系统版本
        /// </summary>
        /// <param name="gameObject"></param>
        public static void GetDeviceSystem(MonoBehaviour gameObject)
        {
            _getDeviceVersion(gameObject.name);
        }
        
        /// <summary>
        /// 设置IOS获取广告状态回调
        /// </summary>
        /// <param name="status"></param>
        public static void SetAdvertisingStatusCallback(Action<AdvertisingStatus> status)
        {
            AdvertisingStatusCallback = status;
            _setAdvertisingStatusCallback(_AdvertisingStatusCallback);
        }
        
        /// <summary>
        /// 设置IOS请求广告权限状态
        /// </summary>
        /// <param name="authority"></param>
        public static void SetAdvertisingAuthorityCallback(Action<AdvertisingAuthority> authority)
        {
            AdvertisingAuthorityCallback = authority;
            _setAdvertisingAuthorityCallback(_AdvertisingAuthorityCallback);
        }

        /// <summary>
        /// 获取IOS深度链接
        /// </summary>
        /// <param name="gameObject"></param>
        public static string GetIOSDeeplinkData(MonoBehaviour gameObject)
        {
            return _getDeeplinkData(gameObject.name);
        }

        #region 回调方法
        private delegate void AdvertisingAuthorityDelegate(string authority);
        [MonoPInvokeCallback(typeof(AdvertisingAuthorityDelegate))]
        public static void _AdvertisingAuthorityCallback(string authority)
        {
            Debug.Log(" authority : " + authority);
            if (authority == "0")
            {
                AdvertisingAuthorityCallback?.Invoke(AdvertisingAuthority.ATTrackingManagerAuthorizationStatusNotDetermined);
            }
            else if (authority == "1")
            {
                AdvertisingAuthorityCallback?.Invoke(AdvertisingAuthority.ATTrackingManagerAuthorizationStatusRestricted);
            }
            else if (authority == "2")
            {
                AdvertisingAuthorityCallback?.Invoke(AdvertisingAuthority.ATTrackingManagerAuthorizationStatusDenied);
            }
            else if (authority == "3")
            {
                AdvertisingAuthorityCallback?.Invoke(AdvertisingAuthority.ATTrackingManagerAuthorizationStatusAuthorized);
            }
            else if (authority == "4")
            {
                AdvertisingAuthorityCallback?.Invoke(AdvertisingAuthority.ATTrackingManagerNone);
            }
        }
        
        private delegate void AdvertisingStatusDelegate(string status);
        [MonoPInvokeCallback(typeof(AdvertisingStatusDelegate))]
        public static void _AdvertisingStatusCallback(string status)
        {
            Debug.Log(" status : " + status);
            if (status == "0")
            {
                AdvertisingStatusCallback?.Invoke(AdvertisingStatus.ATTrackingManagerAuthorizationStatusNotDetermined);
            }
            else if (status == "1")
            {
                AdvertisingStatusCallback?.Invoke(AdvertisingStatus.ATTrackingManagerAuthorizationStatusRestricted);
            }
            else if (status == "2")
            {
                AdvertisingStatusCallback?.Invoke(AdvertisingStatus.ATTrackingManagerAuthorizationStatusDenied);
            }
            else if (status == "3")
            {
                AdvertisingStatusCallback?.Invoke(AdvertisingStatus.ATTrackingManagerAuthorizationStatusAuthorized);
            }
            else if (status == "4")
            {
                AdvertisingStatusCallback?.Invoke(AdvertisingStatus.ATTrackingManagerNone);
            }
        }
        

        #endregion

        /*
         * Native ios method mapping
         */
        
        [DllImport("__Internal")]
        private static extern void _requestAdvertisingStatus(string objectName);
        
        [DllImport("__Internal")]
        private static extern void _requestAdvertisingAuthority(string objectName);
        
        [DllImport("__Internal")]
        private static extern void _getDeviceVersion(string objectName);

        [DllImport("__Internal")]
        private static extern string _getDeeplinkData(string objectName);
        
        [DllImport("__Internal")]
        private static extern void _setAdvertisingStatusCallback(AdvertisingStatusDelegate callback);

        [DllImport("__Internal")]
        private static extern void _setAdvertisingAuthorityCallback(AdvertisingAuthorityDelegate callback);
    } 
#endif
}
