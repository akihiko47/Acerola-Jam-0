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

    private bool isWorking = true;


    private void Start() {
        maxIntensity = flashlight.intensity;
    }

    void Update() {
        if (Input.GetMouseButtonDown(0)) {
            if (isWorking) {
                SoundManager.PlaySound(SoundManager.Sound.flashlightCharge);
                flashlight.intensity += clickGain * Time.deltaTime;
                if (flashlight.intensity > maxIntensity) {
                    flashlight.intensity = maxIntensity;
                }
            } else {
                SoundManager.PlaySound(SoundManager.Sound.flashlightBrokenCharge);
            }
        }

        if (flashlight.intensity > 0) {
            flashlight.intensity -= decreaseSpeed * Time.deltaTime;
            if (flashlight.intensity < 0f) {
                flashlight.intensity = 0f;
            }
        }
    }

    public void SetWorking(bool state) {
        isWorking = state;
        if (state == false) {
            SoundManager.PlaySound(SoundManager.Sound.flashlightBreak);
        }
    }

    public bool GetWorking() {
        return isWorking;
    }

    public void HalfIntensity() {
        flashlight.intensity *= 0.2f;
    }
}
