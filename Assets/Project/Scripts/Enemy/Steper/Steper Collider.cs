using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SteperCollider : MonoBehaviour {

    Steper steperScript;

    private void Start() {
        steperScript = transform.parent.GetComponent<Steper>();
    }

    private void OnTriggerEnter(Collider other) {
        if (other.tag == "Player") {
            steperScript.StartSteps();
        }
    }
}
