using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Collections.Generic;

public static class SaveSystem
{

    /// <summary>
    /// TH esave system works by saving a
    /// 
    /// 
    /// Most of the actual varible changes occure in the level logic 
    /// </summary>
    /// <param name="player"></param>
    /// <param name="firsttime"></param>


    public static void SavePlayer(List<Data_World> player,bool firsttime)
    {
        Debug.Log("saved");


        BinaryFormatter formatter = new BinaryFormatter();
        string path = Application.persistentDataPath + "/player.guess";
        FileStream stream = new FileStream(path, FileMode.Create);

        PlayerData data = new PlayerData(player,firsttime);

        formatter.Serialize(stream, data);
        stream.Close();

    }


    public static PlayerData LoadPlayer()
    {
        Debug.Log("loaded");

        string path = Application.persistentDataPath + "/player.guess";

        if (File.Exists(path))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);

            PlayerData data = formatter.Deserialize(stream) as PlayerData;
            stream.Close();


            return data;

        }
        else
        {
            Debug.LogError("Save File not found in" + path);
            return null;

        }


    }


}
