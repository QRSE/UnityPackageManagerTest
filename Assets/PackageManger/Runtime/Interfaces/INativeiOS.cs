namespace Native.SDK
{
    public interface INativeiOS
    {
        /// <summary>
        /// 获取IOS设备的系统版本
        /// </summary>
        /// <param name="version">版本号</param>
        void onGetDeviceSystem(string version);
    }
}