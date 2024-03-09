using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lamp : MonoBehaviour, IInteractable, IItemNeeder {

    [SerializeField]
    private Light lampLight;

    [SerializeField]
    private PlayerFlashlight playerFlashlight;

    private bool canInteract = true;

    private void Update() {
        if (playerFlashlight.GetWorking() == false) {
            float lampDist = (transform.position - playerFlashlight.transform.position).magnitude;
            bool playerNearLamp = (lampDist <= lampLight.range) && lampLight.enabled;

            if (playerNearLamp) {
                playerFlashlight.SetWorking(true);
            }
        }
    }

    public void OnInteract() {
        lampLight.enabled = true;
    }

    public bool IsInteractable() {
        return canInteract;
    }

    public string GetMessage() {
        return "Our lamp! But it's out of batteries :c";
    }

    public Item GetNeededItem() {
        return new Item { itemType = Item.ItemType.Batteries };
    }
}
