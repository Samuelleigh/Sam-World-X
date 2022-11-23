using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MovingJigsaw;
using UnityEngine.Audio;
using TMPro;

public class MainMenuMusicController : MonoBehaviour
{

    public AudioSource background;
    public JigLevelManager jigManager;
    public TextMeshProUGUI mutesoundtext;
    public TextMeshProUGUI muteMusictext;

    public void Start()
    {

        if (jigManager.muteVideo)
        {
            muteMusictext.text = "Unmute Music";
            background.enabled = false;
        }
        else 
        {
            muteMusictext.text = "Mute Music";
            background.enabled = true;
        }
       


    }

    public void EnableBackgroundMusic() 
    {

        jigManager = FindObjectOfType<JigLevelManager>();

        if (jigManager.muteVideo)
        {
            muteMusictext.text = "Unmute Music";
            background.enabled = false;
        }
        else
        {
            muteMusictext.text = "Mute Music";
            background.enabled = true;
        }

    }


}
