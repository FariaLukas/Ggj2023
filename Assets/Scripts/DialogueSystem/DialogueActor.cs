using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

[CreateAssetMenu(menuName = "ScriptableObjects/DialogueSystem/Actor")]
public class DialogueActor : ScriptableObject
{
    public string ActorName;
    [PreviewField(50)]
    public Sprite ActorSprite;
    public bool LeftSide;
}
