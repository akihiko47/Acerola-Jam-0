using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndGame : MonoBehaviour {

    private PlayerMovement playerMovement;

    private void Start() {
        playerMovement = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMovement>();
    }

    private void OnTriggerEnter(Collider other) {
        playerMovement.enabled = false;
        ScenesManager.Instance.LoadNextScene();
    }

}
