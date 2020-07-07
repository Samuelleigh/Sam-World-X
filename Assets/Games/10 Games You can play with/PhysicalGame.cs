using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

[System.Serializable]
public class PhysicalGame
{
    
    public string title1;
    [TextArea]
    public string writing1;
    public string title2;
    [TextArea]
    public string writing2;
    public Sprite sprite;
    public VideoClip videoClip;

    public bool isSpread;
    public bool SingleGame;
}
