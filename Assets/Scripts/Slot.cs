using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class Slot : MonoBehaviour, IDragHandler, IEndDragHandler, IPointerEnterHandler, IPointerExitHandler
{
    public ItemData itemData;

    private Transform tempParentForSlots;
    private InventoryManager inventoryManager;
    private Controller controller;
    private string parentName;

    public void OnPointerEnter(PointerEventData eventData)
    {
        controller.itemYouCanEquipName = itemData.name;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        controller.itemYouCanEquipName = Controller.EQUIPE_NOT_SELECTED_TEXT;
    }

    private void Start()
    {
        tempParentForSlots = GameObject.Find("Canvas").transform;
        inventoryManager = FindObjectOfType<InventoryManager>();
        controller = FindObjectOfType<Controller>();
    }
    public void OnDrag(PointerEventData data)
    {
        transform.SetParent(tempParentForSlots);
        transform.position = data.position;
    }

    private void AddToListOnDrag(List<GameObject> slots, List<ItemData> items, Transform parent)
    {
        if (itemData == null) return;
        if (itemData.isUniq || slots.Count == 0)
        {
            slots.Add(gameObject);
            items.Add(itemData);
            transform.SetParent(parent);
            parentName = transform.parent.name;
        }
        else if (!itemData.isUniq)
        {
            for (int i = 0; i < slots.Count; i++)
            {
                if (slots[i].GetComponent<Slot>().itemData.id == itemData.id)
                {
                    items[i].count += itemData.count;
                    slots[i].transform.Find("ItemCountText").GetComponent<Text>().text = slots[i].GetComponent<Slot>().itemData.count.ToString();
                    Destroy(gameObject);
                    break;
                }
                else if (i == slots.Count - 1)
                {
                    slots.Add(gameObject);
                    items.Add(itemData);
                    transform.SetParent(parent);
                    parentName = transform.parent.name;
                    break;

                }
            }
        }
    }

    public void OnEndDrag(PointerEventData data)
    {
        float sicDistance = Vector3.Distance(transform.position
            , inventoryManager.invContent.transform.position);
        float sccDistance = Vector3.Distance(transform.position
            , inventoryManager.chestContent.transform.position);
        if (sicDistance < sccDistance)
        {
            if (parentName == "InventoryContent")
            {
                transform.SetParent(inventoryManager.invContent.transform);
            }
            else
            {
                inventoryManager.chestSlots.Remove(gameObject);
                controller.currentChestItems.Remove(itemData);
                AddToListOnDrag(inventoryManager.inventorySlots,
                    controller.inventoryItem,
                    inventoryManager.invContent.transform);
            }
        }
        else
        {
            if (parentName == "ChestContent")
            {
                transform.SetParent(inventoryManager.chestContent.transform);
            }
            else
            {
                inventoryManager.inventorySlots.Remove(gameObject);
                controller.inventoryItem.Remove(itemData);
                AddToListOnDrag(inventoryManager.chestSlots,
                    controller.currentChestItems,
                    inventoryManager.chestContent.transform);
            }
        }
    }
}