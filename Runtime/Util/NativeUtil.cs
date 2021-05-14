namespace Native.SDK
{
    #region Enum
    /// <summary>
    /// IOS广告状态
    /// </summary>
    public enum AdvertisingStatus
    {
        /// <summary>
        /// 未向用户请求授权
        /// </summary>
        ATTrackingManagerAuthorizationStatusNotDetermined = 0,
        /// <summary>
        /// 用户在系统级别开启了限制广告追踪
        /// </summary>
        ATTrackingManagerAuthorizationStatusRestricted = 1,
        /// <summary>
        /// 用户拒绝向App授权
        /// </summary>
        ATTrackingManagerAuthorizationStatusDenied = 2,
        /// <summary>
        /// 用户同意向App授权
        /// </summary>
        ATTrackingManagerAuthorizationStatusAuthorized = 3,
        /// <summary>
        /// 不为IOS14系统
        /// </summary>
        ATTrackingManagerNone = 4
    }
    
    /// <summary>
    /// IOS广告状态权限
    /// </summary>
    public enum AdvertisingAuthority
    {
        /// <summary>
        /// 未向用户请求授权
        /// </summary>
        ATTrackingManagerAuthorizationStatusNotDetermined = 0,
        /// <summary>
        /// 用户在系统级别开启了限制广告追踪
        /// </summary>
        ATTrackingManagerAuthorizationStatusRestricted = 1,
        /// <summary>
        /// 用户拒绝向App授权
        /// </summary>
        ATTrackingManagerAuthorizationStatusDenied = 2,
        /// <summary>
        /// 用户同意向App授权
        /// </summary>
        ATTrackingManagerAuthorizationStatusAuthorized = 3,
        /// <summary>
        /// 不为IOS14系统
        /// </summary>
        ATTrackingManagerNone = 4
    }

    #endregion
}