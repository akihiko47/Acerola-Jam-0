using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;

public class UIMainMenu : MonoBehaviour {

    [SerializeField]
    private GameObject CreditsPanel;

    [SerializeField]
    private GameObject SettingsPanel;

    [SerializeField]
    private AudioMixer masterMixer;

    void Awake() {
        Cursor.lockState = CursorLockMode.None;

        CreditsPanel.SetActive(false);
        SettingsPanel.SetActive(false);
    }

    public void StartNewGame() {
        MusicManager.Instance.StopMusic();
        SoundManager.PlaySound(SoundManager.Sound.buttonClick);
        ScenesManager.Instance.LoadNextScene();
    }

    public void ActivateCreditsPanel() {
        SoundManager.PlaySound(SoundManager.Sound.buttonClick);
        CreditsPanel.SetActive(true);
    }

    public void DeactivateCreditsPanel() {
        SoundManager.PlaySound(SoundManager.Sound.buttonClick);
        CreditsPanel.SetActive(false);
    }

    public void ActivateSettingsPanel() {
        SoundManager.PlaySound(SoundManager.Sound.buttonClick);
        SettingsPanel.SetActive(true);
    }

    public void DeactivateSettingsPanel() {
        SoundManager.PlaySound(SoundManager.Sound.buttonClick);
        SettingsPanel.SetActive(false);
    }

    public void SetMusicVolume(float volume) {
        masterMixer.SetFloat("Music Volume", Mathf.Log10(volume) * 20);
    }

    public void SetSoundsVolume(float volume) {
        masterMixer.SetFloat("SFX Volume", Mathf.Log10(volume) * 20);
    }

    public void QuitGame() {
        Application.Quit();
    }
}

