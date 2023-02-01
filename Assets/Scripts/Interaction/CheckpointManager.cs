using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CheckpointManager : Singleton<CheckpointManager>
{
    public Checkpoint CurrenCheckpoint;
    public List<IResetable> resetables = new List<IResetable>();

    protected override void Awake()
    {
        base.Awake();
        resetables = FindObjectsOfType<MonoBehaviour>().OfType<IResetable>().ToList();
    }

    public void SetNewCheckpoint(Checkpoint checkpoint)
    {
        CurrenCheckpoint = checkpoint;
        foreach (var r in resetables)
        {
            if (r.Colected())
                resetables.Remove(r);
        }
    }

    public void RespawnCharacter(GameObject character)
    {
        character.transform.position = CurrenCheckpoint ?
        CurrenCheckpoint.transform.position : transform.position;

        resetables.ForEach(r => r.OnReset());
    }
}

public interface IResetable
{
    public void OnReset();
    public bool Colected();
}
