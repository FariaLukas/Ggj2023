using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Audio;
[CreateAssetMenu(menuName = "ScriptableObjects/Audio/AudioSettings", fileName = "AudioSettingsSO")]
public class AudioSettingsSO : ScriptableObject
{
    public AudioMixerGroup outputAudioMixerGroup = null;

    [Header("Sound properties")]
    [PropertyRange(0f, 1)] public float volume = 1f;
    [Range(-3f, 3f)] public float pitch = 1f;
    [Range(-1f, 1f)] public float panStereo = 0f;


    [Header("Spatialisation")]
    [PropertySpace(0, 10)]
    [Range(0f, 1f)] public float spatialBlend = 0f;

    public virtual void SetSettings(AudioSource source)
    {
        source.volume = volume;
        source.pitch = pitch;
        source.spatialBlend = spatialBlend;
        source.panStereo = panStereo;
        source.outputAudioMixerGroup = outputAudioMixerGroup;
    }
}
