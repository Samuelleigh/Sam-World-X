using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

enum GameState {Start, Overworld, Content, End }

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
    private List<int> EffectID = new List<int> {1,2,3,4,5};



    //variables variables
    private List<string> varNames = new List<string>();
    delegate void MyDelegate(object VarIs,string name);
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


    private void Awake()
    {
        levelManager = FindObjectOfType<WDLevelManager>();
        ink = FindObjectOfType<WayDownInk>();
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
        varNames.Add("Mood");
        varNames.Add("Strange");
        varNames.Add("StrangeAmount");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void StartGame() 
    {

        //randomize potions
        for (int i = 0; i < EffectID.Count; i++)
        {
            int temp = EffectID[i];
            int randomIndex = Random.Range(i, EffectID.Count);
            EffectID[i] = EffectID[randomIndex];
            EffectID[randomIndex] = temp;
        }



        //turn on varible frame
        VaribleFrame.SetActive(true);
        InventoryFrame.SetActive(true);

        CreatePotionButton(1);
        CreatePotionButton(1);

        gameState = GameState.Start;
        currentLevel = levelManager.Levels[startingInteraction];
        ink.StartStory();

        //obserbe

        ink.story.ObserveVariables(varNames,(string varName, object newValue) =>
        {
            Debug.Log(varName);
            Debug.Log(newValue);
            myDelegate(newValue, varName);
        });

        ink.story.EvaluateFunction("intialsetup");
        LoadLevel();

    }

    public void LoadLevel() 
    {
        if (deadNextTurn == false)
        {

            ui.SwitchLayer(2);
            ink.story.ChoosePathString(currentLevel.EventName);
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
        Debug.Log("move back to overworld");
        HideMedia();
        potentiallevels.Clear();

        ui.SwitchLayer(1);

        //this code is for selecting the next Content Knot to play

        currentsectionID++;

 

        foreach (WDLevel level in levelManager.Levels) 
        {

            if (level.sectionNumbers.Contains(currentsectionID)) 
            {
                potentiallevels.Add(level);
            
            }

        }

        currentLevel = potentiallevels[Random.Range(0, potentiallevels.Count)];
      

        StartCoroutine(OverWorldWait(3));

    
    }

    public void GameOver() 
    {

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

    public void UpdateVarFrame(object varIs,string name) 
    {
        if (name == "KneeCaps") { kneecapsText.text = varIs.ToString();}
        if (name == "Age") { ageText.text = varIs.ToString(); }
        if (name == "Body") { bodyText.text = varIs.ToString(); CheckGameOver(varIs,0, 10000); }
        if (name == "Awareness") { awarenessText.text = varIs.ToString(); CheckGameOver(varIs,0, 100); }
        if (name == "Mood") { moodText.text = varIs.ToString(); }
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

    public void CheckGameOver(object current,int min, int max)
    {

        Debug.Log((int)current + " "  );

        if (deadNextTurn == false)
        {
            if ((int)current <= min || (int)current >= max) { deadNextTurn = true; }
            else { deadNextTurn = false; }
        }

        
    }

   public void CreatePotionButton(int potionID) 
    {

        Debug.Log("potion spawned is "  + potionID);
        
        if (createPotion == true)
        {
            Button button = Instantiate(potionPrefrab) as Button;
            button.transform.SetParent(inventory.transform, false);

            button.interactable = true;

            button.onClick.AddListener(delegate
            {
                ConsumePotion(button, potionID);
            });
        }
        
    }


    public void ConsumePotion(Button button, int choice) 
    {

       
        ink.story.state.LoadJson(ink.lastState);

        switch (EffectID[choice]) 
        {
            case 1:
                Debug.Log(EffectID[choice]);
                ink.story.EvaluateFunction("potion1");
                break;
            case 2:
                Debug.Log(EffectID[choice]);
                ink.story.EvaluateFunction("potion2");
                break;
            case 3:
                Debug.Log(EffectID[choice]);
                ink.story.EvaluateFunction("potion3");
                break;
            case 4:
                Debug.Log(EffectID[choice]);
                ink.story.EvaluateFunction("potion4");
                break;
            case 5:
                Debug.Log(EffectID[choice]);
                ink.story.EvaluateFunction("potion5");
                break;
        }

        ink.showNextLine = false;
        ink.lastpressedbuttonText = ink.lastchoice.text;
        ink.SaveMomentBeforeState();
        ink.story.ChooseChoiceIndex(ink.lastchoice.index);

        createPotion = false;
        ink.instantTextScroll = true;
        ink.RefreshView();
        createPotion = true;


        Destroy(button.gameObject);
       
       
    }

}
