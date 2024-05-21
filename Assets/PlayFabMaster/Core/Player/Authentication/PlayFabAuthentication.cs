using System;
using Cysharp.Threading.Tasks;
using PlayFab;
using PlayFab.ClientModels;

namespace PlayFabMaster
{
    public static class PlayFabAuthentication
    {
        private static string _customId;
        public static async UniTask<string> LoginAndGetPlayFabIdAsync(string customId,Action<bool> isCreated = null)
        {
            PlayFabResult<LoginResult> loginResult = null;
            try
            {
                var loginParams = await LoginWithCustomIDAsync(customId);
                loginResult = loginParams.result;
                isCreated?.Invoke(loginParams.created);
            }
            catch (PlayFabException e)
            {
                Console.WriteLine(e);
                throw;
            }
            return loginResult.Result.PlayFabId;
        }

        public static async UniTask<bool> IsCustomIdLinkedAsync(string customId)
        {
            PlayFabResult<LoginResult> login = null;
            var request = new LoginWithCustomIDRequest
            {
                CustomId = customId
            };
            try
            {
                login = await PlayFabClientAPI.LoginWithCustomIDAsync(request);
                if (login.Result.EntityToken == null)
                    return false;
                return true;
            }
            catch (PlayFabException e)
            {
                Console.WriteLine(e);
                return false;
            }
        }

        private static async UniTask<(PlayFabResult<LoginResult> result, bool created)> LoginWithCustomIDAsync(
            string id)
        {
            PlayFabResult<LoginResult> login = null;
            var request = new LoginWithCustomIDRequest
            {
                CustomId = id,
                CreateAccount = true
            };

            try
            {
                login = await PlayFabClientAPI.LoginWithCustomIDAsync(request);
                login.HasError();
                return (login, login.Result.NewlyCreated);
            }
            catch (PlayFabException e)
            {
                Console.WriteLine(e);
                throw;
            }
        }
        public static async UniTask DeleteCustomIdAsync(string customId)
        {
            var request = new UnlinkCustomIDRequest
            {
                CustomId = customId
            };
            try
            {
                await PlayFabClientAPI.UnlinkCustomIDAsync(request);
            }
            catch (PlayFabException e)
            {
                Console.WriteLine(e);
                throw;
            }
        }
    }
}