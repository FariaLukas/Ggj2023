using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class InventoryDisplayer : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI ItemsDisplayer;
    [SerializeField] private string prefix;

    private void Start()
    {
        if (Inventory.Instance)
            Inventory.Instance.OnUpdateInventory += UpdateDisplayingItems;

        UpdateDisplayingItems();
    }

    private void UpdateDisplayingItems()
    {
        ItemsDisplayer.text = "";

        foreach (var item in Inventory.Instance.CurrentHeldObjects)
        {
            ItemsDisplayer.text += $"{prefix}{item}\n";
        }
    }

    private void OnDestroy()
    {
        if (Inventory.Instance)
            Inventory.Instance.OnUpdateInventory -= UpdateDisplayingItems;
    }
}
