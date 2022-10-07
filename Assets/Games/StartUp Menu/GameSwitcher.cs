using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameSwitcher : MonoBehaviour
{
    public bool FunGameSelect;
    public Transform playertransform;
   // public static GameSwitcher instance;

    public void Awake()
    {
       
    }

    public void FunSwitchMode() 
    {

        FunGameSelect = true;
        BackToMainMenuScene();


    }

    public void QuickSwitchMode() 
    {

        FunGameSelect = false;
        BackToMainMenuScene();



    }

    private void Update()
    {

        if (Input.GetKey("s")) 
        {

            if (Input.GetKey("a"))
            {
                if (Input.GetKey("m"))
                {


                    Debug.Log("wow");
                    QuickSwitchMode();



                }




            }



        }


    }

    public void BackToMainMenuScene() 
    {

        if (FunGameSelect == false)
        {
            SceneManager.LoadScene("Main Menu", LoadSceneMode.Single);
        }
        else 
        {
            SceneManager.LoadScene("Overworld", LoadSceneMode.Single);

        }
   
    }


    public void LoadNewGame(string name) 
    {

        if (FunGameSelect == true) { }

            SceneManager.LoadScene(name, LoadSceneMode.Single);    
    }
}
