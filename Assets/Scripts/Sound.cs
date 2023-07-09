using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

[CreateAssetMenu(menuName = "ScriptableObjects/Sound")]
public class Sound : ScriptableObject {
    [field: SerializeField] public AudioClip Clip { get; private set; }
    [field: SerializeField] [field: Range(0f, 3f)] public float Volume { get; private set; }
    [field: SerializeField] [field: Range(0f, 3f)] public float Pitch { get; private set; }
    [field: SerializeField] public bool Loop { get; private set; }
    [field: SerializeField] public bool PlayOnAwake { get; private set; }
    AudioSource _source;

    public void Initialize(AudioSource src) {
        _source = src;
        _source.volume = Volume;
        _source.pitch = Pitch;
        _source.clip = Clip;
        _source.loop = Loop;
        if (PlayOnAwake) {
            _source.Play();
        }
    }
    public void PlaySound() {
        _source.Play();
    }
    public void StopSound() {
        _source.Stop();
    }
}
