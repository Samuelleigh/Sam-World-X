using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


public enum State { MainMenu, WorldSelect, LevelSelect, Gameplay, Extra };

public class GameLogic : MonoBehaviour
{

    public State currentState = State.MainMenu;
    public Data_World currentWorld;
    public Data_Level currentLevel;

    public DataManager dataManager;

    public List<GameObject> frameGroups;
    public LevelLogic levelLogic;
    public GameObject reminder;
    public GameObject Howtoplay;

    public bool changedname = false;
    public TMP_InputField field;

    public AudioManager audioManager;

    // Start is called before the first frame update
    void Start()
    {

        if (Application.platform == RuntimePlatform.WebGLPlayer)
        {
           // canvas.width = canvas.clientWidth * window.devicePixelRatio;

            //canvas.height = canvas.clientHeight * window.devicePixelRatio;

        }

        levelLogic = FindObjectOfType<LevelLogic>();
        dataManager = FindObjectOfType<DataManager>();
        audioManager = FindObjectOfType<AudioManager>();
        StartCoroutine(SpawnReminder());
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.Escape))
        {
            quitGame();

        }
    }

    public void ChangeName()
    {
        changedname = true;

    }

    public void PlayDrum()
    {
        audioManager.Playdrum();

    }

    //Sets up the Level Select Frame

    public void ChangeWorld(Data_World world)
    {
        currentWorld = world;
        int numofunlocked = 0;
        int numofcompleted = 0;

        //shows how many unlocked levels there are

        for (int i = 0; i < world.levels.Count; i++)
        {          
            if (world.levels[i].unlocked == true) { numofunlocked++; }
            if (world.levels[i].completed == true) { numofcompleted++; }

        }

        world.numOfunlockedLevels = numofunlocked;
        world.numOfCompletedLevels = numofcompleted;

        ChangeState("LevelSelect");
        frameGroups[2].GetComponentInChildren<LevelSelect>().OpenWorld(currentWorld);


        


    }

    //sets up the game logic

    public void ChangeLevel(Data_Level level)
    {
        levelLogic.StartLevel(level);

    }




  


    public void ChangeState(string ChangeTostr)
    {

        State newState = (State)System.Enum.Parse(typeof(State), ChangeTostr);

       

        //ending
       // Debug.Log("wow2");

        ChangeFrameGroup(newState);
        //change current state offically
        currentState = newState;
    }





    //UI MoveMent

    public void ChangeFrameGroup(State ChangeTo)
    {

      //  Debug.Log("wow3");
        //set current group to inactive
        int old = (int)currentState;
        frameGroups[old].SetActive(false);


        //set new group to active
        int newgroup = (int)ChangeTo;
        frameGroups[newgroup].SetActive(true);

        if (newgroup == 0) { if (changedname == true) { field.text = "ops, I forgot. sorry :("; } }

       // Debug.Log("wow4");

    }

    public void quitGame()
    {
        SaveSystem.SavePlayer(dataManager.AllWorlds,false);
        SaveSystem.LoadPlayer();

        Application.Quit();

    }

    IEnumerator SpawnReminder()
    {

        yield return new WaitForSeconds(120f);
        reminder.SetActive(true);
        MoveToFront(reminder);

        yield return new WaitForSeconds(180f);
        reminder.SetActive(true);
        MoveToFront(reminder);
    }



    public void MoveToFront(GameObject frame)
    {
       frame.transform.SetAsLastSibling();
    }





}
