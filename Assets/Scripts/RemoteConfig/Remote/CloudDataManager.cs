// Copyright (C) 2023 Rafael Zagolin, All Rights Reserved
using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;

using UnityEngine;
using Unity.Services.Core;
using Unity.Services.Authentication;
using Unity.Services.RemoteConfig;
using UnityEditor.MemoryProfiler;
using System.Data;
using System.Drawing.Text;
using System.Linq;

namespace Rafa
{
    public class CloudDataManager : MonoBehaviour
    {
        [HideInInspector]
        public CloudDataManager Instance;

        private RemoteConfigService Remote => RemoteConfigService.Instance;
        /*
        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSplashScreen)]
        public static void StartUp()
        {
            new GameObject().AddComponent<CloudDataManager>();
        }*/

        private void Awake()
        {
            // Creating this class as a Singleton 
            if (Instance != null && Instance != this)
            {
                Destroy(gameObject);
                return;
            }

            Instance = this;         
        }

        public async Task Start()
        {
            // TODO: assume we're connected already

            // TODO: setup remote config callback

            // TODO: choose environment
            // Authenticate/auth with Unity Gaming Services
            await Connection.Service.Authenticate();

            // Fetch remote config data
            FetchConfigData();

        }

        public void FetchConfigData()
        {
            // do some cool ugs (unity gaming service) remote config stuff here
            RemoteConfigService.Instance.SetEnvironmentID( Connection.Service.RemoteID );

            // fetch settings for our main character
            Remote.FetchConfigs(new UserAttributes(), new AppAttributes());

            // check that scene has not been unloaded
            if(this == null)
            {
                return;
            }

            ApplyRemoteConfigs(Remote.appConfig);
        }

        private void ApplyRemoteConfigs(RuntimeConfig theAppConfig)
        {
            // TODO: turn those settings into a c# class
            //var actualData = theAppConfig.GetFloat("Speed");
            var thePlayerDataJSON = theAppConfig.GetJson("Player");
            PlayerData theData = JsonUtility.FromJson<PlayerData>(thePlayerDataJSON);
            



            // find all instances that implement the interface IRemoteCharacter
            List<IRemoteData> list = FindRemoteDataObjects();
            foreach (IRemoteData remoteData in list)
            {
                remoteData.LoadConfigs(theData);
                Debug.Log(remoteData);
            }


            // tell each one to update (the new Remote data)
            

        }

        public List<IRemoteData> FindRemoteDataObjects()
        {
            IEnumerable<IRemoteData> list = FindObjectsOfType<MonoBehaviour>().OfType<IRemoteData>();
            return new List<IRemoteData>(list);
        }

        public struct UserAttributes
        {

        }

        public struct AppAttributes
        {

        }
    }  
}
