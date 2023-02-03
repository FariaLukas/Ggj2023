using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using DG.Tweening;
using Sirenix.OdinInspector;
using UnityEngine.Events;

public class CheckpointManager : Singleton<CheckpointManager>
{
    public Checkpoint CurrenCheckpoint;
    [SerializeField] private CanvasGroup checkAnimation;
    [SerializeField] private CanvasGroup RespawnAnimation;
    [SerializeField] private float checkSpeed = .5f, respawnSpeed = 1f;
    [ShowInInspector]
    public List<IResetable> resetables = new List<IResetable>();
    public bool Animating;

    protected override void Awake()
    {
        base.Awake();
        resetables = FindObjectsOfType<MonoBehaviour>().OfType<IResetable>().ToList();
        RespawnOut();
    }

    public void SetNewCheckpoint(Checkpoint checkpoint)
    {
        CurrenCheckpoint = checkpoint;

        for (int i = resetables.Count - 1; i >= 0; i--)
        {
            var r = resetables[i];
            if (r.Colected())
                resetables.Remove(r);
        }

        if (checkpoint.ShowWarning && checkAnimation)
        {
            checkAnimation.DOFade(1, checkSpeed).SetLoops(2, LoopType.Yoyo);
        }
    }

    public void RespawnCharacter(GameObject character)
    {
        RespawnIn(() => RespawnOut(), character);

        resetables.ForEach(r => r.OnReset());
    }

    public void RespawnIn(UnityAction onEnd = null, GameObject character = null)
    {
        RespawnAnimation.alpha = 0;
        RespawnAnimation.blocksRaycasts = true;
        Animating = true;

        RespawnAnimation.DOFade(1, respawnSpeed).OnComplete(() =>
        {
            onEnd?.Invoke();
            if (character)
                character.transform.position = CurrenCheckpoint ?
                CurrenCheckpoint.transform.position : transform.position;
        });
    }

    public void RespawnOut(UnityAction onEnd = null, GameObject character = null)
    {
        RespawnAnimation.alpha = 1;
        RespawnAnimation.blocksRaycasts = true;
        RespawnAnimation.DOFade(0, respawnSpeed).OnComplete(() =>
        {
            onEnd?.Invoke();
            Animating = false;
            if (character)
                character.transform.position = CurrenCheckpoint ?
                CurrenCheckpoint.transform.position : transform.position;
            RespawnAnimation.blocksRaycasts = false;
            Debug.Log("Can Walk");
        });
    }
}

public interface IResetable
{
    public void OnReset();
    public bool Colected();
}
