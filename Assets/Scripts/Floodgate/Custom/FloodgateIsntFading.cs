using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloodgateIsntFading : Floodgate
{
    public override bool IsOpen()
    {
        if (CheckpointManager.Instance)
            return !CheckpointManager.Instance.Animating;

        return true;
    }
}

