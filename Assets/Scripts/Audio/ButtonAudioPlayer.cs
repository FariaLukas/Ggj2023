using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonAudioPlayer : AudioPlayer
{
    private Button _button;

    private void Awake()
    {
        TryGetComponent(out _button);
        if (_button)
            _button.onClick.AddListener(PlaySfx);
    }

    private void OnDestroy()
    {
        if (_button)
            _button.onClick.RemoveListener(PlaySfx);
    }
}
