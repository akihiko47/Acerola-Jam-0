using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightFlicker : MonoBehaviour {

    [SerializeField]
    private float maxWait = 1;

    [SerializeField]
    private float maxFlicker = 0.2f;

    private Light myLight;

    private float timer;
    private float interval;

    private void Start() {
        myLight = GetComponent<Light>();
    }

    void Update() {
        timer += Time.deltaTime;
        if (timer > interval) {
            ToggleLight();
        }
    }

    void ToggleLight() {
        myLight.enabled = !myLight.enabled;
        if (myLight.enabled) {
            interval = Random.Range(0, maxWait);
        } else {
            interval = Random.Range(0, maxFlicker);
        }

        timer = 0;
    }
}
