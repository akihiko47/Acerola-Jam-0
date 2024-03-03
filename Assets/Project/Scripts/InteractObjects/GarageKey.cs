using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Key : MonoBehaviour, IInteractable, IPickAble {

    private bool canInteract = true;

    public void OnInteract() {
        Destroy(gameObject);
    }

    public bool IsInteractable() {
        return canInteract;
    }

    public string GetMessage() {
        return "This key can help me to get out!";
    }

    public Item GetItem() {
        return new Item { itemType = Item.ItemType.GarageKey };
    }
}
