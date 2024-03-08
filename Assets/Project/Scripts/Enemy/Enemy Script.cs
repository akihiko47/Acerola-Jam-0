using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyScript : MonoBehaviour {

    [SerializeField]
    private Transform target;

    [SerializeField]
    private float speed;

    void Update() {
        Vector3 dir = (target.position - transform.position).normalized;

        transform.position += dir * speed * Time.deltaTime;
    }

    private void OnTriggerEnter(Collider other) {
        if (other.gameObject.tag == "Player") {
            other.gameObject.GetComponent<PlayerHealth>().Death();
        }
    }

    public void SetTarget(Transform newTarget) {
        target = newTarget;
    }
}
