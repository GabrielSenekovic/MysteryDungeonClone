using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour {

    static AudioSource audioSource;

    public void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public static void ChangeBGM(AudioClip music)
    {
        Debug.Log("Changing BGM!");
        audioSource.Stop();
        audioSource.clip = music;
        audioSource.Play();
    }

}
