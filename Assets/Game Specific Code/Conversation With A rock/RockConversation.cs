using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Ink;

public class RockConversation : MonoBehaviour
{

    public GameObject InputFrame;
    public GameObject rockParent;
    public BasicInkExample inkScript;
    public UIMaster uimaster;

    // Start is called before the first frame update

    void Awake()
    {
        inkScript = FindObjectOfType<BasicInkExample>();
        uimaster = FindObjectOfType<UIMaster>();
    }

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetKey("escape"))
        {
            QuitProgram();
        }

        if (uimaster.CurrentLayer == 2) 
        {
            Invoke("QuitProgram",2f);
           
        }

        if (inkScript.lastpressedbuttonText == "But I think I can trust you.")
        {
            InputFrame.SetActive(true);
        }
        else 
        {
            InputFrame.SetActive(false);
        }
        if (inkScript.gameover == true ) { uimaster.SwitchLayer(2); }

    }

    public void QuitProgram() 
    {

        Application.Quit();
    }


}
