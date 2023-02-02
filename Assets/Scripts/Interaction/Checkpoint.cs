using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkpoint : CollisionDetection
{
    protected override void TriggerEnter(GameObject instigator)
    {
        base.TriggerEnter(instigator);
        CheckpointManager.Instance?.SetNewCheckpoint(this);
        Collider.enabled = false;
    }
}
