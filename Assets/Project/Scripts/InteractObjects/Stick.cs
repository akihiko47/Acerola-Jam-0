using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stick : MonoBehaviour, IInteractable, IPickAble {

    private bool canInteract = true;

    public void OnInteract() {
        Destroy(gameObject);
    }

    public bool IsInteractable() {
        return canInteract;
    }

    public string GetMessage() {
        return "With this stick i can reach further";
    }

    public Item GetItem() {
        return new Item { itemType = Item.ItemType.Stick };
    }
}
