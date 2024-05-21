using Cysharp.Threading.Tasks;
using PlayFab;
using PlayFab.ClientModels;

namespace PlayFabMaster
{
    public static class TitlePlayer
    {
        public static async UniTask<UserAccountInfo> GetAccountInfo(string playFabId)
        {
            var request = new GetAccountInfoRequest
            {
                PlayFabId = playFabId,
            };
            var result = await PlayFabClientAPI.GetAccountInfoAsync(request);
            return !result.HasError() ? result.Result.AccountInfo : null;
        }
    }
}