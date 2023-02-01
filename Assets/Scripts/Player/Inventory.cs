using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : Singleton<Inventory>
{
    public const string saveKey = "Inventory";
    public List<string> CurrentHeldObjects = new List<string>();

    public System.Action OnUpdateInventory;

    protected override void Awake()
    {
        base.Awake();
        Load();
    }

    public void AddItem(string itemName)
    {
        CurrentHeldObjects.Add(itemName);
        OnUpdateInventory?.Invoke();
    }

    public void RemoveItem(string itemName)
    {
        if (!CurrentHeldObjects.Contains(itemName)) return;
        CurrentHeldObjects.Remove(itemName);
        OnUpdateInventory?.Invoke();
    }

    protected void Save()
    {
        //ES3.Save(saveKey, CurrentHeldObjects);
    }

    public bool HasItem(string itemName)
    {
        return CurrentHeldObjects.Contains(itemName);
    }

    protected void Load()
    {
        // if (ES3.KeyExists(saveKey))
        //     ES3.LoadInto(saveKey, CurrentHeldObjects);

        OnUpdateInventory?.Invoke();
    }
}
