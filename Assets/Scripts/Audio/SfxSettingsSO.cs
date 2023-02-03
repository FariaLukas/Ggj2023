using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObjects/Audio/SfxSettings", fileName = "SfxSettingsSO")]
public class SfxSettingsSO : AudioSettingsSO
{
    [BoxGroup("RandomAspects")]
    [SerializeField] protected SfxRandomType randomType;

    [Tooltip("This will be decrement or increment from 1, to create the range of the random value")]
    [BoxGroup("RandomAspects")]
    [ShowIf(nameof(_hasRandomPitch))]
    [SerializeField] protected float randomPitch = 0f;

    [Tooltip("This will be decrement from 1, to create the range of the random value")]
    [BoxGroup("RandomAspects")]
    [ShowIf(nameof(_hasRandomVol))]
    [SerializeField] protected float randomVolume = 0f;

    private bool _hasRandomVol => randomType == SfxRandomType.PitchAndVolume ||
            randomType == SfxRandomType.Volume ||
            randomType == SfxRandomType.HighPitchAndVolume ||
            randomType == SfxRandomType.LowPitchAndVolume;

    private bool _hasRandomPitch => randomType != SfxRandomType.None &&
        randomType != SfxRandomType.Volume;

    public float GetPitch()
    {
        var lowPitch = randomType == SfxRandomType.PitchAndVolume ||
        randomType == SfxRandomType.Pitch ||
        randomType == SfxRandomType.LowPitch ||
        randomType == SfxRandomType.LowPitchAndVolume ? randomPitch : 0;

        var highPitch = randomType == SfxRandomType.PitchAndVolume ||
        randomType == SfxRandomType.Pitch ||
        randomType == SfxRandomType.HighPitch ||
        randomType == SfxRandomType.HighPitchAndVolume ? randomPitch : 0;

        float x = pitch - lowPitch;
        float y = pitch + highPitch;

        if (x == 1 && y == 1) return 1;

        return Random.Range(x, y);
    }

    public float GetVolume()
    {
        if (randomType == SfxRandomType.PitchAndVolume ||
        randomType == SfxRandomType.Volume ||
        randomType == SfxRandomType.HighPitchAndVolume ||
        randomType == SfxRandomType.LowPitchAndVolume)
        {
            float x = volume - randomVolume;
            return Random.Range(x, volume);
        }
        else
        {
            return volume;
        }
    }

    public override void SetSettings(AudioSource source)
    {
        source.volume = GetVolume();
        source.pitch = GetPitch();
        source.spatialBlend = spatialBlend;
        source.panStereo = panStereo;
        source.outputAudioMixerGroup = outputAudioMixerGroup;
    }
}

public enum SfxRandomType
{
    None,
    Pitch,
    HighPitch,
    LowPitch,
    Volume,
    PitchAndVolume,
    HighPitchAndVolume,
    LowPitchAndVolume
}