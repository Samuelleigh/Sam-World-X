using UnityEngine.Audio;
using UnityEngine;
using System;
using System.Collections.Generic;
using Random = UnityEngine.Random;

public class SoundSystem : MonoBehaviour
{

    public Sound[] sounds;
    public Sound[] music;
    public static SoundSystem instance;
    public AudioSource musicSource;
    public List<AudioSource> soundEffectsSources;
    public bool mutesounds = false;

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

            soundEffectsSources.Add(s.source);

        }


        musicSource = gameObject.AddComponent<AudioSource>();

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

    public void PlayMusic(string name)
    {
        musicSource.Stop();
        Sound s = Array.Find(music, sound => sound.name == name);

        if (s == null)
        {
            Debug.LogWarning("Sound: " + name + " not found");
            return;
        }

        musicSource.clip = s.clip;        
        musicSource.volume = s.volume;
        musicSource.pitch = s.pitch;
        musicSource.loop = s.loop;

        musicSource.Play();

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
                    Debug.Log(soundNames[i]);
                    bucket.Add(sounds[b]);
                  //  return;
                }
            
            }

        }
        bucket[Random.Range(0, bucket.Count)].source.Play();
    }


    public void PlayRandomSound(List<String> soundnames) 
    {
        List<Sound> bucket = new List<Sound>();
        foreach (Sound s in sounds) 
        {
            

            if (soundnames.Contains(s.name))
            {
                Debug.Log(s.name);
                bucket.Add(s);
            }
            else 
            {
                Debug.Log(s.name);
                Debug.Log("not in here");
            }
        }

        bucket[Random.Range(0, bucket.Count)].source.Play();

    }

    public void SwitchMuteSounds() 
    {
        mutesounds = !mutesounds;


        for (int i = 0; i < soundEffectsSources.Count; i++) 
        {
            if (mutesounds)
            {
                soundEffectsSources[i].playOnAwake = false;
                soundEffectsSources[i].enabled = false;

            }
            else
            {

                soundEffectsSources[i].enabled = true;

            }

        }      
              

    }
}
