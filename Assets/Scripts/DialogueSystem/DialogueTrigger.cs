using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Events;

public class DialogueTrigger : CollisionDetection
{
    [SerializeField] private DialogueSO Dialogue;
    [FoldoutGroup("Events")]
    [PropertyOrder(2)]
    [SerializeField] private UnityEvent OnDialogueEnd;

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
        OnDialogueEnd?.Invoke();
    }
}
