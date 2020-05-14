using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "New Level",menuName = "Jigsaw Level")]
[System.Serializable]
public class JigsawScriptObject : ScriptableObject
{

    public new string name;
    public int Xpieces;
    public int Ypieces;
    public string SceneName;
    public int numberOfpuzzles = 1;
    public List<int> cameraID;
    public bool enableShift = true;
    public string style;
    public int difficulty;
    public bool centerAligned = false;

}
