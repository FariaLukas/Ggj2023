using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class InventoryDisplayer : MonoBehaviour
{
    [SerializeField] private Image Prefab;
    [SerializeField] private Transform Parent;

    private Dictionary<PickableData, Image> _prefabs = new Dictionary<PickableData, Image>();

    private void Start()
    {
        var seeds = FindObjectsOfType<Pickable>();

        foreach (var seed in seeds)
        {
            var item = Instantiate(Prefab, Parent);
            item.sprite = seed.Data.DisplayerOff;
            _prefabs.Add(seed.Data, item);
        }

        if (Inventory.Instance)
            Inventory.Instance.OnUpdateInventory += UpdateDisplayingItems;
    }

    private void UpdateDisplayingItems()
    {

        foreach (var seed in _prefabs)
        {
            if (Inventory.Instance.CurrentHeldObjects.Contains(seed.Key.Identifier))
            {
                seed.Value.sprite = seed.Key.Displayer;
            }
            else if (seed.Value.sprite == seed.Key.Displayer)
            {
                seed.Value.sprite = seed.Key.DisplayerUsed;
            }
        }
    }

    private void OnDestroy()
    {
        if (Inventory.Instance)
            Inventory.Instance.OnUpdateInventory -= UpdateDisplayingItems;
    }
}
