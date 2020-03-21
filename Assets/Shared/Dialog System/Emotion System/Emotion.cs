using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]

public enum EmotionType {neutral, happy, sad, angry, concerned,annoyed};

[System.Serializable]
public class Emotion
{
    public EmotionType EmotionType;

    public Sprite RightEye;
    public Sprite LeftEye;
    public Sprite Mouth;
    public Sprite OpenMouth;

}
