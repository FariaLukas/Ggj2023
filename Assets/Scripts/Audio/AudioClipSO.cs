using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;
using Helper;

[CreateAssetMenu(menuName = "ScriptableObjects/Audio/AudioClip", fileName = "AudioClipSO")]
public class AudioClipSO : ScriptableObject
{
    public bool looping;
    public bool isMusic;
    public SequenceMode sequenceMode;

    [InlineEditor(InlineEditorModes.SmallPreview)]
    [SerializeField] private List<AudioClip> clip = new List<AudioClip>();
    [PropertyOrder(2)] [SerializeField] private bool debug;

    private AudioClip _nextClipToPlay;
    private AudioClip _lastClipPlayed;
    private int _lastClipIndex = -1;

    [Button]
    public AudioClip GetAudioClip(bool reset = false)
    {
        if (reset)
            ResetAudios();

        if (sequenceMode == SequenceMode.Random || clip.Count <= 1)
            _nextClipToPlay = clip.GetRandom();
        else if (sequenceMode == SequenceMode.RandomNoImmediateRepeat)
        {
            _nextClipToPlay = clip.GetRandomButNotSame(_lastClipPlayed);
            _lastClipPlayed = _nextClipToPlay;
        }
        else
        {
            _nextClipToPlay = GetSequencialClip();

            if (debug)
                Debug.Log(name + " Got: " + _lastClipIndex + " : Resetd" + reset);

            _lastClipPlayed = _nextClipToPlay;
        }

        return _nextClipToPlay;
    }

    private AudioClip GetSequencialClip()
    {
        if (!_lastClipPlayed || _lastClipPlayed &&
        _lastClipIndex >= clip.Count - 1)
        {
            _lastClipIndex = -1;
        }

        _lastClipIndex++;

        return _nextClipToPlay = clip[_lastClipIndex];
    }

    private void ResetAudios()
    {
        _lastClipIndex = -1;
        _lastClipPlayed = _nextClipToPlay = null;
    }
}

public enum SequenceMode
{
    Random,
    RandomNoImmediateRepeat,
    Sequencial
}