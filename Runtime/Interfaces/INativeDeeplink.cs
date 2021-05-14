namespace Native.SDK
{
    /// <summary>
    /// 深度链接回调
    /// </summary>
    public interface INativeDeeplink
    {
        /// <summary>
        /// 深度链接回调
        /// </summary>
        /// <param name="data">回调数据</param>
        void onGetDeeplinkData(string data);
    }
}