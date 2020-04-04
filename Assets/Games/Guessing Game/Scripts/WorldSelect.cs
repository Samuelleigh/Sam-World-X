using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WorldSelect : MonoBehaviour
{

    public GameLogic gamelogic;
    public DataManager dataManager;
    public List<GameObject> worldbuttons;
    public Color greenMe;

    private void Awake()
    {
        dataManager = FindObjectOfType<DataManager>();
        gamelogic = FindObjectOfType<GameLogic>();
    }

    // Start is called before the first frame update
    void Start()
    {
        RefreshWorlds();



    }

    private void OnEnable()
    {
        RefreshWorlds();
    }

    public void RefreshWorlds()
    {
        //go through each world button, if it's larger than the number of worlds, turn it off,
        //get the number of correct levels in the that world
        //if that number is equal to the number of 
        //Debug.Log("refreshed");

        foreach (Data_World world in dataManager.AllWorlds)
        {
            int count = 0;
            foreach (Data_Level level in world.levels)
            {

                if (level.completed == true) { count++; }
            }

            if (count >= world.levels.Count)
            {
                world.completed = true;

            }
        }

        //makes visual changes depending on the previous 

        for (int i = 0; i < worldbuttons.Count; i++)
        {

            if (i < dataManager.AllWorlds.Count)
            {

                if (dataManager.AllWorlds[i].completed == true)
                {
                    //visual change goes here
                    worldbuttons[i].GetComponent<Image>().color = greenMe;
                }


            }
            else { worldbuttons[i].SetActive(false); }

        }



    }

    public void SelectWorld(int worldbutton)
    {
        Debug.Log("hit button " + worldbutton );
        gamelogic.ChangeWorld(dataManager.AllWorlds[worldbutton]);

    }


    // Update is called once per frame
    void Update()
    {
        
    }
}
