using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using Sirenix.OdinInspector;

namespace HamsterRacing.Waypoints
{
    public enum WaypointType { Cyclic, BackAndForth }
    public class WaypointSystem : MonoBehaviour
    {
        public List<Vector3> positions => waypoints;
        [SerializeField] private WaypointType type;
        [SerializeField] private bool useTransformPosition = true;
        [SerializeField] private List<Vector3> waypoints = new List<Vector3>();

        [Header("Gizmo")]
        [SerializeField] private float labelHeight;
        [SerializeField] private float radius = .5f;
        [SerializeField] private int fontSize = 22;

        private int _currentWaypoint;
        private bool _reverse;

        private void Awake()
        {
            if (!useTransformPosition) return;

            for (int i = 0; i < waypoints.Count; i++)
            {
                waypoints[i] += transform.position;
            }
        }

        public Vector3 GetNextWaypoint()
        {
            if (type == WaypointType.Cyclic)
            {
                _currentWaypoint++;
                if (_currentWaypoint >= waypoints.Count)
                    _currentWaypoint = 0;
            }
            else
            {
                if (_reverse)
                {
                    _currentWaypoint--;
                    if (_currentWaypoint <= 0)
                    {
                        _currentWaypoint = 0;
                        _reverse = false;
                    }
                }
                else
                {
                    _currentWaypoint++;
                    if (_currentWaypoint >= waypoints.Count - 1)
                    {
                        _currentWaypoint = waypoints.Count - 1;
                        _reverse = true;
                    }
                }
            }

            return CurrentWaypoint();
        }

        public Vector3 CurrentWaypoint()
        {
            return GetWaypoint(_currentWaypoint);
        }

#if UNITY_EDITOR
        private void OnDrawGizmos()
        {
            for (int i = 0; i < waypoints.Count; i++)
            {
                Gizmos.DrawWireSphere(GetWaypoint(i), radius);

                if (i < waypoints.Count - 1)
                {
                    Gizmos.DrawLine(GetWaypoint(i), GetWaypoint(i + 1));
                }
                else if (type == WaypointType.Cyclic)
                {
                    Gizmos.DrawLine(GetWaypoint(i), GetWaypoint(0));
                }

                GUIStyle style = new GUIStyle();
                style.fontSize = fontSize;
                Handles.Label(GetWaypoint(i) + Vector3.up * labelHeight, i.ToString(), style);
            }

        }
#endif

        private Vector3 GetWaypoint(int id)
        {
            var position = Vector3.zero;

            if (waypoints.Count > id)
            {
                return Application.isPlaying ? waypoints[id] :
                waypoints[id] + transform.position;
            }

            return position;
        }
    }

}
