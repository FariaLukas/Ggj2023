using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class FloodgateIsntAnimating : Floodgate
{
    [SerializeField] private List<AnimateVines> Animations = new List<AnimateVines>();

    public override void Setup()
    {
        base.Setup();

        Animations = FindObjectsOfType<AnimateVines>().ToList();
    }

    public override bool IsOpen()
    {
        foreach (var a in Animations)
        {
            if (a.Animating)
                return false;
        }

        return true;
    }
}
