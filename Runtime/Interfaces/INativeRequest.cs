using UnityEngine;

namespace Native.SDK
{
	public interface INativeRequest
	{
		/// <summary>
		/// 广告状态回调
		/// </summary>
		/// <param name="status">状态</param>
		void onRequestAdvertisingStatus(string status);
		
		/// <summary>
		/// 请求广告权限回调
		/// </summary>
		/// <param name="authority">权限状态</param>
		void onRequestAdvertisingAuthority(string authority);
	}
}

