using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterCollider : MonoBehaviour {

    Monster monsterScript;

    private void Start() {
        monsterScript = transform.parent.GetComponent<Monster>();
    }

    private void OnTriggerEnter(Collider other) {
        if (other.tag == "Player") {
            monsterScript.Activate();
        }
    }

}
