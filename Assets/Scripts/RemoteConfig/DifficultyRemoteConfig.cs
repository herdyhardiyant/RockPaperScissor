using System;
using UnityEngine;
using Unity.Services.Authentication;
using Unity.Services.Core;
using System.Threading.Tasks;
using Unity.Services.RemoteConfig;

namespace RemoteConfig
{
    public class DifficultyRemoteConfig : MonoBehaviour
    {
        public struct Difficulty
        {
            public int DifficultyLevel;
        }
        
        
        public struct AppStruct
        {
            
        }

        async void Awake()
        {
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

            RemoteConfigService.Instance.FetchConfigs(new Difficulty(), 
                new AppStruct());

        }


        private void ApplyDifficultySetting(ConfigResponse response)
        {

            switch (response.requestOrigin)
            {
                case ConfigOrigin.Remote:
                    Debug.Log("Successfully fetched remote config");
                    var difficulty = RemoteConfigService.Instance.appConfig.GetInt("difficulty");
                    print("Difficulty is " + difficulty);

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