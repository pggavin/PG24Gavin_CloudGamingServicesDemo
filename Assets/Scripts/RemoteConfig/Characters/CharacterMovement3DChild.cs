// Copyright (C) 2023 Rafael Zagolin, All Rights Reserved


using CharacterMovement;
using Rafa;
using UnityEngine;

public class CharacterMovement3DChild : CharacterMovement3D, IRemoteData
{
    public void Awake()
    {
        base.Awake();
    }

    public void LoadConfigs(PlayerData theData)
    {
        // TODO: take data loaded in param list and apply it to this behaviour
        _speed = theData.Speed;
        Debug.Log("Load configs " + theData.Speed);
    }
}
