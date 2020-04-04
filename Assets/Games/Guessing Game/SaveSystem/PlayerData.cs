using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlayerData
{
    public bool firstTime;
    public List<bool> worldsComplete;
    public List<bool> levelsCompleted;
    public List<bool> levelsUnlocked;

    

    public PlayerData(List<Data_World> world,bool firsttime)
    {
        firstTime = firsttime;
        worldsComplete = new List<bool>();
        levelsCompleted = new List<bool>();
        levelsUnlocked = new List<bool>();

        
        for (int i = 0; i < world.Count; i++)
        {

            if (world[i].unlocked == true)
            {
                worldsComplete.Add(true);
            }
            else
            {
                worldsComplete.Add(false);

            }

        

            for (int z = 0; z < world[i].levels.Count; z++)
            {

                if (world[i].levels[z].unlocked == true)
                {
                  
                    levelsUnlocked.Add(true);
                }
                else {
                   
                    levelsUnlocked.Add(false);
                }


                if (world[i].levels[z].completed == true)
                {
                    levelsCompleted.Add(true);
                }
                else
                {
                    levelsCompleted.Add(false);
                }

            }


        }


    }

}
