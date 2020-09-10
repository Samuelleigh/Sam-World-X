using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public enum PuzzleResolution {_800x800, _1248x702,_450x800, Custom};

[CreateAssetMenu(fileName = "New Level",menuName = "Jigsaw")]
[System.Serializable]
public class JigsawScriptObject : ScriptableObject
{

    public new string name;
    public int Xpieces;
    public int Ypieces;
    public PuzzleResolution puzzleResolution;
    private int XCustom;
    private int YCustom;
    public string SceneName;
    public int numberOfpuzzles = 1;
    public List<int> cameraID;
    public bool enableShift = true;
    public string style;
    public int difficulty;
    public bool centerAligned = false;
    public bool allowCustomFile = true;
    public VideoClip videoClip;
    public Texture puzzleTexture;

    [TextArea]
    public string Notes;
}
