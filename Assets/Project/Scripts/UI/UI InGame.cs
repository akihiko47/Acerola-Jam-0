using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;

public class UIInGame : MonoBehaviour {

    [SerializeField]
    private GameObject SettingsPanel;

    [SerializeField]
    private GameObject TutorialPanel;

    [SerializeField]
    private AudioMixer masterMixer;

    [SerializeField]
    private Slider sensitivitySlider;

    [SerializeField]
    private Slider musicSlider;

    [SerializeField]
    private Slider sfxSlider;

    private GameObject player;
    private GameObject playerCam;

    void Awake() {
        SettingsPanel.SetActive(false);


    }

    private void Start() {
        player = GameObject.FindGameObjectWithTag("Player");
        playerCam = GameObject.FindGameObjectWithTag("Player Camera");

        if (PlayerPrefs.HasKey("sensitivity")) {
            float sens = PlayerPrefs.GetFloat("sensitivity");
            playerCam.GetComponent<PlayerCamera>().SetSensitivity(sens);
            sensitivitySlider.value = sens;
        }

        if (PlayerPrefs.HasKey("music volume")) {
            float musicVolume = PlayerPrefs.GetFloat("music volume");
            masterMixer.SetFloat("Music Volume", Mathf.Log10(musicVolume) * 20);
            musicSlider.value = musicVolume;
        }
        
        if (PlayerPrefs.HasKey("sfx volume")) {
            float sfxVolume = PlayerPrefs.GetFloat("sfx volume");
            masterMixer.SetFloat("SFX Volume", Mathf.Log10(sfxVolume) * 20);
            sfxSlider.value = sfxVolume;
        }
    }

    private void Update() {
        if (Input.GetKeyDown(KeyCode.Escape) && !SettingsPanel.activeSelf) {
            ActivateSettingsPanel();
        }

        if (Input.GetKeyDown(KeyCode.Space) && TutorialPanel.activeSelf) {
            DeactivateTutorialPanel();
        }
    }

    public void ActivateSettingsPanel() {
        Time.timeScale = 0f;
        GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerFlashlight>().enabled = false;
        SoundManager.PlaySound(SoundManager.Sound.buttonClick);
        SettingsPanel.SetActive(true);
        Cursor.lockState = CursorLockMode.None;
    }

    public void DeactivateSettingsPanel() {
        Time.timeScale = 1f;
        GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerFlashlight>().enabled = true;
        SoundManager.PlaySound(SoundManager.Sound.buttonClick);
        SettingsPanel.SetActive(false);
        Cursor.lockState = CursorLockMode.Locked;
        PlayerPrefs.Save();
    }

    public void DeactivateTutorialPanel() {
        TutorialPanel.SetActive(false);
    }

    public void SetSensitivity(float sens) {
        playerCam.GetComponent<PlayerCamera>().SetSensitivity(sens);
        PlayerPrefs.SetFloat("sensitivity", sens);
    }

    public void SetMusicVolume (float volume) {
        masterMixer.SetFloat("Music Volume", Mathf.Log10(volume) * 20);
        PlayerPrefs.SetFloat("music volume", volume);
    }

    public void SetSoundsVolume (float volume) {
        masterMixer.SetFloat("SFX Volume", Mathf.Log10(volume) * 20);
        PlayerPrefs.SetFloat("sfx volume", volume);
    }

    public void QuitGame() {
        Application.Quit();
    }
}

