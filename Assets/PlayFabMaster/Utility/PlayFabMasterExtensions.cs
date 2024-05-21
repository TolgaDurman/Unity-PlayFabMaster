using PlayFab;
using UnityEngine;

namespace PlayFabMaster
{
    public static class PlayFabMasterExtensions
    {
        public static bool HasError(this PlayFabBaseResult result)
        {
            if (result.Error != null)
                Debug.LogError(result.Error?.GenerateErrorReport());
            return result.Error != null;
        }
    }
}