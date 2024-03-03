using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IInteractable {
    void OnInteract();

    bool IsInteractable();

    string GetMessage();

}

public interface IPickAble {

}

public class Interactor : MonoBehaviour {

    [SerializeField]
    private Transform interactorSource;

    [SerializeField]
    private float interactRange;

    private InteractorUI interactorUI;

    private void Start() {
        interactorUI = GetComponent<InteractorUI>();
    }

    private void Update() {
        if (Physics.Raycast(interactorSource.position, interactorSource.forward, out RaycastHit hitInfo, interactRange)) {

            if (hitInfo.collider.gameObject.TryGetComponent(out IInteractable interactObj)) {

                interactorUI.ShowInfo(interactObj.GetMessage());

                if (Input.GetKeyDown(KeyCode.E)) {
                    if (interactObj.IsInteractable()) {
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
