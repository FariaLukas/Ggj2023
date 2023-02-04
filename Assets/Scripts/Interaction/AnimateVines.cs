using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.Events;
using Sirenix.OdinInspector;

public class AnimateVines : CollisionDetection
{
    public bool Animating;
    [SerializeField] private float Speed;
    [SerializeField] private GameObject MaskObject;
    [SerializeField] private Ease ease;
    [FoldoutGroup("Events")]
    [PropertyOrder(2)]

    [SerializeField] private UnityEvent OnAnimationFinished;

    private void Start()
    {
        if (MaskObject)
            MaskObject.SetActive(true);
    }

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
        OnAnimationFinished?.Invoke();
    }
}
