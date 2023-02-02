using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class AnimateVines : CollisionDetection
{
    public bool Animating;
    [SerializeField] private float Speed;
    [SerializeField] private GameObject MaskObject;
    [SerializeField] private Ease ease;

    protected override void TriggerEnter(GameObject instigator)
    {
        base.TriggerEnter(instigator);
        if (MaskObject)
        {
            MaskObject.transform.DOScaleY(0, Speed).SetEase(ease).OnComplete(() => FinishAnimation());
            Animating = true;
        }
        Collider.enabled = false;
    }

    protected void FinishAnimation()
    {
        Animating = false;
        MaskObject.SetActive(false);
    }
}
