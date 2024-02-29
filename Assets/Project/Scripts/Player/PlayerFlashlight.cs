using UnityEngine;

public class PlayerFlashlight : MonoBehaviour {

    [SerializeField]
    private Light flashlight;

    void Update() {
        if (Input.GetMouseButtonDown(0)) {
            flashlight.enabled = !flashlight.enabled;
        }
    }
}
