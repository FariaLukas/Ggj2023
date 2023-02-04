using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : Singleton<Inventory>
{
    public const string storageSave = "Awards";
    public List<string> CurrentHeldObjects = new List<string>();
    public List<string> AwardsObjects = new List<string>();

    public System.Action OnUpdateInventory;

    protected override void Awake()
    {
        base.Awake();
        Load();
    }

    public void AddItem(string itemName)
    {
        CurrentHeldObjects.Add(itemName);

        if (!AwardsObjects.Contains(itemName))
            AwardsObjects.Add(itemName);

        OnUpdateInventory?.Invoke();

        Save();
    }

    public void RemoveItem(string itemName)
    {
        if (!CurrentHeldObjects.Contains(itemName)) return;
        CurrentHeldObjects.Remove(itemName);
        OnUpdateInventory?.Invoke();
    }

    protected void Save()
    {
        ES3.Save(storageSave, AwardsObjects);
    }

    public bool HasItem(string itemName)
    {
        return CurrentHeldObjects.Contains(itemName);
    }

    protected void Load()
    {
        if (ES3.KeyExists(storageSave))
        {
            AwardsObjects = ES3.Load<List<string>>(storageSave);
        }

        OnUpdateInventory?.Invoke();
    }
}
