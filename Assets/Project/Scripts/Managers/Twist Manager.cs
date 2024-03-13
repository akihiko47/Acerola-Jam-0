using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class TwistManager : MonoBehaviour {

    private Transform player;
    private PlayerMovement playerMovement;

    [SerializeField]
    private AudioMixer mixer;

    public static TwistManager Instance { get; private set; }

    private void Awake() {
        if (Instance != null && Instance != this) {
            Destroy(this);
        } else {
            Instance = this;
        }
    }

    private void Start() {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        playerMovement = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMovement>();
        Shader.SetGlobalFloat("_Twisting", 0f);
        mixer.SetFloat("Pitch Shift", 1f);
    }

    private void Update() {
        Shader.SetGlobalFloat("_PlayerPosX", player.position.x);
        Shader.SetGlobalFloat("_PlayerPosZ", player.position.z);
    }

    public void StartTwist() {
        playerMovement.DecreaseSpeed();
        StartCoroutine(IStartTwist());
    }

    public void EndTwist() {
        playerMovement.IncreaseSpeed();
        Shader.SetGlobalFloat("_Twisting", 0);
        RenderSettings.fogColor = new Color(0f, 0f, 0f);
        mixer.SetFloat("Pitch Shift", 1f);
    }

    private IEnumerator IStartTwist() {
        float twisting = 0f;
        float color = 0f;
        float pitch = 1f;
        for (int i = 1; i <= 20; i++) {
            twisting += 0.07f;
            color += 0.005f;
            pitch -= 0.025f;
            mixer.SetFloat("Pitch Shift", pitch);
            Shader.SetGlobalFloat("_Twisting", twisting);
            RenderSettings.fogColor = new Color(color, 0f, 0f);

            yield return new WaitForSeconds(0.1f);
        }
    }

}
