using UnityEditor;
using UnityEngine;

namespace PlayFabMaster
{
    public class PlayFabSettings : ScriptableObject
    {
        #if UNITY_EDITOR
        private const string SettingsAssetPath = "Assets/PlayFabMaster/Resources/PlayFabSettings.asset";
        [InitializeOnLoadMethod]
        private static void Initialize()
        {
            if (!System.IO.File.Exists(SettingsAssetPath))
            {
                var settings = CreateInstance<PlayFabSettings>();
                AssetDatabase.CreateAsset(settings, SettingsAssetPath);
                AssetDatabase.SaveAssets();
            }
        }
        #endif
        public string TitleId;
        public string SecretKey;

        public void Setup()
        {
            PlayFab.PlayFabSettings.staticSettings.TitleId = TitleId;
            PlayFab.PlayFabSettings.staticSettings.DeveloperSecretKey = SecretKey;
        }
    }
}
