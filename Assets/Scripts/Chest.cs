using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chest : MonoBehaviour
{
    public List<ItemData> chestItems = new List<ItemData>();
    private void Awake()
    {
        InventoryManager inventoryManager = GameObject.Find("InventoryManager")
            .GetComponent<InventoryManager>();
        int itemCountainChest = Random.Range(3, 7);
        for(int i = 0; i < itemCountainChest; i++)
        {
            inventoryManager.CreateItem(
                Random.Range(0, inventoryManager.items.Length),
                chestItems);
        }
    }
}
