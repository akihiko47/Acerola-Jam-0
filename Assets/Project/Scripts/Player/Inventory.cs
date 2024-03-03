using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory {

    private List<Item> itemList;

    public Inventory() {
        itemList = new List<Item>();
    }

    public void AddItem(Item item) {
        itemList.Add(item);
        Debug.Log(itemList.Count);
    }

    public void RemoveItem(Item item) {
        Item itemToRemove = null;
        foreach (Item inventoryItem in itemList) {
            if (inventoryItem.itemType == item.itemType) {
                itemToRemove = inventoryItem;
            }
        }
        if (itemToRemove != null) {
            itemList.Remove(itemToRemove);
        }
        Debug.Log(itemList.Count);
    }

    public bool IsInInventory(Item item) {
        bool isIn = false;
        foreach (Item inventoryItem in itemList) {
            if (inventoryItem.itemType == item.itemType) {
                isIn = true;
            }
        }
        return isIn;
    }

}
