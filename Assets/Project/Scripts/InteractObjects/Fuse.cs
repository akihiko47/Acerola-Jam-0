using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fuse : MonoBehaviour, IInteractable, IPickAble, IItemNeeder {

    private bool canInteract = true;

    public void OnInteract() {
        Destroy(gameObject);
    }

    public bool IsInteractable() {
        return canInteract;
    }

    public string GetMessage() {
        return "With this part i can fix the light, but it's too high";
    }

    public Item GetNeededItem() {
        return new Item { itemType = Item.ItemType.Stick };
    }

    public Item GetItem() {
        return new Item { itemType = Item.ItemType.Fuse };
    }
}