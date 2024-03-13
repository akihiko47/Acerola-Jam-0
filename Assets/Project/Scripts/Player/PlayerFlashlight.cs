using UnityEngine;

public class PlayerFlashlight : MonoBehaviour {

    [SerializeField]
    private Light flashlight;

    [SerializeField]
    private GameObject flashlightModel;

    [SerializeField]
    private ParticleSystem flashlightSparks;

    [SerializeField]
    private SpriteRenderer lightHalo;

    [SerializeField]
    private GameObject flashlightModelCopy;

    [SerializeField]
    private Transform flashlightHandle;

    [SerializeField]
    private float decreaseSpeed = 1f;

    [SerializeField]
    private float clickGain = 5f;

    [SerializeField]
    DissolveScript dissolveScript;

    private float maxIntensity;

    private bool isWorking = true;


    private void Start() {
        flashlightModel.SetActive(false);
        maxIntensity = flashlight.intensity;
    }

    void Update() {
        if (Input.GetMouseButtonDown(0)) {
            if (isWorking) {
                flashlightHandle.localRotation = Quaternion.Euler(0f, 0f, 0f);
                SoundManager.PlaySound(SoundManager.Sound.flashlightCharge);
                flashlight.intensity += clickGain;
                if (flashlight.intensity > maxIntensity) {
                    flashlight.intensity = maxIntensity;
                }
            } else {
                SoundManager.PlaySound(SoundManager.Sound.flashlightBrokenCharge);
            }
        }

        if (Input.GetMouseButtonUp(0)) {
            flashlightHandle.localRotation = Quaternion.Euler(-30f, 0f, 0f);
        }

        if (flashlight.intensity > 0) {
            lightHalo.color = new Color(1f, 0.964f, 0.768f, (flashlight.intensity / maxIntensity));
            flashlight.intensity -= decreaseSpeed * Time.deltaTime;
            if (flashlight.intensity < 0f) {
                flashlight.intensity = 0f;
            }
        }
    }

    public void SetWorking(bool state) {
        isWorking = state;
        if (state == false) {
            flashlightSparks.Play();
            SoundManager.PlaySound(SoundManager.Sound.flashlightBreak);
        } else {
            flashlightSparks.Stop();
        }
    }

    public bool GetWorking() {
        return isWorking;
    }

    public void HalfIntensity() {
        flashlight.intensity *= 0.2f;
    }

    public void ConfigureFlashlightMovement() {
        flashlightModel.SetActive(true);
        flashlightModelCopy.SetActive(false);
    }
}
