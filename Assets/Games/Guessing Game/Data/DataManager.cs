using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

public class DataManager : MonoBehaviour
{

    public bool DebugStartFromStart;

    public List<Data_World> AllWorlds;
    public List<Data_World> backup;

    public PlayerData player;

    private void Awake()
    {
        backup = DeepClone<List<Data_World>>(AllWorlds);
    }

    // Start is called before the first frame update
    void Start()
    {
        if (DebugStartFromStart == true) { SaveGame(backup); DebugStartFromStart = false; }


        LoadGame(); 

       

    }


    public void SaveGame(List<Data_World> SaveWorld)
    {
        SaveSystem.SavePlayer(SaveWorld, DebugStartFromStart);
    }

    public void LoadGame()
    {

      //  Debug.Log("loaded game");

       player = SaveSystem.LoadPlayer();

      //  if (player.worldsComplete.Count == 0) { Debug.Log("no save file present so created a blank one"); SaveGame(backup); LoadGame(); return; }



        //is world locked

        for (int i = 0; i < player.worldsComplete.Count; i++)
        {
            AllWorlds[i].unlocked = player.worldsComplete[i];
   
        }

        //is world complete

        for (int i = 0; i < player.worldsComplete.Count; i++)
        {
            AllWorlds[i].unlocked = player.worldsComplete[i];

        }



        //is level complete

        int placeinList = 0;

            for (int i = 0; i < AllWorlds.Count; i++)
            {

                for (int q = 0; q < AllWorlds[i].levels.Count; q++)
                {
                    if (player.levelsCompleted[placeinList] == true)
                    {
                    AllWorlds[i].levels[q].completed = true;                    
                    placeinList++;

                    }
                }
            }

           
            
        //is level locked

       placeinList = 0;

        for (int i = 0; i < AllWorlds.Count; i++)
        {

            for (int q = 0; q < AllWorlds[i].levels.Count; q++)
            {

                AllWorlds[i].levels[q].unlocked = player.levelsUnlocked[placeinList];
                placeinList++;

            }
        }


    }

    public void ResetEverything()
    {
        AllWorlds = DeepClone<List<Data_World>>(AllWorlds);
    }


    public static T DeepClone<T>(T obj)
    {
        using (var ms = new MemoryStream())
        {
            var formatter = new BinaryFormatter();
            formatter.Serialize(ms, obj);
            ms.Position = 0;

            return (T)formatter.Deserialize(ms);
        }
    }

}

