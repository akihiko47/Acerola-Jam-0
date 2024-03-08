using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightsManager : MonoBehaviour {

    [SerializeField]
    GameObject lampsFolder;

    public void ActivateLights() {
        foreach (Transform child in lampsFolder.transform) {
            Light light = child.GetChild(0).GetComponent<Light>();
            light.enabled = true;
        }
    }

    public void DeactivateLights() {
        foreach (Transform child in lampsFolder.transform) {
            Light light = child.GetChild(0).GetComponent<Light>();
            light.enabled = false;
        }
    }

}
