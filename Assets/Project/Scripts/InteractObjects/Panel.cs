using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Panel : MonoBehaviour, IInteractable, IItemNeeder {

    private bool canInteract = true;

    public void OnInteract() {
        Debug.Log("Light are now on!");
    }

    public bool IsInteractable() {
        return canInteract;
    }

    public string GetMessage() {
        return "My father told me that i need something to make this thing work";
    }

    public Item GetNeededItem() {
        return new Item { itemType = Item.ItemType.Fuse };
    }
}
