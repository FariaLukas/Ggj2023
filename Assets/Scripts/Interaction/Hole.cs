using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hole : CollisionDetection
{
    protected override void TriggerEnter(GameObject instigator)
    {
        base.TriggerEnter(instigator);
        CheckpointManager.Instance?.RespawnCharacter(instigator);
    }
}
