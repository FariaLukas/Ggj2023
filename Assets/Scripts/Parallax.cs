using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class Parallax : MonoBehaviour
{
    [SerializeField] private float scrollSpeed = 0.3f;
    [SerializeField] private float radius = 1f;
    [SerializeField] private float minDist = .1f;

    private Vector2 _newDirection;
    private float _startTime;
    private float _journeyLength;
    private void Start()
    {
        ChooseNewDirection();
    }

    private void Update()
    {
        float distCovered = (Time.time - _startTime) * scrollSpeed;
        float fractionOfJourney = distCovered / _journeyLength;
        transform.position = Vector2.Lerp(transform.position, _newDirection, fractionOfJourney);
        if (Vector2.Distance(transform.position, _newDirection) < minDist)
        {
            ChooseNewDirection();
        }
    }

    protected void ChooseNewDirection()
    {
        _newDirection = Random.insideUnitCircle + (Vector2)transform.parent.position;
        _startTime = Time.time;
        _journeyLength = Vector3.Distance(transform.position, _newDirection);
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, radius);
    }
}
