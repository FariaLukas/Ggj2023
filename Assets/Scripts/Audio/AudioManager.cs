using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Audio;
using Utils.Pool;

public class AudioManager : Singleton<AudioManager>
{
    [SerializeField] private AudioSource prefab;
    [AssetSelector]
    [SerializeField] private SfxSettingsSO sfxDefaultSettings;
    [AssetSelector]
    [SerializeField] private AudioSettingsSO defaultSettings;

    [Header("Audio control")]
    [SerializeField] private AudioMixer audioMixer = default;
    [Range(0f, 1f)]
    [SerializeField] private float _masterVolume = 1f;
    [Range(0f, 1f)]
    [SerializeField] private float _musicVolume = 1f;
    [Range(0f, 1f)]
    [SerializeField] private float _sfxVolume = 1f;

    private GenericPool<AudioSource> _pool;
    [ShowInInspector]
    private Dictionary<AudioSource, AudioClipSO> _playingSounds = new Dictionary<AudioSource, AudioClipSO>();

    protected override void Awake()
    {
        transform.parent = null;
        base.Awake();
        DontDestroyOnLoad(this);
        _pool = new GenericPool<AudioSource>(prefab, 7, transform);
    }

    public AudioSource PlaySfx(AudioClipSO clipSO, Vector3 pos,
    SfxSettingsSO settings = null, bool playIfNotPlaying = false,
    bool reset = false)
    {
        if (!clipSO) return null;

        if (settings == null)
            settings = sfxDefaultSettings;

        return Play(clipSO, pos, settings, playIfNotPlaying, reset);
    }

    public AudioSource PlaySfx(AudioClipSO clipSO,
    SfxSettingsSO settings = null, bool playIfNotPlaying = false,
    bool reset = false)
    {
        return PlaySfx(clipSO, transform.position,
        settings, playIfNotPlaying, reset);
    }

    public AudioSource Play(AudioClipSO clipSO,
    AudioSettingsSO settings = null, bool playIfNotPlaying = false,
    bool reset = false)
    {
        return Play(clipSO, transform.position,
        settings, playIfNotPlaying, reset);
    }

    public AudioSource Play(AudioClipSO clipSO, Vector3 pos,
    AudioSettingsSO settings = null, bool playIfNotPlaying = false,
    bool reset = false)
    {
        if (!clipSO) return null;

        if (settings == null)
            settings = defaultSettings;

        if (playIfNotPlaying)
        {
            if (_playingSounds.ContainsValue(clipSO))
                return null;
        }

        var clip = clipSO.GetAudioClip(reset);
        var source = _pool.GetPoolObject();
        source.gameObject.SetActive(true);

        source.clip = clip;
        source.loop = clipSO.looping;

        settings.SetSettings(source);

        source.Play();

        _playingSounds.Add(source, clipSO);

        if (!clipSO.looping)
            FinishAudio(source);

        return source;
    }

    public void StopAudio(AudioSource source)
    {
        if (_playingSounds.ContainsKey(source))
        {
            source.Stop();
            _pool.Destroy(source);
            _playingSounds.Remove(source);
        }
    }

    public void FinishAudio(AudioSource source)
    {
        float timeRemaining = 0;
        
        if (source.clip)
            timeRemaining = source.clip.length - source.time;

        StartCoroutine(StopAudio(source, timeRemaining));
    }

    private IEnumerator StopAudio(AudioSource source, float delay)
    {
        yield return new WaitForSeconds(delay);
        StopAudio(source);
    }

    public void ChangeMasterVolume(float newVolume)
    {
        _masterVolume = newVolume;
        SetGroupVolume("MasterVolume", _masterVolume);
    }

    public void ChangeMusicVolume(float newVolume)
    {
        _musicVolume = newVolume;
        SetGroupVolume("MusicVolume", _musicVolume);
    }

    public void ChangeSFXVolume(float newVolume)
    {
        _sfxVolume = newVolume;
        SetGroupVolume("SFXVolume", _sfxVolume);
    }

    public void SetGroupVolume(string parameterName, float normalizedVolume)
    {
        if (!audioMixer) return;
        bool volumeSet = audioMixer.SetFloat(parameterName, NormalizedToMixerValue(normalizedVolume));
        if (!volumeSet)
            Debug.LogError("The AudioMixer parameter was not found");
    }

    public float GetGroupVolume(string parameterName)
    {
        if (!audioMixer) return 0;
        if (audioMixer.GetFloat(parameterName, out float rawVolume))
        {
            return MixerValueToNormalized(rawVolume);
        }
        else
        {
            Debug.LogError("The AudioMixer parameter was not found");
            return 0f;
        }
    }

    private float MixerValueToNormalized(float mixerValue)
    {
        // We're assuming the range [-80dB to 0dB] becomes [0 to 1]
        return 1f + (mixerValue / 80f);
    }

    private float NormalizedToMixerValue(float normalizedValue)
    {
        // We're assuming the range [0 to 1] becomes [-80dB to 0dB]
        // This doesn't allow values over 0dB
        return (normalizedValue - 1f) * 80f;
    }
}
