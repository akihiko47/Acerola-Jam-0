using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DissolveScript : MonoBehaviour {

    private GameObject[] objects;
    void Start() {

        objects = GameObject.FindGameObjectsWithTag("Dissolve");

        foreach (GameObject obj in objects) {
            obj.GetComponent<Renderer>().material.renderQueue = 3000;
        }

    }

    public void SetDissolveColliders(bool active) {
        objects = GameObject.FindGameObjectsWithTag("Dissolve");

        foreach (GameObject obj in objects) {
            obj.GetComponent<Collider>().enabled = active;
        }
    }
}
