using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IInteractable {
    void OnInteract();

    bool IsInteractable();

    string GetMessage();

}

public interface IPickAble {
    Item GetItem();
}

public interface IItemNeeder {
    Item GetNeededItem();
}

public class Interactor : MonoBehaviour {

    [SerializeField]
    private Transform interactorSource;

    [SerializeField]
    private float interactRange;

    private InteractorUI interactorUI;

    private Inventory inventory;

    private void Start() {
        interactorUI = GetComponent<InteractorUI>();
        inventory = new Inventory();
    }

    private void Update() {
        //  if we hit something
        if (Physics.Raycast(interactorSource.position, interactorSource.forward, out RaycastHit hitInfo, interactRange)) {

            //  if this item is interactable
            if (hitInfo.collider.gameObject.TryGetComponent(out IInteractable interactObj)) {

                //  show message
                interactorUI.ShowInfo(interactObj.GetMessage());


                //  if object can be iteracted - iteract
                if (Input.GetKeyDown(KeyCode.E) && interactObj.IsInteractable()) {

                    IItemNeeder itemNeeder = hitInfo.collider.gameObject.GetComponent<IItemNeeder>();
                    IPickAble pickable = hitInfo.collider.gameObject.GetComponent<IPickAble>();


                    if ( itemNeeder != null && pickable != null) {
                        //  if object need items and can be picked up

                        Item neededItem = itemNeeder.GetNeededItem();
                        //  interact only if player has item
                        if (inventory.IsInInventory(neededItem)) {
                            inventory.AddItem(pickable.GetItem());
                            inventory.RemoveItem(neededItem);
                            SoundManager.PlaySound(SoundManager.Sound.itemPickup);
                            interactObj.OnInteract();
                        }

                    } else if (itemNeeder != null) {
                        //  if object need item

                        Item neededItem = itemNeeder.GetNeededItem();
                        //  interact only if player has item
                        if (inventory.IsInInventory(neededItem)) {
                            inventory.RemoveItem(neededItem);
                            SoundManager.PlaySound(SoundManager.Sound.itemUse);
                            interactObj.OnInteract();
                        }

                    } else if (pickable != null) {
                        //  if object can be picked up

                        inventory.AddItem(pickable.GetItem());
                        SoundManager.PlaySound(SoundManager.Sound.itemPickup);
                        interactObj.OnInteract();

                    } else {
                        //  object doesnt need anything

                        SoundManager.PlaySound(SoundManager.Sound.itemUse);
                        interactObj.OnInteract();
                    }
                }

            } else if (interactorUI.IsActive()) {
                interactorUI.HideInfo();
            }

        } else if (interactorUI.IsActive()) {
            interactorUI.HideInfo();
        }
    }
}
