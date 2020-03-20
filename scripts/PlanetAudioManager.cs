using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlanetAudioManager : MonoBehaviour
{
    private AudioSource backgroundAudioSource;
    public void Start()
    {
        backgroundAudioSource = this.GetComponent<AudioSource>();
    }
    public void Update()
    {
        if(Time.timeScale != 0)
        {
            if (!backgroundAudioSource.isPlaying)
            {
                backgroundAudioSource.Play();
                backgroundAudioSource.loop = true;
            }
        }
        else
        {
            backgroundAudioSource.Stop();
        }
    }
}
