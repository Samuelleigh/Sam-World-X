using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;


public class DrawingLevelManager : MonoBehaviour
{
    public List<DrawingLevel> levelList;
    public List<DrawingLevel> backup;

    public List<Sprite> sprite;


    private void Awake()
    {
        backup = DeepClone<List<DrawingLevel>>(levelList);
    }

    public void ResetData() 
    {

        levelList = backup;
    
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
