using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class IntroManager : MonoBehaviour {

    [SerializeField]
    TMP_Text mainText;

    [SerializeField]
    GameObject skipObject;

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
                LoadNextScene();
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

    private void DeactivateSkipObject() {
        skipObject.SetActive(false);
    }

}
