using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;


public static class UIRaycastExtensions
{
    public static bool IsAboveUI(this Vector2 position)
    {
        return IsAboveUI(new Vector3(position.x, position.y, 0));
    }

    public static bool IsAboveUI(this Vector3 position)
    {
        PointerEventData eventPosition = new PointerEventData(EventSystem.current);
        eventPosition.position = position;

        List<RaycastResult> results = new List<RaycastResult>();
        EventSystem.current.RaycastAll(eventPosition, results);

        foreach (var r in results)
        {
            if (r.gameObject != null)
                Debug.Log(r.gameObject.name);
        }

        return results.Count > 0;
    }

}


