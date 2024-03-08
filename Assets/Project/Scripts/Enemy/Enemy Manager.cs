using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour {

    [SerializeField]
    GameObject enemyPrefab;

    [SerializeField]
    Light playerFlashlight;

    private bool isAttacking = false;

    private GameObject enemy;

    private void Update() {
        if (playerFlashlight.intensity <= 0.01 && !isAttacking) {
            StartAttack();
        } else if (playerFlashlight.intensity > 0.01 && isAttacking) {
            StopAttack();
        }
    }

    private void StartAttack() {
        isAttacking = true;
        enemy = Instantiate(enemyPrefab, new Vector3(0f, 2.5f, -30f), Quaternion.identity);
        enemy.GetComponent<EnemyScript>().SetTarget(playerFlashlight.transform);
    }

    private void StopAttack() {
        isAttacking = false;
        Destroy(enemy);
    }

}
