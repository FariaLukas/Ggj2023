using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class SquashAnimation : MonoBehaviour
{
    [System.Serializable]
    public struct Variables
    {
        public Vector2 EndValue;
        public float Speed;
        public Ease Ease;
    }

    public Variables Strech, Squash;


    private void Start()
    {
        SquashAnim();
    }

    private void SquashAnim()
    {
        transform.DOScale(Squash.EndValue, Squash.Speed).SetEase(Squash.Ease).OnComplete(() => StrechAnim()); ;
    }

    private void StrechAnim()
    {
        transform.DOScale(Strech.EndValue, Strech.Speed).SetEase(Strech.Ease).OnComplete(() => SquashAnim());
    }
}
