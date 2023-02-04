using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public enum Movement { Normal, AddForce, Lerp }
    [SerializeField] private Movement MovementType = Movement.AddForce;
    [SerializeField] private Rigidbody2D PlayerRigidbody;
    [SerializeField] private bool NotUsingKeyboard;

    [SerializeField] private float MovementSpeed = 12;
    [SerializeField] private float LerpSpeed = 2.0f;
    [SerializeField] private Vector2 MinSpeed;
    [SerializeField] private Vector2 XDeadzone = new Vector2(-.05f, .05f), YDeadzone = new Vector2(-.05f, .05f);
    [SerializeField] private AudioPlayer Audio;
    [SerializeField] private Animator Animator;

    [SerializeField] private FloodgatesPipe _pipe;
    private Vector3 _lerpAcceleration;
    private Vector3 _acceleration;

    private void Awake()
    {
        if (!PlayerRigidbody)
            TryGetComponent(out PlayerRigidbody);

#if UNITY_EDITOR
        if (!NotUsingKeyboard)
            MovementSpeed /= 2;
#endif
    }

    private void Update()
    {
        if (!_pipe.IsOpen())
        {
            _lerpAcceleration = _acceleration = Vector3.zero;
            Audio.StopAudio();
            Animator.SetBool("Moving", false);
            return;
        }

        _acceleration = Input.acceleration;
        _acceleration.x = _acceleration.x > XDeadzone.x && _acceleration.x < XDeadzone.y ? 0 : _acceleration.x;
        _acceleration.y = _acceleration.y > YDeadzone.x && _acceleration.y < YDeadzone.y ? 0 : _acceleration.y;
        _acceleration.z = 0;

        if (MovementType == Movement.AddForce) return;

        if (MovementType == Movement.Lerp)
        {
            LerpMovement();
            return;
        }

        Filter();
    }

    private void FixedUpdate()
    {
        if (!_pipe.IsOpen())
        {
            return;
        }

        if (MovementType != Movement.AddForce) return;

        _lerpAcceleration = Vector3.Lerp(_lerpAcceleration, _acceleration, LerpSpeed * Time.fixedDeltaTime);

        var dir = new Vector2(_lerpAcceleration.x, _lerpAcceleration.y);

        if (dir.sqrMagnitude > 1) dir.Normalize();

        bool moving = _acceleration != Vector3.zero;
#if UNITY_EDITOR

        if (!NotUsingKeyboard)
        {
            dir = new Vector3(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"), 0);
            moving = dir != Vector2.zero;
        }
#endif
        if (moving)
        {
            Audio.PlaySfx();
        }
        else
        {
            Audio.StopAudio();
        }

        Animator.SetBool("Moving", moving);

        transform.rotation = Quaternion.LookRotation(Vector3.forward, moving ? dir : Vector2.zero);
        PlayerRigidbody.MovePosition((Vector2)transform.position + dir * MovementSpeed * Time.fixedDeltaTime);
    }

    protected void AddForce()
    {
        Vector3 acceleration = Input.acceleration;

        PlayerRigidbody.AddForce(acceleration * MovementSpeed);

        var velocityX = PlayerRigidbody.velocity.x;

        if (velocityX > MinSpeed.x)
            velocityX = MinSpeed.x;

        if (velocityX < -MinSpeed.x)
            velocityX = -MinSpeed.x;

        var velocityY = PlayerRigidbody.velocity.y;

        if (velocityY > MinSpeed.y)
            velocityY = MinSpeed.y;

        if (velocityY < -MinSpeed.y)
            velocityY = -MinSpeed.y;

        PlayerRigidbody.velocity = new Vector2(velocityX, velocityY);
    }

    protected void Filter()
    {
        Vector3 aNew = Input.acceleration;
        var a = (0.1f * aNew);
        a += (0.9f * a);
        a.z = 0f;

        transform.Translate(a * MovementSpeed * Time.deltaTime);
    }

    public void LerpMovement()
    {
        _lerpAcceleration = Vector3.Lerp(_lerpAcceleration, Acceleration(), LerpSpeed * Time.deltaTime);

        var dir = new Vector2(_lerpAcceleration.x, _lerpAcceleration.y);

        if (dir.sqrMagnitude > 1) dir.Normalize();

        transform.Translate(dir * MovementSpeed * Time.deltaTime);
    }

    protected Vector3 Acceleration()
    {
        Vector3 acceleration = Input.acceleration;
        acceleration.x = acceleration.x > XDeadzone.x && acceleration.x < XDeadzone.y ? 0 : acceleration.x;
        acceleration.y = acceleration.y > YDeadzone.x && acceleration.y < YDeadzone.y ? 0 : acceleration.y;
        acceleration.z = 0;
        return acceleration;
    }

    protected Vector2 SetMinValue(Vector2 dir)
    {
        var velocityX = dir.x;

        if (velocityX > 0)
        {
            if (velocityX < MinSpeed.x)
                velocityX = MinSpeed.x;
        }
        else
        {
            if (velocityX > -MinSpeed.x)
                velocityX = -MinSpeed.x;
        }

        var velocityY = dir.y;

        if (velocityY > 0)
        {
            if (velocityY < MinSpeed.y)
                velocityY = MinSpeed.y;
        }
        else
        {
            if (velocityY > -MinSpeed.y)
                velocityY = -MinSpeed.y;
        }

        return new Vector2(velocityX, velocityY);
    }
}
