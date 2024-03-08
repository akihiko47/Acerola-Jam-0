using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

public class Panel : MonoBehaviour, IInteractable, IItemNeeder {

    [SerializeField]
    LightsManager lightsManager;

    [SerializeField]
    PostProcessLayer postEffects;

    [SerializeField]
    GameObject hintText;

    [SerializeField]
    GameObject endGame;

    [SerializeField]
    EnemyManager enemyManager;

    private bool canInteract = true;


    public void OnInteract() {
        Debug.Log("Light are now on!");
        lightsManager.ActivateLights();
        hintText.SetActive(true);
        endGame.SetActive(true);
        postEffects.enabled = false;
        enemyManager.StopAttack();
        enemyManager.SetLightsOn(true);
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
