using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : CollisionDetection
{
    [SerializeField] private PickableData Key;
    [SerializeField] private GameObject Visual;
    public System.Action OnInsertKey;

    protected override void TriggerEnter(GameObject instigator)
    {
        if (!Inventory.Instance || !Key) return;

        if (!Inventory.Instance.HasItem(Key.Identifier)) return;

        base.TriggerEnter(instigator);

        OnInsertKey?.Invoke();

        Inventory.Instance.RemoveItem(Key.Identifier);

        Collider.enabled = false;
        Visual.SetActive(false);
    }
}
