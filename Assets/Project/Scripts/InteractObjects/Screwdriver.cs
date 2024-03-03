using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Screwdriver : MonoBehaviour, IInteractable, IPickAble {

    private bool canInteract = true;

    public void OnInteract() {
        Destroy(gameObject);
    }

    public bool IsInteractable() {
        return canInteract;
    }

    public string GetMessage() {
        return "This is a screwdriver";
    }

    public Item GetItem() {
        return new Item { itemType = Item.ItemType.Screwdriver };
    }
}
