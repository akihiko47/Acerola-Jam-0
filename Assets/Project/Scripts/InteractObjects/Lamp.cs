using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lamp : MonoBehaviour, IInteractable, IItemNeeder {

    [SerializeField]
    private Light lampLight;

    private bool canInteract = true;

    public void OnInteract() {
        lampLight.enabled = true;
    }

    public bool IsInteractable() {
        return canInteract;
    }

    public string GetMessage() {
        return "This is my parent's lamp. I need batteries to turn it on.";
    }

    public Item GetNeededItem() {
        return new Item { itemType = Item.ItemType.Batteries };
    }
}
