using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;

public class UIInGame : MonoBehaviour {

    [SerializeField]
    private GameObject SettingsPanel;

    [SerializeField]
    private AudioMixer masterMixer;

    void Awake() {
        SettingsPanel.SetActive(false);


    }

    private void Update() {
        if (Input.GetKeyDown(KeyCode.Escape) && !SettingsPanel.activeSelf) {
            ActivateSettingsPanel();
        }
    }

    public void ActivateSettingsPanel() {
        Time.timeScale = 0f;
        SoundManager.PlaySound(SoundManager.Sound.buttonClick);
        SettingsPanel.SetActive(true);
        Cursor.lockState = CursorLockMode.None;
    }

    public void DeactivateSettingsPanel() {
        Time.timeScale = 1f;
        SoundManager.PlaySound(SoundManager.Sound.buttonClick);
        SettingsPanel.SetActive(false);
        Cursor.lockState = CursorLockMode.Locked;
    }

    public void SetMusicVolume (float volume) {
        masterMixer.SetFloat("Music Volume", Mathf.Log10(volume) * 20);
    }

    public void SetSoundsVolume (float volume) {
        masterMixer.SetFloat("SFX Volume", Mathf.Log10(volume) * 20);
    }

    public void QuitGame() {
        Application.Quit();
    }
}

