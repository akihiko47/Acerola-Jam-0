using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MusicManager : MonoBehaviour {

    [SerializeField]
    SoundManager.Sound[] musicList;

    public static MusicManager Instance;

    private AudioSource musicAudioSource;

    private int prevSceneNumber = -1;

    private void Awake() {
        if (Instance == null) {
            Instance = this;
            DontDestroyOnLoad(this.gameObject);

            musicAudioSource = GetComponent<AudioSource>();
            musicAudioSource.loop = true;

            SceneManager.sceneLoaded += OnSceneLoaded;
        } else {
            Destroy(gameObject);
        }
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode) {
        PlayMusicOnScene();
    }

    void Start() {
        PlayMusicOnScene();
    }

    private void PlayMusicOnScene() {
        int sceneNumber = SceneManager.GetActiveScene().buildIndex;
        if (prevSceneNumber == -1) {  // first launch
            prevSceneNumber = 0;

            Sounds.SoundAudioClip audioData = SoundManager.GetFullAudioClip(musicList[sceneNumber]);
            musicAudioSource.clip = audioData.audioClip;
            musicAudioSource.volume = audioData.volume;
            musicAudioSource.pitch = audioData.pitch;
            musicAudioSource.outputAudioMixerGroup = audioData.audioMixer;

            musicAudioSource.Play();
        } else {  // not first scene
            if (sceneNumber != prevSceneNumber) {

                Sounds.SoundAudioClip audioData = SoundManager.GetFullAudioClip(musicList[sceneNumber]);
                musicAudioSource.clip = audioData.audioClip;
                musicAudioSource.volume = audioData.volume;
                musicAudioSource.pitch = audioData.pitch;
                musicAudioSource.outputAudioMixerGroup = audioData.audioMixer;
                musicAudioSource.Play();

                prevSceneNumber = sceneNumber;
            }
        }
    }

    public void StopMusic() {
        musicAudioSource.Stop();
    }
}
