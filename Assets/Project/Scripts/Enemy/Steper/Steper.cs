using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Steper : MonoBehaviour {

    [SerializeField]
    private Transform startPos;

    [SerializeField]
    private Transform endPos;

    [SerializeField]
    private int stepsNum;

    [SerializeField, Range(0f, 100f)]
    private float duration;

    public void StartSteps() {
        StartCoroutine(PlaySteps());
    }

    private IEnumerator PlaySteps() {
        Vector3 increment = (endPos.position - startPos.position) / (stepsNum - 1);
        float timeIncrement = duration / stepsNum;

        for (int i = 0; i < stepsNum; i++) {
            Vector3 soundPos = startPos.position + increment * i;
            SoundManager.PlaySound(SoundManager.Sound.monsterStep, soundPos);
            yield return new WaitForSeconds(timeIncrement);
        }

        Destroy(gameObject);
    }

}
