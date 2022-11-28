using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangePhoneOrientationMode : MonoBehaviour
{
    public bool landscape = false;


    private void Start()
    {

        if (landscape == true)
        {
            Screen.orientation = ScreenOrientation.LandscapeLeft;
        }
        else
        {
            Screen.orientation = ScreenOrientation.Portrait;
        }

    }
}
