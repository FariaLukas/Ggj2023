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
}
