using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{

    private static AudioManager instance;

    private AudioManager()
    {}

    public static AudioManager getInstance()
    {
        if (instance == null) instance = new AudioManager();
        return instance;
    }
    
    public void PlayAudio(AudioClip audio)
    {
        var prefab = new GameObject();
        var audioSource = prefab.AddComponent<AudioSource>();
        audioSource.clip = audio;
        audioSource.Play();
        var d = prefab.AddComponent<AudioObject>();
        d.Destroy();
    }

    public void MisicOff(bool off)
    {
        PlayerPrefs.SetInt("VolumeMusic", off ? 0: 1);
    }
    
    public void PlayAudio(AudioClip audio, float time)
    {
        var prefab = new GameObject();
        var audioSource = prefab.AddComponent<AudioSource>();
        audioSource.clip = audio;
        audioSource.Play();
        var d = prefab.AddComponent<AudioObject>();
        d.Init(time);
    }

}