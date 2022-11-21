﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace TheWayDown
{
    enum GameState { Start, Overworld, Content, End }

    public class WayDownGame : MonoBehaviour
    {
        public int startingInteraction;

        GameState gameState = GameState.Start;
        public int currentsectionID;
        public List<WDLevel> potentiallevels;
        public bool deadNextTurn = false;

        public UIMaster ui;
        public WDLevelManager levelManager;
        public WDLevel currentLevel;
        public WayDownInk ink;

        //Media View
        public Camera MediaCamera;

        //potion
        public Button potionPrefrab;
        public GameObject inventory;
        private List<int> EffectID = new List<int> { 1, 2, 3, 4, 5 };
        public bool justReturnedFromOverworld = true;



        //variables variables
        private List<string> varNames = new List<string>();
        delegate void MyDelegate(object VarIs, string name);
        MyDelegate myDelegate;

        public GameObject VaribleFrame;
        public GameObject InventoryFrame;
        public TextMeshProUGUI kneecapsText;
        public TextMeshProUGUI ageText;
        public TextMeshProUGUI bodyText;
        public TextMeshProUGUI awarenessText;
        public TextMeshProUGUI moodText;
        public TextMeshProUGUI strangeText;
        public TextMeshProUGUI strangeAmountText;

        private bool createPotion = true;
        public List<Sprite> PotionSprites;

        public TextMeshProUGUI infotext;
        public SoundSystem soundsystem;

        public List<GameObject> levelSets;

        private void Awake()
        {
            levelManager = FindObjectOfType<WDLevelManager>();
            ink = FindObjectOfType<WayDownInk>();
            soundsystem = FindObjectOfType<SoundSystem>();
        }

        // Start is called before the first frame update
        void Start()
        {
            VaribleFrame.SetActive(false);
            InventoryFrame.SetActive(false);
            ui.SwitchLayer(0);


            myDelegate = UpdateVarFrame;

            varNames.Add("KneeCaps");
            varNames.Add("Age");
            varNames.Add("Body");
            varNames.Add("Awareness");
            varNames.Add("Strange");
            varNames.Add("StrangeAmount");

            if (levelManager.debug == true)
            {

                StartGame();
                ui.SwitchLayer(2);


            }
        }

     

        public void StartGame()
        {

            // for (int i = 0; i < levelSets.Count; i++) { levelSets[i].SetActive(false); }

            // soundsystem.PlayMusic("music");
            soundsystem.PlaySound("click");

            //randomize potion Effects
            for (int i = 0; i < EffectID.Count; i++)
            {
                int temp = EffectID[i];
                int randomIndex = Random.Range(i, EffectID.Count);
                EffectID[i] = EffectID[randomIndex];
                EffectID[randomIndex] = temp;
            }

            for (int i = 0; i < EffectID.Count; i++)
            {
                Debug.Log(EffectID[i]);
            }

            //Randomize potion Sprites
            for (int i = 0; i < PotionSprites.Count; i++)
            {
                Sprite temp = PotionSprites[i];
                int randomIndex = Random.Range(i, PotionSprites.Count);
                PotionSprites[i] = PotionSprites[randomIndex];
                PotionSprites[randomIndex] = temp;
            }


            //turn on varible frame
            VaribleFrame.SetActive(true);
            InventoryFrame.SetActive(true);



            gameState = GameState.Start;
            currentLevel = levelManager.Levels[startingInteraction];
            ink.StartStory();
            HideMedia();


            //obserbe

            ink.story.ObserveVariables(varNames, (string varName, object newValue) =>
             {
            //   Debug.Log("===");
            // Debug.Log(varName);
            //Debug.Log(newValue);
            myDelegate(newValue, varName);
             });

            CreatePotionButton(1);
            CreatePotionButton(2);

            ink.story.EvaluateFunction("intialsetup");

            ink.story.ChoosePathString("scapegoat");
            ink.SaveMomentBeforeState();

            LoadLevel();

        }

        public void LoadLevel()
        {

            gameState = GameState.Content;
            if (deadNextTurn == false)
            {

                ui.SwitchLayer(2);
                ink.story.ChoosePathString(currentLevel.EventName);
                Debug.Log(currentLevel.EventName);
                ink.RefreshView();
            }
            else
            {
                ui.SwitchLayer(2);
                ink.story.ChoosePathString(levelManager.deathlevel.EventName);
                ink.RefreshView();
            }
        }

        public void MoveToOverworld()
        {

            gameState = GameState.Overworld;
            soundsystem.PlayMusic("Travel1");

            Debug.Log("move back to overworld");
            HideMedia();
            potentiallevels.Clear();

            ink.story.ChoosePathString("scapegoat");
            ink.SaveMomentBeforeState();
            justReturnedFromOverworld = true;

            ui.SwitchLayer(1);

            //this code is for selecting the next ink Knot to play

            currentsectionID++;

            if (currentLevel.EventScenes != null)
            {
                currentLevel.EventScenes.SetActive(false);
            }

            currentLevel = levelManager.Levels[currentsectionID];


            if (currentLevel.EventScenes != null)
            {
                currentLevel.EventScenes.SetActive(true);
            }
            StartCoroutine(OverWorldWait(3));


        }

        public void GameOver()
        {
            soundsystem.PlayMusic("Null");
            gameState = GameState.End;
            ui.SwitchLayer(3);
            VaribleFrame.SetActive(false);
            InventoryFrame.SetActive(false);

        }

        public void ShowMedia(int cameraID)
        {
            ui.IndivdualFrames[1].SetActive(true);

            Debug.Log(cameraID);

            MediaCamera.transform.position = currentLevel.cameraTransform[cameraID].position;
            MediaCamera.transform.rotation = currentLevel.cameraTransform[cameraID].rotation;
            MediaCamera.transform.SetParent(currentLevel.cameraTransform[cameraID]);

        }

        public void TriggerAnimation(int animatorID, string triggername, bool loop)
        {
            Debug.Log(triggername);
            currentLevel.Animators[animatorID - 1].SetTrigger(triggername);


        }

        public void HideMedia()
        {

            ui.IndivdualFrames[1].SetActive(false);

        }

        public void UpdateVarFrame(object varIs, string name)
        {
            if (name == "KneeCaps") { kneecapsText.text = varIs.ToString(); }
            if (name == "Age") { ageText.text = varIs.ToString(); }
            if (name == "Body") { bodyText.text = varIs.ToString(); CheckGameOver(varIs, 0, 10000); }
            if (name == "Awareness") { awarenessText.text = varIs.ToString(); CheckGameOver(varIs, 0, 100); }
            if (name == "Strange") { strangeText.text = varIs.ToString(); }
            if (name == "StrangeAmount") { strangeAmountText.text = varIs.ToString(); }

        }


        public IEnumerator OverWorldWait(int waittime)
        {

            yield return new WaitForSeconds(waittime);

            LoadLevel();

        }

        public void ReloadGame()
        {

            SceneManager.LoadScene(SceneManager.GetActiveScene().name);

        }

        public void CheckGameOver(object current, int min, int max)
        {



        }

        public void CreatePotionButton(int potionID)
        {

            Debug.Log("potion spawned is " + potionID);

            if (createPotion == true)
            {
                Button button = Instantiate(potionPrefrab) as Button;
                button.transform.SetParent(inventory.transform, false);

                button.interactable = true;

                button.GetComponent<Image>().sprite = PotionSprites[potionID];

                button.onClick.AddListener(delegate
                {
                    ConsumePotion(button, potionID);
                });
            }

        }

        public void ShowText(string s)
        {

            ui.TurnFrameOn(3);
            infotext.text = s;


        }



        public void ConsumePotion(Button button, int choice)
        {

            Debug.Log("======================================CONSUME===============================");
            ink.story.state.LoadJson(ink.lastState);

            switch (EffectID[choice])
            {
                case 0:
                    Debug.Log(EffectID[choice] + " KneeCaps + 11");
                    ink.story.EvaluateFunction("potion1");
                    // ShowText("KneeCaps Increased by 100");
                    break;
                case 1:
                    Debug.Log(EffectID[choice] + " KneeCaps + 11");
                    ink.story.EvaluateFunction("potion1");

                    //ShowText("Body has been increased");
                    break;
                case 2:
                    Debug.Log(EffectID[choice] + " Body + 1 * 0.3");
                    ink.story.EvaluateFunction("potion2");
                    //  ShowText("Age minus 100");
                    break;
                case 3:
                    Debug.Log(EffectID[choice] + " Age - 100");
                    ink.story.EvaluateFunction("potion3");
                    //  ShowText("Mood Changed");
                    break;
                case 4:
                    Debug.Log(EffectID[choice] + " Awareness + 0.1");
                    ink.story.EvaluateFunction("potion4");
                    //  ShowText("I keep finding myself.");
                    break;
                case 5:
                    Debug.Log(EffectID[choice] + "strangeAmountText ammount + 1 *2");
                    ink.story.EvaluateFunction("potion5");
                    //  ShowText("I keep finding myself.");
                    break;
            }



            if (gameState == GameState.Content)
            {
                if (justReturnedFromOverworld == false)
                {
                    ink.showNextLine = false;
                    ink.lastpressedbuttonText = ink.lastchoice.text;
                    ink.SaveMomentBeforeState();
                    ink.story.ChooseChoiceIndex(ink.lastchoice.index);
                }
                else
                {
                    ink.story.ChoosePathString(currentLevel.EventName);
                }

                createPotion = false;
                ink.instantTextScroll = true;
                ink.RefreshView();
                createPotion = true;
            }


            Destroy(button.gameObject);


        }

        public void BackToSamWorld() 
        {

          //  soundsystem.s

            SceneManager.LoadScene("Main Menu",LoadSceneMode.Single);
            
        }


    }
}