using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObjects/DialogueSystem/Dialogue")]
public class DialogueSO : ScriptableObject
{
    public List<Sentence> Dialogue = new List<Sentence>();
}

[System.Serializable]
public class Sentence
{
    public DialogueActor Actor;
    [TextArea]
    public string Message;
}
