using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class InteractorUI : MonoBehaviour {

    [SerializeField]
    private GameObject infoContainer;

    [SerializeField]
    private TextMeshProUGUI infoText;

    private bool isActive = false;

    private void Start() {
        infoContainer.SetActive(false);
    }

    public void ShowInfo(string message) {
        isActive = true;
        infoContainer.SetActive(true);
        infoText.text = message;
    }

    public void HideInfo() {
        isActive = false;
        infoContainer.SetActive(false);
        infoText.text = "";
    }

    public bool IsActive() {
        return isActive;
    }
}
