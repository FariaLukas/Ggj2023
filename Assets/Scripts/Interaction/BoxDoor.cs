using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxDoor : CollisionDetection
{
    [SerializeField] private GameObject Visual;

    protected override void TriggerEnter(GameObject instigator)
    {
        base.TriggerEnter(instigator);
        Collider.enabled = false;
        if (Visual)
            Visual.SetActive(false);
    }
}
