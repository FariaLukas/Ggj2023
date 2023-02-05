using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxMovable : MonoBehaviour, IResetable
{
    [SerializeField] private AudioPlayer Audio;
    [SerializeField] private bool StartCollected;

    private Rigidbody2D _boxRB;
    private Vector2 _initialPosition;
    private bool _collected;

    public bool Colected()
    {
        return _collected;
    }

    public void SetCollected()
    {
        _collected = true;
    }

    public void OnReset()
    {
        if (Colected()) return;
        transform.position = _initialPosition;
    }

    private void Start()
    {
        _initialPosition = transform.position;
        if (StartCollected)
            SetCollected();
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
