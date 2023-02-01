using System.Collections;
using System.Collections.Generic;
using MonsterWhaser.Utilities.Views;
using UnityEngine;

public class AnimatePause : MonoBehaviour
{
    [SerializeField] protected bool DisableOnExit;
    [SerializeField] protected ViewAnimationSettingsSO EnterSettings;
    [SerializeField] protected ViewAnimationSettingsSO ExitSettings;

    [Header("References")]
    [SerializeField] protected ViewReferences ViewReferences;
    
    public void AnimateIn()
    {
        ExitSettings.TryAdjustParemeter(ViewReferences);
        EnterSettings.Animate(ViewReferences, AnimationKind.IN, DisableOnExit);
    }

    public void AnimateOut()
    {
        EnterSettings.TryAdjustParemeter(ViewReferences);
        ExitSettings.Animate(ViewReferences, AnimationKind.OUT, DisableOnExit);
    }
}
