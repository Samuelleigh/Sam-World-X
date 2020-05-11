using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;



[System.Serializable]
public class JigsawLevel
{

    public string Name;
    public bool alt = false;
    public int Xpieces;
    public int Ypieces;
    public string SceneName;
    public int numberOfpuzzles = 1;
    public List<int> cameraID;
    public bool completed = false;
    public bool enableShift = true;

}
