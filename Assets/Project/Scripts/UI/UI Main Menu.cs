using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIMainMenu : MonoBehaviour {

    [SerializeField]
    GameObject CreditsPanel;

    void Awake() {
        Cursor.lockState = CursorLockMode.None;

        CreditsPanel.SetActive(false);
    }

    /*public void PlayHoverSound() {
        SoundManager.PlaySound(SoundManager.Sound.buttonOver);
    }*/

    private void StartNewGame() {
        //MusicManager.Instance.StopMusic();
        //SoundManager.PlaySound(SoundManager.Sound.buttonClick);
        ScenesManager.Instance.LoadNextScene();
    }

    public void ActivateCreditsPanel() {
        //SoundManager.PlaySound(SoundManager.Sound.buttonClick);
        CreditsPanel.SetActive(true);
    }

    public void DeactivateCreditsPanel() {
        //SoundManager.PlaySound(SoundManager.Sound.buttonClick);
        CreditsPanel.SetActive(false);
    }

    public void QuitGame() {
        Application.Quit();
    }
}

