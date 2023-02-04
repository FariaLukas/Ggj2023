using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

[CreateAssetMenu(menuName = "ScriptableObjects/Pickable/Data")]
public class PickableData : ScriptableObject
{
    public string Identifier;
    [PreviewField(50)]
    public Sprite Displayer;
    [PreviewField(50)]
    public Sprite DisplayerOff;
    [PreviewField(50)]
    public Sprite DisplayerUsed;
    [PreviewField(50)]
    public Sprite DisplayerWorld;
}
