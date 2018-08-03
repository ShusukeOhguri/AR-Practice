using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundOfExclamation : MonoBehaviour {

    public AudioClip exclamation;
    public AudioSource audioSource;

    void Start()
    {
        audioSource = gameObject.GetComponent<AudioSource>();
    }
	
    public void PlayExclamation()
    {
        audioSource.time = 0.8f;
        audioSource.Play();
    }
}