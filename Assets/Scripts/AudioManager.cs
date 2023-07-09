using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour {
    public static AudioManager Instance { get; private set; }
    [SerializeField] Sound[] sounds;

    void Awake() {
        Instance = this;
        foreach (Sound toInit in sounds) {
            AudioSource newSource = gameObject.AddComponent<AudioSource>();
            toInit.Initialize(newSource);
        }
    }
    public void PlaySoundByName(string name) => FindSoundByName(name).PlaySound();
    public void StopSoundByName(string name) => FindSoundByName(name).StopSound();

    Sound FindSoundByName(string str) {
        Sound output = Array.Find(sounds, s => s.name == str);
        if (output != null) { return output; }
        return null;
    }
}