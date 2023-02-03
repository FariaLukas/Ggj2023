using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

public class AudioPlayer : MonoBehaviour
{
    [AssetSelector]
    [SerializeField] private AudioClipSO audioClip;
    [AssetSelector]
    [SerializeField] private AudioSettingsSO settings;
    [SerializeField] private bool reset, playIfNotPlaying;
    [SerializeField] private bool localPosition;
    [ShowIf(nameof(localPosition))]
    [SerializeField] private Vector3 position;
    [SerializeField] private bool debug;
    [SerializeField] private bool isSfx;
    [SerializeField] private bool playOnStart;
    [SerializeField] private bool playOnEnable;
    private AudioSource _currenSource;

    private void Start()
    {
        if (playOnStart)
        {
            if (isSfx || settings &&
            settings.GetType().Equals(typeof(SfxSettingsSO)))
                PlaySfx();
            else
                PlayAudio();
        }
    }

    private void OnEnable()
    {
        if (playOnEnable)
        {
            if (isSfx || settings &&
           settings.GetType().Equals(typeof(SfxSettingsSO)))
                PlaySfx();
            else
                PlayAudio();
        }
    }

    public void ChangeAudio(AudioClipSO clipSO, AudioSettingsSO settingsSO = null)
    {
        audioClip = clipSO;
        settings = settingsSO;
    }

    public void PlayAudio(AudioClipSO clipSO, AudioSettingsSO settingsSO = null)
    {
        if (!CanPlay()) return;

        if (settingsSO == null)
            settingsSO = settings;

        if (localPosition)
            _currenSource = AudioManager.Instance.Play(clipSO, transform.position + position,
            settingsSO, playIfNotPlaying, reset);
        else
            _currenSource = AudioManager.Instance.Play(clipSO, settingsSO, playIfNotPlaying, reset);
    }

    [Button]
    public void PlayAudio()
    {
        PlayAudio(audioClip, settings);
    }

    public void PlaySfx(AudioClipSO clipSO, SfxSettingsSO settingsSO = null)
    {
        if (!CanPlay()) return;

        if (localPosition)
            _currenSource = AudioManager.Instance.PlaySfx(clipSO, transform.position + position,
            settingsSO, playIfNotPlaying, reset);
        else
            _currenSource = AudioManager.Instance.PlaySfx(clipSO, settingsSO, playIfNotPlaying, reset);
    }

    [Button]
    public void PlaySfx()
    {
        if (settings == null)
        {
            PlaySfx(audioClip);
        }
        else if (settings.GetType().Equals(typeof(SfxSettingsSO)))
        {
            PlaySfx(audioClip, (SfxSettingsSO)settings);
        }
    }

    public void StopAudio()
    {
        if (!_currenSource) return;
        if (CanPlay() && _currenSource.isPlaying &&
        _currenSource.clip == audioClip.GetAudioClip()) return;

        AudioManager.Instance.StopAudio(_currenSource);
    }

    protected bool CanPlay()
    {
        return AudioManager.Instance && audioClip && Application.isPlaying;
    }

    private void OnDrawGizmos()
    {
        if (!debug) return;
        Gizmos.DrawWireSphere(transform.position + position, 1f);
    }

}
