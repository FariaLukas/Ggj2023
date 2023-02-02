using UnityEngine;
using Sirenix.OdinInspector;

public abstract class Floodgate : MonoBehaviour
{
    public string floodgateName;
    [ShowInInspector]
    public abstract bool IsOpen();

    public virtual void Setup() { }
    public virtual void Clear() { }
    public virtual void ClearSetup() { }

}