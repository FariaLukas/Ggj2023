using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FinishLevel : CollisionDetection
{
    [SerializeField] private SceneReference nextScene;
    [SerializeField] private AudioPlayer AudioPlayer;

    protected override void TriggerEnter(GameObject instigator)
    {
        if (nextScene == null) return;
        base.TriggerEnter(instigator);
        Collider.enabled = false;
        Finish();
    }

    public void Finish()
    {
        if (nextScene == null) return;
        CheckpointManager.Instance.RespawnIn(() =>
                {
                    SceneManager.LoadScene(nextScene);
                    if (AudioPlayer)
                        AudioPlayer.PlaySfx();
                });
    }
}
