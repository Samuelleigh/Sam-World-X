﻿using UnityEngine.Audio;
using UnityEngine;
using System;
using System.Collections.Generic;
using Random = UnityEngine.Random;

public class SoundSystem : MonoBehaviour
{

    public Sound[] sounds;
    public static SoundSystem instance;

    // Start is called before the first frame update

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else {
            Destroy(gameObject);
            return;
        }
        DontDestroyOnLoad(gameObject);

        foreach (Sound s in sounds) 
        
        {
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;

            s.source.volume = s.volume;
            s.source.pitch = s.pitch;
            s.source.loop = s.loop;
        }
    }

    public void PlaySound(string name) 
    {

        Sound s = Array.Find(sounds, sound => sound.name == name);

        if (s == null) 
        {
            Debug.LogWarning("Sound: " + name + " not found");
            return;
        }

        s.source.Play();   
    
    }

    public void PlayRandomSound(string[] soundNames) 
    {

        
        List<Sound> bucket = new List<Sound>();

        for (int i  =0; i < soundNames.Length; i++) 
        {
            for (int b = 0; b < sounds.Length ; b++) 
            {
                if (soundNames[i] == sounds[b].name) 
                {

                    bucket.Add(sounds[b]);
                  //  return;
                }
            
            }

        }
        bucket[Random.Range(0, bucket.Count)].source.Play();
    }
}