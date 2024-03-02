using UnityEngine;

public class PlayerFlashlight : MonoBehaviour {

    [SerializeField]
    private Light flashlight;

    [SerializeField]
    DissolveScript dissolveScript;

    private bool transition = false;
    private float t = 0f;

    private Color targetColor = Color.black;

    void Update() {
        if (Input.GetMouseButtonDown(0)) {
            if (targetColor == Color.white) {
                targetColor = Color.black;
                dissolveScript.SetDissolveColliders(false);
            } else if (targetColor == Color.black) {
                targetColor = Color.white;
                dissolveScript.SetDissolveColliders(true);
            }
            t = 0f;
            transition = true;
        }

        if (transition) {
            t += Time.deltaTime;
            flashlight.color = Color.Lerp(flashlight.color, targetColor, t);

            if (t > 0.95) {
                t = 1f;
                transition = false;
            }
        }
    }
}
