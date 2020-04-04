using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class LevelSelect : MonoBehaviour
{

    public Data_World currentWorld;
    public GameLogic gameLogic;


    public GameObject line;
    public List<GameObject> lines;
    public GameObject contentView;
    public TextMeshProUGUI WorldText;
    public GameObject EndFrame;

    public Sprite lockedSprite;
    public Sprite unlockedSprite;

    public Color greenMe;
    public Color GreyMe;

    public GameObject worldSelect;

    // Start is called before the first frame update
    void Start()
    {
        gameLogic = FindObjectOfType<GameLogic>();
        EndFrame.SetActive(false);
    }

    private void OnEnable()
    {
        RefreshList();    
    }

    public void OpenWorld(Data_World world)
    {


        //set up cache if this is the first time a world is opening.
        if (lines.Count == 0)
        {
            for (int i = 0; i < 40; i++)
            {
                lines.Add(Instantiate(line));
                lines[i].transform.SetParent(contentView.transform);
                lines[i].SetActive(false);

            }

        }



        //set world as current world
        currentWorld = world;

        //make lines inactive
        for (int i = 0; i < 40; i++)
        {
            if (lines[i].activeSelf == true) { lines[i].SetActive(false); }
        }

        //add line to content
        for (int i = 0; i < currentWorld.levels.Count; i++)
        {

            //Debug.Log("mondays");
            lines[i].SetActive(true);
            lines[i].transform.SetParent(contentView.transform);
            lines[i].GetComponentInChildren<TextMeshProUGUI>().text = "Level " + (i + 1);

        }

        RefreshList();

        //designate the correct thigns

        //set world name

    }

    public void StartLevel()
    {


        //if the level isn't completed, do this
        if (gameLogic.currentWorld.completed == false)
        {
            //picks which level to play, the while loop stops the level being locked or completed.
            int levelpicked = Random.Range(0, currentWorld.levels.Count);
            Data_Level level = currentWorld.levels[levelpicked];



           // Debug.Log(levelpicked);
           // Debug.Log(level.completed);

           // Debug.Log(level.completed);

            while ((level.completed == true) || (level.unlocked == false))
            {
              //  Debug.Log("attempt");

                levelpicked = Random.Range(0, currentWorld.levels.Count);
                level = currentWorld.levels[levelpicked];


            }


            gameLogic.currentLevel = level;


            gameLogic.ChangeState("Gameplay");
            gameLogic.ChangeLevel(gameLogic.currentLevel);
        }
        else
        {

             EndFrame.SetActive(true);

        }


       // currentWorld.levels[levelpicked]
    }





        
        

    


    public void RefreshList()
    {

      //  Debug.Log("refreshedlist");
        for (int i = 0; i < currentWorld.levels.Count; i++)
        {

            //checks to see if numofcompletedlevels is enough to unlock the next level, then unlocks it.
            if (currentWorld.numOfCompletedLevels >= currentWorld.levels[i].unlockedAt)
            {
                currentWorld.levels[i].unlocked = true;
            }


            //if unlocked 
            if (currentWorld.levels[i].unlocked == false)
            {

                lines[i].GetComponent<Image>().color = Color.grey;

                lines[i].GetComponentInChildren<LevelBarScript>().lockedImage.sprite = lockedSprite;
                lines[i].GetComponentInChildren<LevelBarScript>().lockedNum.text = currentWorld.levels[i].unlockedAt.ToString();
            }

            if (currentWorld.levels[i].unlocked == true)
            {
                lines[i].GetComponent<Image>().color = GreyMe;
                lines[i].GetComponentInChildren<LevelBarScript>().lockedImage.sprite = unlockedSprite;
                lines[i].GetComponentInChildren<LevelBarScript>().lockedNum.text = "";

            }

            //if completed
            if (currentWorld.levels[i].completed == true)
            {
                lines[i].GetComponent<Image>().color = greenMe;
                lines[i].GetComponentInChildren<LevelBarScript>().lockedImage.sprite = lockedSprite;

            }

            //triggers refresh world
            

        }

        worldSelect.GetComponent<WorldSelect>().RefreshWorlds();

    

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
