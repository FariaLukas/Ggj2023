using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName ="ScriptableObjects/Pickable/Storage")]
public class SeedStorage : ScriptableObject
{
    public List<PickableData> Datas = new List<PickableData>();
}
