using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public static class SoundManager {

    public enum Sound {
        buttonClick,
        step,
        musicMainMenu,
        musicMainScene,
    }

    private static GameObject oneShotGameObject;
    private static AudioSource oneShotAudioSource;


    public static void PlaySound(Sound sound) {
        Sounds.SoundAudioClip audioData = GetFullAudioClip(sound);
        if (Sounds.CanPlaySound(audioData)) {
            if (oneShotGameObject == null) {
                oneShotGameObject = new GameObject("Sound");
                oneShotAudioSource = oneShotGameObject.AddComponent<AudioSource>();

                oneShotAudioSource.volume = audioData.volume;
                oneShotAudioSource.pitch = audioData.pitch;
                oneShotAudioSource.loop = audioData.loop;
                oneShotAudioSource.outputAudioMixerGroup = audioData.audioMixer;
            }
            oneShotAudioSource.PlayOneShot(audioData.audioClip);
        }
    }

    public static void PlaySound(Sound sound, Vector3 position) {
        Sounds.SoundAudioClip audioData = GetFullAudioClip(sound);
        if (Sounds.CanPlaySound(audioData)) {
            GameObject soundGameObject = new GameObject("Sound");
            soundGameObject.transform.position = position;
            AudioSource audioSource = soundGameObject.AddComponent<AudioSource>();

            audioSource.volume = audioData.volume;
            audioSource.pitch = audioData.pitch;
            audioSource.loop = audioData.loop;

            audioSource.maxDistance = 100f;
            audioSource.rolloffMode = AudioRolloffMode.Linear;
            audioSource.dopplerLevel = 0f;
            audioSource.spatialBlend = 1f;

            audioSource.clip = audioData.audioClip;
            audioSource.Play();

            Object.Destroy(soundGameObject, audioSource.clip.length);
        }
    }

    public static Sounds.SoundAudioClip GetFullAudioClip(Sound sound) {
        foreach (Sounds.SoundAudioClip soundAudioClip in Sounds.Instance.soundAudioClipArray) {
            if (soundAudioClip.sound == sound) {
                return soundAudioClip;
            }
        }
        Debug.LogError("Sound " + sound + " not found!");
        return null;
    }
}
