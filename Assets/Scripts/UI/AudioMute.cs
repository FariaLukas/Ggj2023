using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class AudioMute : MonoBehaviour
{
    public const string muteSave = "Mute";
    [SerializeField] private AudioMixer mixer;
    [SerializeField] private GameObject toggle;

    private Button _button;
    private bool _muted;

    private void Awake()
    {
        if (ES3.KeyExists(muteSave))
            _muted = ES3.Load<bool>(muteSave);

        UpdateToggle();
    }

    private void OnEnable()
    {

        UpdateToggle();
    }

    private void Start()
    {
        TryGetComponent(out _button);
        _button.onClick.AddListener(ToggleMute);

        UpdateToggle();
    }

    private void ToggleMute()
    {
        _muted = !_muted;
        ES3.Save(muteSave, _muted);
        UpdateToggle();
    }

    private void UpdateToggle()
    {
        toggle.SetActive(_muted);
        mixer.SetFloat("Sound", _muted ? -80 : 0);
    }

    private void OnDestroy()
    {
        _button.onClick.RemoveListener(ToggleMute);
    }
}
