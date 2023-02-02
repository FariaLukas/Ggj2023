using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Sirenix.OdinInspector;

public class FloodgatesPipe : MonoBehaviour
{

    public bool debug = false;
    public bool notSetupOnAwake;
    protected List<Floodgate> _floodgates = new List<Floodgate>();

    protected virtual void Awake()
    {
        AddAllFloodgates();
        if (notSetupOnAwake) return;
        SetupFloodgates();
    }

    private void OnDestroy()
    {
        ClearFloodgatesSetup();
        ClearFloodgates();
    }

    public void ResetFloodgates()
    {
        ClearFloodgates();
    }

    [ShowInInspector]
    public virtual bool IsOpen()
    {
        foreach (var floodgate in _floodgates)
            if (!floodgate.IsOpen())
                return false;

        return true;
    }

    private void ClearFloodgates()
    {
        foreach (var floodgate in _floodgates)
            floodgate.Clear();
    }

    public void SetupFloodgates()
    {
        foreach (var floodgate in _floodgates)
            floodgate.Setup();
    }

    public void ClearFloodgatesSetup()
    {
        _floodgates.ForEach(f => f.ClearSetup());
    }

    public virtual void AddAllFloodgates()
    {
        _floodgates = GetComponents<Floodgate>().ToList();
    }

}