using System.Collections;
using System.Collections.Generic;
using UnityEngine.Audio;
using UnityEngine;

public class Sounds : MonoBehaviour {

    public static Sounds Instance;

    public SoundAudioClip[] soundAudioClipArray;

    [System.Serializable]
    public class SoundAudioClip {
        public SoundManager.Sound sound;
        public AudioClip audioClip;

        public float playDelay;
        [System.NonSerialized] public float lastTimePlayed = -5f;

        public AudioMixerGroup audioMixer;

        public float volume = 1f;
        public float pitch = 1f;
        public bool loop = false;
    }



    private void Awake() {
        if (Instance == null) {
            Instance = this;
        } else {
            Destroy(gameObject);
            DontDestroyOnLoad(gameObject);
        }
    }

    public static bool CanPlaySound(SoundAudioClip sound) {

        float lastTimePlayed = sound.lastTimePlayed;
        float maxDelay = sound.playDelay;
        if (lastTimePlayed + maxDelay < Time.time) {
            sound.lastTimePlayed = Time.time;
            return true;
        } else {
            return false;
        }
    }
}
