using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Key : MonoBehaviour, IInteractable {

    private bool canInteract = true;

    public void OnInteract() {
        Debug.Log("Interaction");
        Destroy(gameObject);
    }

    public bool IsInteractable() {
        return canInteract;
    }

    public string GetMessage() {
        return "This key can help me to get out!";
    }
}
