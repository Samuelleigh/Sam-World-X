using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;


public class DrawingGameLogic : MonoBehaviour
{
    // Start is called before the first frame update

    public SoundSystem sound;
    public UIMaster ui;
    public DrawingLevelManager drawingLevelManager;
    public DrawingLevel currentLevel;
    public Sprite currentSprite;
    public int CurrentLevelID;
    public DrawScript drawScript;

    public TextMeshProUGUI TimerCounter;
    public TextMeshProUGUI SpeakerText;
    public TextMeshProUGUI LinkText;
    public GameObject linkButton;

    public GameObject ResultFrame;
    public GameObject ResultImage;
    public GameObject startButton;
    public GameObject endbutton;


    public bool levelInSession = false;



    private void Awake()
    {
        ui = FindObjectOfType<UIMaster>();
        drawingLevelManager = FindObjectOfType<DrawingLevelManager>();
        sound = FindObjectOfType<SoundSystem>();
       // drawScript = FindObjectOfType<DrawScript>();
        
    }

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public void SelectLevel(int levelID) 
    {

        currentLevel = drawingLevelManager.levelList[levelID -1];
        CurrentLevelID = levelID -1;
        currentSprite = drawingLevelManager.sprite[CurrentLevelID];

        if (currentLevel.haslink == true)
        {
           // Debug.Log("lwww");
            linkButton.SetActive(true);
            LinkText.text = currentLevel.buttontext;
        }
        else 
        {
           // Debug.Log("link shouldn't been seen");
            linkButton.SetActive(false);
        
        }

        TimerCounter.text = currentLevel.Time.ToString();
        SpeakerText.text = "Speaker: " + currentLevel.nameOfSpeaker;

        ui.NextLayer();
    
    
    
    }

    public void Levelstart() 
    {
        levelInSession = true;
        startButton.SetActive(false);
        sound.PlaySound(currentLevel.levelNum.ToString());

        StartCoroutine(Timer());
    
    }

    public void LevelEnd(int levelID) 
    {

        ResultFrame.SetActive(true);
        ResultImage.GetComponent<Image>().sprite = currentSprite;

     

        endbutton.SetActive(true);

    
    }

    public void BackToMenu() 
    {

        int d = drawScript.drawHistory.Count;

        for (int i = 0; i <= d; i++) 
        {
            drawScript.Undo();

        }

        startButton.SetActive(true);
        endbutton.SetActive(false);
        ResultFrame.SetActive(false);
        drawingLevelManager.ResetData();

        ui.PreviousLayer();
    }


    public IEnumerator Timer() 
    {

        while (levelInSession) 
        {

            yield return new WaitForSeconds(1);

            currentLevel.Time--;
            TimerCounter.text = currentLevel.Time.ToString();

            if (currentLevel.Time == 0) { levelInSession = false; }

        }

        LevelEnd(CurrentLevelID);

    
    
    
    }

    public void OpenWebsite() 
    {


        Application.OpenURL(currentLevel.linkURL);
    
    
    }
}
