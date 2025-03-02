using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class InventoryManager : MonoBehaviour
{
    [SerializeField]
    private GameObject slotPref;
    public GameObject inventoryPanel, chestPanel;
    public GameObject invContent, chestContent;
    public ItemData[] items;
    public List<GameObject> inventorySlots = new List<GameObject>();
    public List<GameObject> chestSlots = new List<GameObject>();

    private void Awake()
    {
        inventoryPanel = GameObject.Find("CharPanel");
        chestPanel = GameObject.Find("ChestPanel");
        invContent = GameObject.Find("InventoryContent");
        chestContent = GameObject.Find("ChestContent");
    }

    private void Start()
    {
        inventoryPanel.SetActive(false);
        chestPanel.SetActive(false);
    }

    public void CreateItem(int itemId, List<ItemData> itemsList)
    {
        ItemData item = new ItemData(
            items[itemId].name,
            items[itemId].id,
            items[itemId].count,
            items[itemId].isUniq,
            items[itemId].description);
        if (!item.isUniq && itemsList.Count > 0)
        {
            for (int i = 0; i < itemsList.Count; i++)
            {
                if (item.id == itemsList[i].id)
                {
                    itemsList[i].count += 1;
                    break;
                }
                else if (i == itemsList.Count - 1)
                {
                    itemsList.Add(item);
                    break;
                }
            }
        }
        else if (item.isUniq || (!item.isUniq && itemsList.Count == 0))
        {
            itemsList.Add(item);
        }
    }

    public void InstantingItem(ItemData item, Transform panel, List<GameObject> itemList)
    {
        GameObject go = Instantiate(slotPref);
        go.transform.SetParent(panel);
        go.AddComponent<Slot>();
        go.GetComponent<Slot>().itemData = item;
        go.transform.Find("ItemNameText").GetComponent<Text>().text = item.name;
        go.transform.Find("ItemCountText").GetComponent<Text>().text = item.count + "";
        go.transform.Find("ItemImage").GetComponent<Image>().sprite = 
            Resources.Load<Sprite>(item.name);
        Color color = item.isUniq ? Color.clear : Color.white;
        go.transform.Find("ItemCountText").GetComponent<Text>().color = color;
        itemList.Add(go);
    }
}
