using UnityEngine;

public class TargetFrameRate : MonoBehaviour
{
    public int FrameRate = 61;

    protected void Awake()
    {
        Application.targetFrameRate = FrameRate;
    }
}
