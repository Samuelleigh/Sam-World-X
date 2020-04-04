using UnityEngine.Audio;
using UnityEngine;
using System.Collections.Generic;

public class AudioManager : MonoBehaviour
{

    public List<AudioClip> drums;
    public AudioSource source;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void Playdrum()
    {
        source.clip = drums[Random.Range(0, drums.Count)];
        source.Play();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
