using System.Collections;
using System.Collections.Generic;
using MonsterWhaser.Utilities.Views;
using UnityEngine;
using UnityEngine.Events;

public class AnimatePause : MonoBehaviour
{
    [SerializeField] protected bool DisableOnExit;
    [SerializeField] protected ViewAnimationSettingsSO EnterSettings;
    [SerializeField] protected ViewAnimationSettingsSO ExitSettings;

    [Header("References")]
    [SerializeField] protected ViewReferences ViewReferences;

    public void AnimateIn(UnityEvent onEnd = null)
    {
        ExitSettings.TryAdjustParemeter(ViewReferences);
        EnterSettings.Animate(ViewReferences, AnimationKind.IN, DisableOnExit, onEnd);
    }

    public void AnimateOut(UnityEvent onEnd = null)
    {
        EnterSettings.TryAdjustParemeter(ViewReferences);
        ExitSettings.Animate(ViewReferences, AnimationKind.OUT, DisableOnExit, onEnd);
    }
}
