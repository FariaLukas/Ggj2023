using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using DG.Tweening;
using Sirenix.OdinInspector;

public class CheckpointManager : Singleton<CheckpointManager>
{
    public Checkpoint CurrenCheckpoint;
    [SerializeField] private CanvasGroup checkAnimation;
    [SerializeField] private float animationSpeed = .3f;
    [ShowInInspector]
    public List<IResetable> resetables = new List<IResetable>();

    protected override void Awake()
    {
        base.Awake();
        resetables = FindObjectsOfType<MonoBehaviour>().OfType<IResetable>().ToList();
    }

    public void SetNewCheckpoint(Checkpoint checkpoint)
    {
        CurrenCheckpoint = checkpoint;

        for (int i = resetables.Count - 1; i >= 0; i--)
        {
            var r = resetables[i];
            if (r.Colected())
                resetables.Remove(r);
            Debug.Log("Try To Remove Set");
        }

        if (checkpoint.ShowWarning && checkAnimation)
        {
            checkAnimation.DOFade(1, animationSpeed).SetLoops(2, LoopType.Yoyo);
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
