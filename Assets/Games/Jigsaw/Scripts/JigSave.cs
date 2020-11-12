using System.Collections;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Collections.Generic;
using UnityEngine;

namespace MovingJigsaw
{
    public static class JigSave
    {

        public static void SavePlayer(List<bool> jig)
        {

            BinaryFormatter formatter = new BinaryFormatter();
            string path = Application.persistentDataPath + "/player.jigs";
            FileStream stream = new FileStream(path, FileMode.Create);

            formatter.Serialize(stream, jig);
            stream.Close();
        }

        public static List<bool> LoadPlayer()
        {

            string path = Application.persistentDataPath + "/player.jigs";
            if (File.Exists(path))
            {
                BinaryFormatter formatter = new BinaryFormatter();
                FileStream stream = new FileStream(path, FileMode.Open);

                List<bool> jigs = formatter.Deserialize(stream) as List<bool>;
                stream.Close();

                return jigs;

            }
            else
            {
                Debug.LogError("no save file found");
                return null;

            }

        }

    }
}
