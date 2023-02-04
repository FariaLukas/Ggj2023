using System;
using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Events;

public class CollisionDetection : MonoBehaviour
{
    public LayerMask LayerToDetect;
    [SerializeField] protected Collider2D Collider;

    [SerializeField] private Color gizmoColor;

    [FoldoutGroup("Events")]
    [PropertyOrder(2)]
    public UnityEvent OnEnterEvent;
    [FoldoutGroup("Events")]
    [PropertyOrder(2)]
    public UnityEvent OnStayEvent;
    [FoldoutGroup("Events")]
    [PropertyOrder(2)]
    public UnityEvent OnExitEvent;

    public Action OnEnter;
    public Action OnStay;
    public Action OnExit;

    private void OnTriggerEnter2D(Collider2D other)
    {

        if (LayerContains(other.transform.gameObject.layer, LayerToDetect))
        {
            TriggerEnter(other.gameObject);
        }
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (LayerContains(other.transform.gameObject.layer, LayerToDetect))
        {
            TriggerStay(other.gameObject);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (LayerContains(other.transform.gameObject.layer, LayerToDetect))
        {
            TriggerExit(other.gameObject);
        }
    }

    protected bool LayerContains(int layer, LayerMask layerMask)
    {
        return layerMask == (layerMask | (1 << layer));
    }

    protected virtual void TriggerEnter(GameObject instigator)
    {
        OnEnter?.Invoke();
        OnEnterEvent?.Invoke();
    }

    protected virtual void TriggerStay(GameObject instigator)
    {
        OnStay?.Invoke();
        OnStayEvent?.Invoke();
    }

    protected virtual void TriggerExit(GameObject instigator)
    {
        OnExit?.Invoke();
        OnExitEvent?.Invoke();
    }

    private void OnDrawGizmos()
    {
        if (!Collider) return;
        Gizmos.matrix = transform.localToWorldMatrix;
        if (Collider is BoxCollider2D)
        {
            Gizmos.color = gizmoColor;

            BoxCollider2D col = (BoxCollider2D)Collider;
            Gizmos.DrawCube((Vector3)Collider.offset, col.size);
        }

        if (Collider is CircleCollider2D)
        {
            Gizmos.color = gizmoColor;

            CircleCollider2D col = (CircleCollider2D)Collider;
            Gizmos.DrawSphere((Vector3)Collider.offset, col.radius);
        }
    }
}
