﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndGameLogic : MonoBehaviour
{

    public Scene currentscene;
    public bool shutdownOnEnd;
    public float shutdownOnEndTime;

    // Start is called before the first frame update
    void Start()
    {
       currentscene = SceneManager.GetActiveScene();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey("escape"))
        {
            QuitProgram();
        }

    }



    public void EndGame()
    {

     
        if (currentscene.name == "Rock Conversation") 
        {
            FindObjectOfType<RockConversation>().gameOver();
        }


        if (shutdownOnEnd == true)
        {
            Debug.Log("w");
            shutdownOnEnd = false;
            Invoke("QuitProgram", shutdownOnEndTime);
        }



    }
    public void QuitProgram()
    {

        Application.Quit();
    }


 
}