using System;
using UnityEngine.Device;

namespace PlayFabMaster
{
    public static class PlayFabMasterUtility 
    {
        public static string GetUniqueDeviceId()
        {
            string customId;
            customId = SystemInfo.deviceUniqueIdentifier;
#if UNITY_ANDROID && !UNITY_EDITOR
            {
                AndroidJavaClass unityPlayer = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
                AndroidJavaObject currentActivity = unityPlayer.GetStatic<AndroidJavaObject>("currentActivity");
                AndroidJavaObject contentResolver = currentActivity.Call<AndroidJavaObject>("getContentResolver");
                AndroidJavaClass secure = new AndroidJavaClass("android.provider.Settings$Secure");
                customId = secure.CallStatic<string>("getString", contentResolver, "android_id");
            }
#endif
#if UNITY_IOS && !UNITY_EDITOR
        {
            customId = UnityEngine.iOS.Device.vendorIdentifier;
        }
#endif
            return customId;
        }
    }
}
