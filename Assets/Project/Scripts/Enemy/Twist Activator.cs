using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TwistActivator : MonoBehaviour {

    private GameObject monsterModel;

    private void Start() {
        monsterModel = transform.GetChild(0).gameObject;
        monsterModel.SetActive(false);
    }

    private void OnTriggerEnter(Collider other) {
        if (other.tag == "Player") {
            StartCoroutine(ActivateMonster());
            SoundManager.PlaySound(SoundManager.Sound.screamer);
            TwistManager.Instance.StartTwist();
        }
    }

    private IEnumerator ActivateMonster() {
        monsterModel.SetActive(true);
        yield return new WaitForSeconds(0.3f);
        Destroy(gameObject);
    }

}
