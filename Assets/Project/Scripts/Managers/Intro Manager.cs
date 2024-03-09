using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class IntroManager : MonoBehaviour {

    [SerializeField]
    private TMP_Text mainText;

    [SerializeField]
    private GameObject skipObject;

    [SerializeField]
    private bool outro = false;

    private int currentText = 0;

    [SerializeField]
    private TextMessage[] textMessagesArray;

    [System.Serializable]
    public class TextMessage {
        public string message;
        public SoundManager.Sound messageSound;
    }

    private void Awake() {
        Cursor.lockState = CursorLockMode.Locked;
        mainText.text = textMessagesArray[0].message;
    }

    private void Update() {
        if (Input.GetKeyDown(KeyCode.Space)) {

            currentText += 1;

            if (currentText > 0) {
                DeactivateSkipObject();
            }

            if (currentText < textMessagesArray.Length) {
                SetCurrentText();
            } else {
                if (outro) {
                    LoadMainMenu();
                } else {
                    LoadNextScene();
                }
            }

        }
    }

    private void SetCurrentText() {
        mainText.text = textMessagesArray[currentText].message;
        SoundManager.PlaySound(textMessagesArray[currentText].messageSound);
    }

    private void LoadNextScene() {
        ScenesManager.Instance.LoadNextScene();
    }

    private void LoadMainMenu() {
        ScenesManager.Instance.LoadMainMenu();
    }

    private void DeactivateSkipObject() {
        skipObject.SetActive(false);
    }

}
