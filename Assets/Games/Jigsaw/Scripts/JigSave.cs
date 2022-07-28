using System.Collections;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Collections.Generic;
using UnityEngine;

namespace MovingJigsaw
{
    public static class JigSave
    {

        public static void SavePlayer(List<JigsawlevelSave> jig1)
        {
            BinaryFormatter formatter = new BinaryFormatter();
            string path = Application.persistentDataPath + "/player.jigs";
            FileStream stream = new FileStream(path, FileMode.Create);

            Debug.Log("save function called");

            formatter.Serialize(stream, jig1);
            stream.Close();
        }

        public static List<JigsawlevelSave> LoadPlayer()
        {

            string path = Application.persistentDataPath + "/player.jigs";
            if (File.Exists(path))
            {
                BinaryFormatter formatter = new BinaryFormatter();
                FileStream stream = new FileStream(path, FileMode.Open);


                Debug.Log("Load Function Called");

                List<JigsawlevelSave> jigs = formatter.Deserialize(stream) as List<JigsawlevelSave>;
                stream.Close();

                return jigs;

            }
            else
            {
                Debug.LogError("no save file found");
                return null;

            }

        }

        public static void SeriouslyDeleteAllSaveFiles()
        {
            BinaryFormatter formatter = new BinaryFormatter();
            string path = Application.persistentDataPath + "/player.jigs";
            FileStream stream = new FileStream(path, FileMode.Create);

            Debug.Log("Delete Save File");

            formatter.Serialize(stream, new List<JigsawlevelSave>());
            stream.Close();

        }
    }
}
