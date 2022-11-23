using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenShotControl : MonoBehaviour
{

    public bool allowScreenShots = false;

    private void Start()
    {

        ScreenShotControl[] shots = FindObjectsOfType<ScreenShotControl>();
        bool onlyone = true;

        foreach (ScreenShotControl s in shots) 
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

    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.P) && allowScreenShots)
        {
            Debug.Log(Application.persistentDataPath + "/Hi");
            ScreenCapture.CaptureScreenshot(Application.persistentDataPath + "/Hi " + Random.Range(0, 1000).ToString() + ".png");

        }



    }    



}
