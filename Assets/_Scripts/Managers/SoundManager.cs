using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager instance;
    [HideInInspector]
    public AudioSource audioSource;

    private void Awake()
    {
        instance = this;
        audioSource = GetComponent<AudioSource>();
    }

    public void PlaySoundFX(AudioClip clip, float volume)
    {
        audioSource.PlayOneShot(clip, volume);
    }
}
