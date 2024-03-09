using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour {
    
    public void Death() {
        SoundManager.PlaySound(SoundManager.Sound.death);
        GetComponent<PlayerMovement>().enabled = false;
        ScenesManager.Instance.RestartScene();
    }

}
