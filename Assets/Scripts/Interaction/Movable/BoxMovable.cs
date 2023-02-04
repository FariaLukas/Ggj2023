using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxMovable : MonoBehaviour
{
    [SerializeField] private AudioPlayer Audio;

    private Rigidbody2D _boxRB;

    private void Start()
    {
        TryGetComponent(out _boxRB);
    }

    private void Update()
    {
        if (!_boxRB || !Audio) return;

        if (_boxRB.velocity != Vector2.zero)
        {
            Audio.PlaySfx();
        }
        else
        {
            Audio.StopAudio();
        }
    }
}
