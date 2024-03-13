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

    [SerializeField]
    private Slider musicSlider;

    [SerializeField]
    private Slider sfxSlider;

    void Awake() {
        Cursor.lockState = CursorLockMode.None;

        CreditsPanel.SetActive(false);
        SettingsPanel.SetActive(false);
    }

    private void Start() {
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

    public void StartNewGame() {
        MusicManager.Instance.StopMusic();
        SoundManager.PlaySound(SoundManager.Sound.buttonClick);
        SoundManager.PlaySound(SoundManager.Sound.boom);
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
        PlayerPrefs.Save();
    }

    public void SetMusicVolume(float volume) {
        masterMixer.SetFloat("Music Volume", Mathf.Log10(volume) * 20);
        PlayerPrefs.SetFloat("music volume", volume);
    }

    public void SetSoundsVolume(float volume) {
        masterMixer.SetFloat("SFX Volume", Mathf.Log10(volume) * 20);
        PlayerPrefs.SetFloat("sfx volume", volume);
    }

    public void QuitGame() {
        Application.Quit();
    }
}

