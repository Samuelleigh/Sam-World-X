using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WDLevelManager : MonoBehaviour
{

    public List<WDLevel> Levels;
    public WDLevel deathlevel;


    public List<WDLevel> Group1Levels;
    public List<WDLevel> Group2Levels;
    public List<WDLevel> Group3Levels;


    private void Awake()
    {

        for (int i = 0; i < Levels.Count; i++) 
        {


            if (Levels[i].EventName == "Group1") 
            {
                int r = Random.Range(0, Group1Levels.Count);
                Levels[i] = Group1Levels[r];
                Group1Levels.RemoveAt(r);
                       
            }

            if (Levels[i].EventName == "Group2")
            {

                int r = Random.Range(0, Group2Levels.Count);
                Levels[i] = Group1Levels[r];
                Group1Levels.RemoveAt(r);


            }


            if (Levels[i].EventName == "Group3")
            {

                int r = Random.Range(0, Group3Levels.Count);
                Levels[i] = Group1Levels[r];
                Group1Levels.RemoveAt(r);


            }




        }


    }

}
