using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System.Linq;
using UnityEngine.Events;

public class ButtonClickSoundSystemLink : MonoBehaviour
{

    public List<Button> buttons = new List<Button>();

    public string soundname;

    public SoundSystem sounds;

    private UnityAction action;

    // Start is called before the first frame update
    void Start()
    {

        sounds = FindObjectOfType<SoundSystem>(true);
        buttons = FindObjectsOfType<Button>(true).ToList();


        foreach (Button b in buttons) 
        {
            b.onClick.AddListener(delegate { PlaySound(); Invoke("SoundClickUpdate",0.5f); } );          
        }

    }

    public void PlaySound() 
    {
        sounds.PlaySound(soundname);
    
    }

    public void SoundClickUpdate() 
    {

        buttons = buttons.Where(item => item != null).ToList();
        
        List<Button> temp = FindObjectsOfType<Button>().ToList();


        foreach (Button b in temp) 
        {
            if (!buttons.Contains(b))
            {
                buttons.Add(b);
                b.onClick.AddListener(delegate { PlaySound(); Invoke("SoundClickUpdate", 0.5f); }); 
            }

        }


    }


    // Update is called once per frame
    void Update()
    {
        
    }
}
