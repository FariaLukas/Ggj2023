using System;
using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Events;

public class Pickable : CollisionDetection, IResetable
{
    [SerializeField] private PickableData Data;
    [FoldoutGroup("Events")]
    [PropertyOrder(2)]
    public UnityEvent OnPickEvent;
    public Action OnPick;
    [SerializeField] protected Transform Visual;

    [SerializeField] protected bool Animate, HideOnPick;
    [ShowIf(nameof(Animate))]
    [SerializeField] protected float Speed = 3f, MinDistance = .1f;

    private float _startTime;
    private float _journeyLength;
    protected Transform _target;

    private Vector2 _initialPosition;
    private bool _collected;

    private void Awake()
    {
        _initialPosition = transform.position;
    }

    protected override void TriggerEnter(GameObject instigator)
    {
        base.TriggerEnter(instigator);

        if (!Animate)
        {
            PickUp();
        }
        else
        {
            _target = instigator.transform;
            _startTime = Time.time;
            _journeyLength = Vector3.Distance(transform.position, _target.position);
        }
    }

    private void Update()
    {
        if (!Animate || _target == null) return;

        float distCovered = (Time.time - _startTime) * Speed;
        float fractionOfJourney = distCovered / _journeyLength;

        transform.position = Vector2.Lerp(transform.position, _target.position, fractionOfJourney);

        if (Vector2.Distance(transform.position, _target.position) < MinDistance)
        {
            PickUp();
        }
    }

    protected virtual void PickUp()
    {
        OnPick?.Invoke();
        OnPickEvent.Invoke();

        _target = null;

        if (HideOnPick)
        {
            Visual.gameObject.SetActive(false);
        }

        if (Data)
            Inventory.Instance?.AddItem(Data.Identifier);

        _collected = true;
    }

    public void OnReset()
    {
        transform.position = _initialPosition;
        Collider.enabled = true;
        Visual.gameObject.SetActive(true);
        if (Data)
            Inventory.Instance?.RemoveItem(Data.Identifier);
    }

    public bool Colected()
    {
        return _collected;
    }
}
