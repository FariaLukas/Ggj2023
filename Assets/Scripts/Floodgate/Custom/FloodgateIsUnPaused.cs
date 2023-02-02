using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloodgateIsUnPaused : Floodgate
{
    public override bool IsOpen()
    {
        return Time.timeScale == 1;
    }
}
