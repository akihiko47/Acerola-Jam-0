using UnityEngine;

public class PlayerFlashlight : MonoBehaviour {

    [SerializeField]
    private Light flashlight;

    private bool transition = false;
    private float t = 0f;

    private Color targetColor = Color.black;

    void Update() {
        if (Input.GetMouseButtonDown(0)) {
            if (targetColor == Color.white) {
                targetColor = Color.black;
            } else if (targetColor == Color.black) {
                targetColor = Color.white;
            }
            t = 0f;
            transition = true;
        }

        if (transition) {
            t += Time.deltaTime;
            flashlight.color = Color.Lerp(flashlight.color, targetColor, t);

            if (t > 0.99) {
                t = 1f;
                transition = false;
            }
        }
    }
}
