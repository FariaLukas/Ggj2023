using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public static class Observer
{
    private static Action<ObserverEvent, object> subscriptions;

    public static void Subscribe(Action<ObserverEvent, object> method)
    {
        subscriptions += method;
    }

    public static void UnSubscribe(Action<ObserverEvent, object> method)
    {
        subscriptions -= method;
    }

    public static void Notify(ObserverEvent tag, object value = null)
    {
        subscriptions.Invoke(tag, value);
    }
}
