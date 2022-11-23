using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeScreenMode : MonoBehaviour
{

    public static ChangeScreenMode instance;

    public bool currentlyFullScreen = true;
    public bool onlyone = false;

    public void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this);
        }
        else 
        {
            Destroy(this);
        }
    }

    void Start() 
    {       

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

      //  Debug.Log("Screen Width");


        if (Screen.fullScreen)
        {
            Debug.Log("Screen Width");
            Screen.SetResolution(Screen.currentResolution.width/2, Screen.currentResolution.height/2, false);
          //  Screen.SetResolution(Screen.currentResolution.width / 2, Screen.currentResolution.height / 2, false);
            return;
        }
        else 
        {
            Screen.SetResolution(Screen.currentResolution.width, Screen.currentResolution.height, false);
            Screen.fullScreenMode = FullScreenMode.ExclusiveFullScreen;
            
            return;
       
        }
    
    }



}
