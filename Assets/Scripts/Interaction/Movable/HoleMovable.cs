using System.Collections;
using System.Collections.Generic;
using HamsterRacing.Waypoints;
using UnityEngine;

public class HoleMovable : Hole
{
    [SerializeField] private float Speed = 5;
    [SerializeField] private float MinDistance = .2f;
    [SerializeField] private WaypointSystem waypoints;
    [SerializeField] private List<GameObject> waypointsDisplayer = new List<GameObject>();
    [SerializeField] private bool Lerp;

    private Vector3 _targetPosition;
    private float _startTime;
    private float _journeyLength;

    private void Start()
    {
        while (waypointsDisplayer.Count < waypoints.positions.Count)
        {
            var p = Instantiate(waypointsDisplayer[0], transform);
            waypointsDisplayer.Add(p);
        }

        for (int i = 0; i < waypoints.positions.Count; i++)
        {
            waypointsDisplayer[i].transform.position = waypoints.positions[i];
        }

        if (waypointsDisplayer.Count > waypoints.positions.Count)
            for (int i = waypoints.positions.Count - 1; i < waypointsDisplayer.Count; i++)
            {
                waypointsDisplayer[i].SetActive(false);
            }

        waypointsDisplayer.ForEach(w => w.transform.parent = null);
    }

    private void Update()
    {
        if (Vector3.Distance(transform.position, waypoints.CurrentWaypoint()) <= MinDistance)
        {
            _targetPosition = waypoints.GetNextWaypoint();
            _startTime = Time.time;
            _journeyLength = Vector3.Distance(transform.position, _targetPosition);
        }

        Vector3 nextPosition = Vector3.zero;

        if (Lerp)
        {
            float distCovered = (Time.time - _startTime) * Speed / 10;
            float fractionOfJourney = distCovered / _journeyLength;
            nextPosition = Vector3.Lerp(transform.position, _targetPosition, fractionOfJourney);
        }
        else
            nextPosition = Vector3.MoveTowards(transform.position, _targetPosition, Speed * Time.deltaTime);

        transform.position = nextPosition;
    }
}


