using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkpoint : CollisionDetection
{
    public bool ShowWarning;
    [SerializeField] private bool SetCheckpointOnTrigger = true;

    protected override void TriggerEnter(GameObject instigator)
    {
        if (!SetCheckpointOnTrigger) return;

        base.TriggerEnter(instigator);
        CheckpointManager.Instance?.SetNewCheckpoint(this);
        Collider.enabled = false;
    }
}
