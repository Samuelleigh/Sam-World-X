using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SightGameLogic : MonoBehaviour
{

    public SightLevel currentLevel;
    public SightLevel PreviousLevel;
    public SightLevelManager manager;

    public bool playFromStart;

    public int timeLeft;

    private void Awake()
    {
        manager = FindObjectOfType<SightLevelManager>();
    }

    // Start is called before the first frame update
    void Start()
    {

        Debug.Log("play from start is  " + playFromStart);

        if (playFromStart)
        {

            manager.currentlevelID = 0; 
         
        }
    
        LoadLevel(manager.levels[manager.currentlevelID]);
        
    }


    public void LoadNextLevel() 
    {      
        manager.currentlevelID++;
        LoadLevel(manager.levels[manager.currentlevelID]);
    }

    public void LoadPreviousLevel() 
    {

        manager.currentlevelID--;
        LoadLevel(manager.levels[manager.currentlevelID]);
    }

    public void LoadLevel(SightLevel level) 
    {

        if (level == currentLevel) { return; }

        currentLevel.LevelSceneParent.SetActive(false);
        currentLevel.LevelUIParent.SetActive(false);

        currentLevel = level;

        currentLevel.LevelSceneParent.SetActive(true);
        currentLevel.LevelUIParent.SetActive(true);


        if (Application.isPlaying)
        {

            if (currentLevel.endStateType == EndStateType.TimeLimit)
            {
                Debug.Log("time limit start");
                StartCoroutine(Countdown());
            }
        }

    }

    public IEnumerator Countdown() 
    {
        timeLeft = currentLevel.timeTilSwitch;
        yield return new WaitForSeconds(timeLeft);        
        LoadNextLevel();


    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
