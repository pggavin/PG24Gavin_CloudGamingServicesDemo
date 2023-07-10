// Copyright (C) 2023 Rafael Zagolin, All Rights Reserved
using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;

using UnityEngine;
using Unity.Services.Core;
using Unity.Services.Authentication;

public class Connection
{
    private static Connection _instance = null;
    private Dictionary<string, string> _environmentList = new Dictionary<string, string>();

    // keep track of current environment
#if UNITY_EDITOR
    private static string _currentEnv = "development";
#else
    private static string _currentEnv = "production";
#endif

    private IAuthenticationService Auth => AuthenticationService.Instance;

    public static Connection Service
    {
        get
        {
            if (_instance == null)
            {
                _instance = new Connection();
            }
            return _instance;
        }
    }

    public string RemoteID => _environmentList[_currentEnv];
    


    private Connection()
    {
        // TODO: Add specs for UGS remote config environments
        _environmentList = new Dictionary<string, string>
        {
            {"production", "ed439148-3c2b-4145-aae6-7e2481053f9c" },
            {"telemetryproject", "7621d1a6-7239-47f4-b499-5360e2fffa42" }
        };

    }

    public async Task Authenticate(string theEnv = "telemetryproject")
    {
        // go connect to the environment specified
        var options = new InitializationOptions().SetOption
            ("com.unity.services.core.environment-name",
            theEnv);

        _currentEnv = theEnv;


        // Let the developer know they are connected

        // initialize the login function
        await UnityServices.InitializeAsync(options); 

        // if it's not signed in, please sign in
        if (!AuthenticationService.Instance.IsSignedIn)
        {
            await Auth.SignInAnonymouslyAsync();
        }

        Debug.Log($"Player ID {Auth.PlayerId}");


    }

}

