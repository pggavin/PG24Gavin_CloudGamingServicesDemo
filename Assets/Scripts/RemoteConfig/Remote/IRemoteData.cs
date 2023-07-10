// Copyright (C) 2023 Rafael Zagolin, All Rights Reserved
using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Rafa
{
    public interface IRemoteData
    {
        /* go find these attached to other game objects so the game
         * objects know when they can get data */
        void LoadConfigs(PlayerData theData);
    }
}

