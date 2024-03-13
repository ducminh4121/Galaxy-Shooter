using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : SingletonDestroy<AudioManager>
{
    private AudioSource _audioSource;

    private void Start()
    {
        _audioSource = GetComponent<AudioSource>();
    }
    public void Shoot(AudioClip audioclip)
    {
        _audioSource.PlayOneShot(audioclip);
    }
}