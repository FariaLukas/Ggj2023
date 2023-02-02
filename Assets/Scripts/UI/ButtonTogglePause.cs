using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonTogglePause : MonoBehaviour
{
    private Button _button;

    private void Start()
    {
        TryGetComponent(out _button);
        _button.onClick.AddListener(ToggelPause);
    }

    private void ToggelPause()
    {
        GameManager.Instance?.TogglePause();
    }

    private void OnDestroy()
    {
        _button.onClick.RemoveListener(ToggelPause);
    }
}
