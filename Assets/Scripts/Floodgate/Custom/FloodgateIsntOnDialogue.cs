using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloodgateIsntOnDialogue : Floodgate
{
    public override bool IsOpen()
    {
        if (DialogueManager.Instance)
            return !DialogueManager.Instance.IsPlaying();
        
        return true;
    }
}
