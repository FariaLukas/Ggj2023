using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueGiveItem : DialogueTrigger
{
    [SerializeField] private Pickable pickable;

    protected override void OnEndDialogue()
    {
        base.OnEndDialogue();
        pickable.PickUp();
    }
}
