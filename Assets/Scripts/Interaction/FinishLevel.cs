using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FinishLevel : CollisionDetection
{
    [SerializeField] private SceneReference nextScene;

    protected override void TriggerEnter(GameObject instigator)
    {
        if (nextScene == null) return;
        base.TriggerEnter(instigator);
        Collider.enabled = false;
        CheckpointManager.Instance.RespawnIn(() => SceneManager.LoadScene(nextScene));
    }
}
