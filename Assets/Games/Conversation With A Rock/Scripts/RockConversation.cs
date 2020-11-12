using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Ink;
using KnockKnock;

    public class RockConversation : MonoBehaviour
    {

        public GameObject InputFrame;
        public GameObject rockParent;
        public BasicInkExample inkScript;
        public UIMaster uimaster;
        public EndGameLogic endgameLogic;

        // Start is called before the first frame update

        void Awake()
        {
            inkScript = FindObjectOfType<BasicInkExample>();
            uimaster = FindObjectOfType<UIMaster>();
            endgameLogic = FindObjectOfType<EndGameLogic>();
        }

        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

            if (uimaster.CurrentLayer == 2)
            {
                endgameLogic.EndGame();

            }

            if (inkScript.lastpressedbuttonText == "But I think I can trust you.")
            {
                InputFrame.SetActive(true);
            }
            else
            {
                InputFrame.SetActive(false);
            }

        }


        public void gameOver()
        {

            uimaster.SwitchLayer(2);

        }



    }
