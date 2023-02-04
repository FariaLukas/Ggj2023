using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

public class DialogueTrigger : CollisionDetection
{
    [SerializeField] private DialogueSO Dialogue;

    [Button]
    public void CallDialogue()
    {
        if (!Dialogue) return;
        DialogueManager.Instance?.StartDialogue(Dialogue, OnEndDialogue);
    }

    protected override void TriggerEnter(GameObject instigator)
    {
        base.TriggerEnter(instigator);
        CallDialogue();
        Collider.enabled = false;
    }

    protected virtual void OnEndDialogue()
    {

    }
}
