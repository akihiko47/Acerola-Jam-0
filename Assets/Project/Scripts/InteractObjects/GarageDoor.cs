using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GarageDoor : MonoBehaviour, IInteractable, IItemNeeder {

    [SerializeField]
    private Transform doorVFX;

    [SerializeField]
    private PlayerFlashlight playerFlashlight;

    private bool canInteract = true;

    public void OnInteract() {
        playerFlashlight.SetWorking(false);
        doorVFX.Rotate(new Vector3(0f, -75f, 0f));
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
