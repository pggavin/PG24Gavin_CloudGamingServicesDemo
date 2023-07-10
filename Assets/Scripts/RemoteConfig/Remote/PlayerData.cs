// Copyright (C) 2023 Rafael Zagolin, All Rights Reserved

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Rafa
{
    [System.Serializable]
    public class PlayerData
    {
        [SerializeField] public float Speed;
        public PlayerData()
        {
            Speed = 1f; // default value
        }
    }

}
