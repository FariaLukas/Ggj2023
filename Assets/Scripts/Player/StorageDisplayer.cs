using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StorageDisplayer : MonoBehaviour
{
    [SerializeField] private SeedStorage Storage;
    [SerializeField] private List<Image> Displayers = new List<Image>();

    private void Start()
    {
        if (Inventory.Instance)
            Inventory.Instance.OnUpdateInventory += UpdateDisplayingItems;

        for (int i = 0; i < Storage.Datas.Count; i++)
        {
            Displayers[i].sprite = Storage.Datas[i].DisplayerWorld;
        }

        UpdateDisplayingItems();
    }

    private void UpdateDisplayingItems()
    {
        for (int i = 0; i < Storage.Datas.Count; i++)
        {
            Displayers[i].gameObject.SetActive(Inventory.
            Instance.AwardsObjects.Contains(Storage.Datas[i].Identifier));
        }
    }

    private void OnDestroy()
    {
        if (Inventory.Instance)
            Inventory.Instance.OnUpdateInventory -= UpdateDisplayingItems;
    }
}
