using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkpoint : CollisionDetection
{
    [SerializeField] private Color gizmoColor;

    protected override void TriggerEnter(GameObject instigator)
    {
        base.TriggerEnter(instigator);
        CheckpointManager.Instance?.SetNewCheckpoint(this);
        Collider.enabled = false;
    }

    private void OnDrawGizmos()
    {
        if (!Collider) return;
        if (Collider is BoxCollider2D)
        {
            Gizmos.color = gizmoColor;

            BoxCollider2D col = (BoxCollider2D)Collider;
            Gizmos.DrawCube(transform.position + (Vector3)Collider.offset, col.size);
        }
    }
}
