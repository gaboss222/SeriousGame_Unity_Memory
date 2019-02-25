using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// AudioController
/// Music from https://www.playonloop.com
/// Sound from https://www.zapsplat.com
/// </summary>
public class AudioScript : MonoBehaviour {

    AudioSource audioSource;

    /// <summary>
    /// Initialization
    /// </summary>
    void Start ()
    {
        audioSource = GetComponent<AudioSource>();      
        audioSource.volume = StaticParameterClass.VolumeLevel / 1.5f;
	}

    /// <summary>
    /// Get component audioSource 
    /// </summary>
    /// <param name="i">audioSource 1 or 2</param>
    /// <returns>Component audio source</returns>
    public AudioSource GetAudioSource(int i)
    {
        return GetComponents<AudioSource>()[i];
    }
}
