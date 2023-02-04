using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColliderDisplayer : MonoBehaviour
{
    [SerializeField] private EdgeCollider2D Collider2D;

    private void Reset()
    {
        if (!Collider2D)
            TryGetComponent(out Collider2D);
    }

    private void OnDrawGizmos()
    {
        if (!Collider2D) return;
        Gizmos.matrix = transform.localToWorldMatrix;
        for (int i = 1; i < Collider2D.pointCount; i++)
        {
            Gizmos.DrawLine(Collider2D.points[i - 1], Collider2D.points[i]);
        }
    }
}
