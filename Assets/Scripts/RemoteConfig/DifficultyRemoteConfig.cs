using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Services.Authentication;
using Unity.Services.Core;
using System.Threading.Tasks;
using Gameplay;
using TMPro;
using Unity.Services.RemoteConfig;

namespace RemoteConfig
{
    public class DifficultyRemoteConfig : MonoBehaviour
    {
        [SerializeField] private GameObject remoteChangePanelUI;
        [SerializeField] private TMP_Text remoteChangeText;

        public static event Action OnRemoteConfigUpdated;
        
        
        public struct Difficulty
        {
            public int DifficultyLevel;
        }


        public struct AppStruct
        {
        }

        async void Awake()
        {
            remoteChangePanelUI.SetActive(false);
            await FetchDifficultySetting();
        }

        async Task FetchDifficultySetting()
        {
            if (Utilities.CheckForInternetConnection())
            {
                await InitializeRPC();
            }

            RemoteConfigService.Instance.FetchCompleted += ApplyDifficultySetting;
            RemoteConfigService.Instance.SetEnvironmentID("3fa76a5f-501e-4669-b56f-ee53509d0490");

            await RemoteConfigService.Instance.FetchConfigsAsync(new Difficulty(),
                new AppStruct());
        }


        private void ApplyDifficultySetting(ConfigResponse response)
        {

            switch (response.requestOrigin)
            {
                case ConfigOrigin.Remote:
                    var difficulty = RemoteConfigService.Instance.appConfig.GetInt("difficulty");
                    remoteChangePanelUI.SetActive(true);
                    remoteChangeText.text = "Remote Config Difficulty changed to " + (Bot.Difficulty)difficulty;
                    Bot.SetDifficulty((Bot.Difficulty)difficulty);
                    OnRemoteConfigUpdated?.Invoke();
                    break;
            }
            
        }


        async Task InitializeRPC()
        {
            await UnityServices.InitializeAsync();

            if (!AuthenticationService.Instance.IsSignedIn)
            {
                await AuthenticationService.Instance.SignInAnonymouslyAsync();
            }
        }
    }
}