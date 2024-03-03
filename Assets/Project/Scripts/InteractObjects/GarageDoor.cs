using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GarageDoor : MonoBehaviour, IInteractable, IItemNeeder {

    private bool canInteract = true;

    public void OnInteract() {
        Destroy(gameObject);
    }

    public bool IsInteractable() {
        return canInteract;
    }

    public string GetMessage() {
        return "This is garage door. I need a key to open it!";
    }

    public Item GetNeededItem() {
        return new Item { itemType = Item.ItemType.GarageKey };
    }
}
