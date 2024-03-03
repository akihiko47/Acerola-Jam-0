using UnityEngine;

public class PlayerFlashlight : MonoBehaviour {

    [SerializeField]
    private Light flashlight;

    [SerializeField]
    private float decreaseSpeed = 1f;

    [SerializeField]
    private float clickGain = 5f;

    [SerializeField]
    DissolveScript dissolveScript;

    private float maxIntensity;


    private void Start() {
        maxIntensity = flashlight.intensity;
    }

    void Update() {
        if (Input.GetMouseButtonDown(0)) {
            flashlight.intensity += clickGain;
            if (flashlight.intensity > maxIntensity) {
                flashlight.intensity = maxIntensity;
            }
        }

        if (flashlight.intensity > 0) {
            flashlight.intensity -= decreaseSpeed;
            if (flashlight.intensity < 0f) {
                flashlight.intensity = 0f;
            }
        }
    }
}
