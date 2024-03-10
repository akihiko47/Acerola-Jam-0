using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster : MonoBehaviour {

    [SerializeField]
    private Transform looker;

    private GameObject model;

    private bool active = false;

    private void Start() {
        model = transform.GetChild(0).gameObject;
        model.SetActive(false);
    }

    private void Update() {
        if (!active) {
            return;
        }

        Vector3 lookDir = looker.forward.normalized;
        Vector3 dir = (transform.position - looker.position).normalized;
        float dot = Vector3.Dot(dir, lookDir);

        float dist = Vector3.Distance(transform.position, looker.position);

        if (dot > 0.7f || dist < 2f) {
            StartCoroutine(Destroy());
        }
    }

    private IEnumerator Destroy() {
        yield return new WaitForSeconds(0.3f);
        Destroy(gameObject);
    }

    public void Activate() {
        model.SetActive(true);
        active = true;
    }

}
