using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxPlacer : CollisionDetection
{
    [SerializeField] private GameObject colliders;
    [SerializeField] private Transform snaper;

    private void Start()
    {
        colliders.SetActive(false);
    }

    protected override void TriggerEnter(GameObject instigator)
    {
        base.TriggerEnter(instigator);

        colliders.SetActive(true);
        Collider.enabled = false;
        instigator.GetComponent<Collider2D>().enabled = false;
    
        instigator.transform.position = snaper.position;
        instigator.transform.rotation = snaper.rotation;
        instigator.GetComponent<BoxMovable>().SetCollected();
    }
}
