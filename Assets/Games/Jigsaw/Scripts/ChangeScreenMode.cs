using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeScreenMode : MonoBehaviour
{

    public bool currentlyFullScreen = true;
    public bool onlyone = false;
    public void Awake()
    {
        
    }

    void Start() 
    {

        Screen.SetResolution(Screen.currentResolution.width, Screen.currentResolution.height, true);
        ChangeScreenMode[] shots = FindObjectsOfType<ChangeScreenMode>();
        bool onlyone = true;

        foreach (ChangeScreenMode s in shots)
        {
            if (s == this)
            {

            }
            else
            {
                onlyone = false;
            }

        }

        if (onlyone == true)
        {
            DontDestroyOnLoad(this);
        }

        Screen.SetResolution(Screen.currentResolution.width, Screen.currentResolution.height, true);
    }

    public void Update()
    {
        if (Input.GetKeyDown("f")) 
        {
            SwitchScreenMode();
        }
    }


    public void SwitchScreenMode() 
    {

        if (currentlyFullScreen)
        {

            Screen.fullScreenMode = FullScreenMode.Windowed;
            Screen.SetResolution(Screen.currentResolution.width / 2, Screen.currentResolution.height / 2, false);
            currentlyFullScreen = false;
        }
        else 
        {

            Screen.SetResolution(Screen.currentResolution.width * 2, Screen.currentResolution.height * 2, true);
            currentlyFullScreen = true;

        }
    
    }



}
