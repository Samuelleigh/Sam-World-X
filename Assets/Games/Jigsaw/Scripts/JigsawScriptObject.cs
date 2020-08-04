using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PuzzleResolution {_700x700,_500x500,_700x300,Custom};

[CreateAssetMenu(fileName = "New Level",menuName = "Jigsaw")]
[System.Serializable]
public class JigsawScriptObject : ScriptableObject
{

    public new string name;
    public int Xpieces;
    public int Ypieces;
    public PuzzleResolution puzzleResolution;
    public int XCustom;
    public int YCustom;
    public string SceneName;
    public int numberOfpuzzles = 1;
    public List<int> cameraID;
    public bool enableShift = true;
    public string style;
    public int difficulty;
    public bool centerAligned = false;

    [TextArea]
    public string Notes;
}
