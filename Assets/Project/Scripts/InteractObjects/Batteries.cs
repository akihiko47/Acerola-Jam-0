using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Batteries : MonoBehaviour, IInteractable, IPickAble, IItemNeeder {

    private bool canInteract = true;

    public void OnInteract() {
        Destroy(gameObject);
    }

    public bool IsInteractable() {
        return canInteract;
    }

    public string GetMessage() {
        return "I can pull batteries out of this thing, but i need a tool.";
    }

    public Item GetNeededItem() {
        return new Item { itemType = Item.ItemType.Screwdriver };
    }

    public Item GetItem() {
        return new Item { itemType = Item.ItemType.Batteries };
    }
}