using System;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace PlayFabMaster
{
    public class PlayFabManager : MonoBehaviour
    {
        public static PlayFabManager Instance { get; private set; }
        [field: SerializeField] public PlayFabSettings PlayFabSettings { get; private set; }

        private void Reset()
        {
            PlayFabSettings = Resources.Load<PlayFabSettings>("PlayFabSettings");
        }

        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
                DontDestroyOnLoad(gameObject);
            }
            else
            {
                Destroy(gameObject);
            }
        }

        private async void Start()
        {
            PlayFabSettings.Setup();
            List<UniTask> tasks = new();
            tasks.Add(PlayFabAuthentication.LoginAndGetPlayFabIdAsync(PlayFabMasterUtility.GetUniqueDeviceId()));
            //find a way to return objects in task list for synchronized execution
        }
    }
}