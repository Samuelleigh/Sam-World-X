using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

enum GameState {Start, Overworld, Content, End }

public class WayDownGame : MonoBehaviour
{

    GameState gameState = GameState.Start;
    public int currentsectionID;
    public List<WDLevel> potentiallevels;

    public UIMaster ui;
    public WDLevelManager levelManager;
    public WDLevel currentLevel;
    public WayDownInk ink;

    //Media View
    public Camera MediaCamera;



    //variables variables
    private List<string> varNames = new List<string>();
    delegate void MyDelegate(object VarIs,string name);
    MyDelegate myDelegate;

    public GameObject VaribleFrame;
    public TextMeshProUGUI kneecapsText;
    public TextMeshProUGUI ageText;
    public TextMeshProUGUI moodText;
    public TextMeshProUGUI strangeText;
    public TextMeshProUGUI strangeAmountText;


    private void Awake()
    {
        levelManager = FindObjectOfType<WDLevelManager>();
        ink = FindObjectOfType<WayDownInk>();
    }

    // Start is called before the first frame update
    void Start()
    {
        VaribleFrame.SetActive(false);
        ui.SwitchLayer(0);

        myDelegate = UpdateVarFrame;

        varNames.Add("KneeCaps");
        varNames.Add("Age");
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

        

        //turn on varible frame
        VaribleFrame.SetActive(true);


        gameState = GameState.Start;
        currentLevel = levelManager.Levels[0];
        ink.StartStory();

        //obserbe

        ink.story.ObserveVariables(varNames,(string varName, object newValue) =>
        {
            Debug.Log(varName);
            Debug.Log(newValue);
            myDelegate(newValue, varName);
        });

        LoadLevel();

    }

    public void LoadLevel() 
    {
        ui.SwitchLayer(2);
        ink.story.ChoosePathString(currentLevel.EventName);
        ink.RefreshView();

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

            if (level.sectionNumber == currentsectionID) 
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

    }

    public void ShowMedia(int cameraID) 
    {
        ui.IndivdualFrames[1].SetActive(true);

        Debug.Log(cameraID);

        MediaCamera.transform.position = currentLevel.cameraTransform[cameraID].position;
        MediaCamera.transform.rotation = currentLevel.cameraTransform[cameraID].rotation;
     
    
    }

    public void HideMedia() 
    {

        ui.IndivdualFrames[1].SetActive(false);

    }

    public void UpdateVarFrame(object varIs,string name) 
    {
        if (name == "KneeCaps") { kneecapsText.text = varIs.ToString(); }
        if (name == "Age") { ageText.text = varIs.ToString(); }
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
}
